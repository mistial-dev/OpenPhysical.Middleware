namespace OpenPhysical.CardEdge.Readers;

public class VirtualPivReader : IPivReader
{
    public required string Name { get; set; }

    public void ColdReset()
    {
        throw new NotImplementedException();
    }
}