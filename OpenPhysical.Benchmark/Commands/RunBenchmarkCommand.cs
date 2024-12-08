#region

#endregion

namespace OpenPhysical.Benchmark.Commands;

#region

using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using CardEdge;
using CardEdge.Readers;
using JetBrains.Annotations;
using log4net;
using Middleware;
using Spectre.Console;
using Spectre.Console.Cli;
using ApplicationId = CardEdge.Application.ApplicationId;

#endregion

/// <summary>
///     Runs a benchmark on the inserted PIV card
/// </summary>
[UsedImplicitly]
[Export(typeof(IBenchmarkCommand))]
public class RunBenchmarkCommand : AsyncCommand<RunBenchmarkCommand.Settings>, IBenchmarkCommand
{
    private const bool SharedConnection = false;

#if DEBUG
    private static readonly ILog Log = LogManager.GetLogger(typeof(Program));
#endif

    /// <summary>
    ///     Registers this command with the parent command
    /// </summary>
    /// <param name="parent"></param>
    [UsedImplicitly]
#pragma warning disable CA1822
    public void Register(IConfigurator parent)
#pragma warning restore CA1822
    {
        parent.AddCommand<RunBenchmarkCommand>("run")
            .WithDescription("Benchmark inserted PIV card")
            .WithExample("run", "auto");
    }

    public override Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        // Attempt to find a reader
        var reader = FindReader(settings.ReaderName);

#if DEBUG
        Log.Info($"Selected reader: {reader.Name}");
#endif
        AnsiConsole.MarkupLine("Selected reader: [bold]{0}[/]", reader.Name);

        // Make sure we have a PIN
        if (string.IsNullOrWhiteSpace(settings.Pin))
        {
            settings.Pin = PromptUserForPin();
        }

        // Use the PIV Middleware to connect to the card, throwing an exception if status is not PivOk
        var status = PivMiddleware.PivConnect(SharedConnection, reader, out var card);
        if (status is not PivStatusWord.PivOk)
        {
            AnsiConsole.MarkupLine("[red]Error connecting to card[/]");
            Environment.Exit(CardEdgeErrors.ErrorConnect);
        }

        // Select the PIV application
        status = PivMiddleware.PivSelectApplication(card, ApplicationId.PivApplicationTruncated,
            out var applicationProperties);
        if (status is not PivStatusWord.PivOk)
        {
            AnsiConsole.MarkupLine("[red]Error selecting PIV application[/]");
            Environment.Exit(CardEdgeErrors.ErrorSelectApplication);
        }

        // The benchmark has completed successfully
        return Task.FromResult(CardEdgeErrors.NoError);
    }

    /// <summary>
    ///     Validate the command settings
    /// </summary>
    /// <param name="context"></param>
    /// <param name="settings"></param>
    /// <returns></returns>
    public override ValidationResult Validate(CommandContext context, Settings settings)
    {
        // Validate the PIN
        if (settings.Pin is not null)
        {
            if (ValidatePin(settings.Pin).Successful is false)
            {
                return ValidatePin(settings.Pin);
            }
        }

        return ValidationResult.Success();
    }

    /// <summary>
    ///     Validates that the PIN is numeric and 6-8 digits
    /// </summary>
    /// <param name="pin"></param>
    /// <returns></returns>
    private static ValidationResult ValidatePin(string pin)
    {
        // Ensure the pin is numeric
        if (!int.TryParse(pin, out _))
        {
            return ValidationResult.Error("PIN must be numeric");
        }

        return pin.Length is < 6 or > 8 ? ValidationResult.Error("PIN must be 6-8 digits") : ValidationResult.Success();
    }

    /// <summary>
    ///     Prompt the user for the PIV card PIN
    /// </summary>
    /// <returns></returns>
    private static string PromptUserForPin()
    {
        var prompt = new TextPrompt<string>("Enter the PIN for the PIV card:")
            .Secret()
            .ValidationErrorMessage("PIN must be 6-8 digits")
            .Validate(ValidatePin);

        return AnsiConsole.Prompt(prompt);
    }

    /// <summary>
    ///     Attempts to find a PIV reader to use
    /// </summary>
    /// <param name="readerName"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    private static IPivReader FindReader(string readerName)
    {
        var readers = PivReader.ScanForReaders().ToList();

        // If the specified reader is not "auto", look for a reader with that input as a substring,
        // ensuring that the reader is unique.
        if (readerName != "auto")
        {
            var matchingReaders = readers.Where(reader => reader.Name.Contains(readerName)).ToList();
            switch (matchingReaders.Count)
            {
                case 0:
                    AnsiConsole.MarkupLine("[red]No PIV readers found[/]");
                    Environment.Exit(CardEdgeErrors.ErrorNoReader);
                    break;
                case 1:
                    return matchingReaders[0];
                case > 1:
                    AnsiConsole.MarkupLine("[red]Multiple readers found[/]");
                    Environment.Exit(CardEdgeErrors.ErrorMultipleReaders);
                    break;
            }
        }

        // If the reader name is "auto", prompt the user for a reader.

        switch (readers.Count)
        {
            case 0:
                AnsiConsole.MarkupLine("[red]No PIV readers found[/]");
                Environment.Exit(CardEdgeErrors.ErrorNoReader);
                break;
            case >= 1:
            {
                var selectedReader = AnsiConsole.Prompt(
                    new SelectionPrompt<IPivReader>
                    {
                        Title = "Select a reader", Converter = v => v.Name, PageSize = 10
                    }.AddChoices(readers)
                );
                return selectedReader;
            }
        }

        throw new InvalidOperationException("No readers found");
    }

    /// <summary>
    ///     Settings for the RunBenchmarkCommand
    /// </summary>
    [UsedImplicitly]
    public class Settings : CommandSettings
    {
        [CommandArgument(0, "[readerName]")]
        [DefaultValue("auto")]
        [UsedImplicitly]
        public required string ReaderName { get; set; }

        [CommandOption("--pin <pin>")]
        [Description("The PIN to use for the PIV card")]
        public string? Pin { get; set; }
    }
}
