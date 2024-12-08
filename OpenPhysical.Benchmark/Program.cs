#region

using log4net.Config;

#endregion

#if DEBUG
// Configure log4net
[assembly: XmlConfigurator(ConfigFile = "Log.config", Watch = true)]
#endif

namespace OpenPhysical.Benchmark;

#region

using System.Diagnostics;
using System.Threading.Tasks;
using Commands;
using log4net;
using Spectre.Console.Cli;

#endregion

/// <summary>
///     PIV Benchmark application
/// </summary>
internal class Program
{
#if DEBUG
    // Log4net logger
    private static readonly ILog _log = LogManager.GetLogger(typeof(Program));
#endif

    /// <summary>
    ///     Entry point for the application
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static async Task<int> Main(string[] args)
    {
#if DEBUG
        _log.Info("Starting application");
#endif
        var mefContainer = new MefContainer();
        mefContainer.ComposeParts(new Program());

        var benchmarkApp = new CommandApp();
        benchmarkApp.Configure(config =>
        {
            // Configure the application
            config.CaseSensitivity(CaseSensitivity.None);
            config.SetApplicationName(Process.GetCurrentProcess().ProcessName);

            // Add commands from the MEF container
            foreach (var command in mefContainer.GetExportedValues<IBenchmarkCommand>())
            {
                // Commands can register themselves with the root command
                command.Register(config);
            }
        });

        return await benchmarkApp.RunAsync(args);
    }
}
