namespace OpenPhysical.CardEdge.Application;

#region

using System;
using JetBrains.Annotations;

#endregion

[PublicAPI]
public class ApplicationId
{
    /// <summary>
    ///     Stores the value of the Application Identifier.
    /// </summary>
    private byte[] _value;

    /// <summary>
    ///     Creates a new instance of the ApplicationId class.
    /// </summary>
    /// <param name="aid"></param>
    private ApplicationId(string aid) => this._value = Convert.FromHexString(aid);

    /// <summary>
    ///     Standard PIV Application Identifier.
    /// </summary>
    public static ApplicationId PivApplication { get; } = new("A0 00 00 03 08 00 00 10 00 01 00");

    /// <summary>
    ///     Truncated PIV Application Identifier, missing the version number
    /// </summary>
    public static ApplicationId PivApplicationTruncated { get; } = new("A0 00 00 03 08 00 00 10 00");
}
