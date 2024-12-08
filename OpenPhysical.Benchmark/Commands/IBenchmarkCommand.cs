namespace OpenPhysical.Benchmark.Commands;

#region

using Spectre.Console.Cli;

#endregion

public interface IBenchmarkCommand : ICommand
{
    /// <summary>
    ///     Registers this command with the parent command
    /// </summary>
    /// <param name="parent"></param>
    public void Register(IConfigurator parent);
}
