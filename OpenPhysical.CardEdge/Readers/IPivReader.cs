namespace OpenPhysical.CardEdge.Readers;

public interface IPivReader
{
    public string Name { get; }

    public void ColdReset();
}
