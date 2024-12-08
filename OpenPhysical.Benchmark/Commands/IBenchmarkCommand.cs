using Spectre.Console.Cli;

namespace OpenPhysical.Benchmark.Commands;


public interface IBenchmarkCommand : ICommand
{
    /// <summary>
    /// Registers this command with the parent command
    /// </summary>
    /// <param name="parent"></param>
    public void Register(IConfigurator parent);
}