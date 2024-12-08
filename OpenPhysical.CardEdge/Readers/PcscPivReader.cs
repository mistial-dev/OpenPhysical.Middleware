#region

#endregion

namespace OpenPhysical.CardEdge.Readers;

#region

using System;
using System.Collections.Generic;
using WSCT.Wrapper.Desktop.Core;

#endregion

internal class PcscPivReader : IPivReader
{
    /// <summary>
    ///     Context for the card reader.
    /// </summary>
    internal required CardContext Context;

    /// <summary>
    ///     Name of the card reader.
    /// </summary>
    internal required string ReaderName;

    public void ColdReset() => throw new NotImplementedException();

    /// <summary>
    ///     Provides the name of the reader.
    /// </summary>
    public string Name => this.ReaderName;

    /// <summary>
    ///     Scans for all PC/SC PIV readers.
    /// </summary>
    /// <returns></returns>
    internal static IEnumerable<PcscPivReader> ScanForReaders()
    {
        // List all the readers
        var context = new CardContext();
        context.Establish();
        context.ListReaderGroups();
        var allGroups = context.Groups;

        // Iterate through all the readers in each group, creating a new PcscPivReader for each one.
        foreach (var group in allGroups)
        {
            context.ListReaders(group);
            foreach (var reader in context.Readers)
            {
                yield return new PcscPivReader { Context = context, ReaderName = reader };
            }
        }
    }
}
