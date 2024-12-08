#region

using JetBrains.Annotations;

#endregion

namespace OpenPhysical.CardEdge.Readers;

public abstract class PivReader : IPivReader
{
    public abstract string Name { get; }

    public abstract void ColdReset();

    [PublicAPI]
    public static IEnumerable<IPivReader> ScanForReaders()
    {
        var readerList = new List<IPivReader>();
        var pcscReaders = PcscPivReader.ScanForReaders();
        readerList.AddRange(pcscReaders);
        return readerList;
    }
}