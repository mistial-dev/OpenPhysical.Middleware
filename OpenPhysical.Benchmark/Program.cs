#region

using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Reflection;
using log4net;
using log4net.Config;
using OpenPhysical.Benchmark.Commands;
using Spectre.Console.Cli;

#endregion

#if DEBUG
// Configure log4net
[assembly: XmlConfigurator(ConfigFile = "Log.config", Watch = true)]
#endif

namespace OpenPhysical.Benchmark;

internal class Program
{
#if DEBUG
    private static readonly ILog _log = LogManager.GetLogger(typeof(Program));
#endif

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
                command.Register(config);
            }

        });

        return await benchmarkApp.RunAsync(args);
    }
}