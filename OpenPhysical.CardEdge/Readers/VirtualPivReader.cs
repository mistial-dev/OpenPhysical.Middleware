namespace OpenPhysical.CardEdge.Readers;

using System;

public class VirtualPivReader : IPivReader
{
    public required string Name { get; set; }

    public void ColdReset() => throw new NotImplementedException();
}
