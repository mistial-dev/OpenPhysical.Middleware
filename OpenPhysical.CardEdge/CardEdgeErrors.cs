namespace OpenPhysical.CardEdge;

public static class CardEdgeErrors
{
    /// <summary>
    /// No reader found
    /// </summary>
    public const int ErrorNoReader = 0xF001;

    /// <summary>
    /// Multiple readers found that satisfy the search criteria
    /// </summary>
    public const int ErrorMultipleReaders = 0xF002;

    /// <summary>
    /// Error connecting to the card
    /// </summary>
    public const int ErrorConnect = 0xF003;

    /// <summary>
    /// Error selecting the application
    /// </summary>
    public const int ErrorSelectApplication = 0xF004;

    /// <summary>
    /// Signifies that no error occurred
    /// </summary>
    public const int NoError = 0x0000;
}