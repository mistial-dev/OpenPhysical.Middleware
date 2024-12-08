#region

using static OpenPhysical.Middleware.PivStatusWord;

#endregion

namespace OpenPhysical.Middleware;

#region

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CardEdge.Application;
using CardEdge.Credential;
using CardEdge.PivApplication;
using CardEdge.Readers;
using JetBrains.Annotations;

#endregion

/// <summary>
///     PIV Middleware, per SP 800-73-5
///     https://nvlpubs.nist.gov/nistpubs/SpecialPublications/NIST.SP.800-73pt3-5.pdf
///     This interface serves as a facade over the Card Edge functionality, providing a mostly-compatible API for the PIV
///     application.
/// </summary>
[PublicAPI]
[SuppressMessage("Performance", "CA1822:Mark members as static")]
public class PivMiddleware
{
    private const string StaticVersionString = "800-73-5 Client API with SM";

    /// <summary>
    ///     Returns the PIV Middleware version string.
    /// </summary>
    /// <param name="version"></param>
    /// <returns></returns>
    [PublicAPI]
    public static PivStatusWord PivMiddlewareVersion(ref string version)
    {
        version = StaticVersionString;
        return PivOk;
    }

    /// <summary>
    ///     Connects the client API to the PIV Card Application on a specific ICC.
    /// </summary>
    /// <param name="sharedConnection"></param>
    /// <param name="connection"></param>
    /// <param name="card"></param>
    /// <returns></returns>
    [PublicAPI]
    public static PivStatusWord PivConnect(bool sharedConnection, IPivReader connection, out ICard? card)
    {
        card = null;
        return PivOk;
    }

    /// <summary>
    ///     Disconnect the PIV API from the PIV Card Application and the ICC that contains the PIV Card Application.
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    [PublicAPI]
    public static PivStatusWord PivDisconnect(ICard card) => PivOk;

    /// <summary>
    ///     Set the PIV Card Application as the currently selected card application and establish the PIV Card Application’s
    ///     security state.
    /// </summary>
    /// <param name="card"></param>
    /// <param name="applicationId"></param>
    /// <param name="applicationProperties"></param>
    /// <returns></returns>
    [PublicAPI]
    public static PivStatusWord PivSelectApplication(ICard? card, ApplicationId applicationId,
        out ApplicationProperties? applicationProperties)
    {
        applicationProperties = null;
        return PivOk;
    }

    /// <summary>
    ///     Establish secure messaging with the PIV Card Application.
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    [PublicAPI]
    public static PivStatusWord PivEstablishSecureMessaging(ICard card) => PivOk;

    /// <summary>
    ///     Set the security state within the PIV Card Application.
    /// </summary>
    /// <param name="card"></param>
    /// <param name="authenticators"></param>
    /// <returns></returns>
    [PublicAPI]
    public static PivStatusWord PivLogIntoCardApplication(ICard card, IEnumerable<IAuthenticator> authenticators) =>
        PivOk;

    /// <summary>
    ///     Return the entire data content of the named data object.
    /// </summary>
    /// <param name="card"></param>
    /// <param name="oid"></param>
    /// <param name="responseData"></param>
    /// <returns></returns>
    [PublicAPI]
    public static PivStatusWord PivGetData(ICard card, ObjectIdentifier oid, out byte[]? responseData)
    {
        responseData = null;
        return PivOk;
    }

    /// <summary>
    ///     Reset the application security state/status of the PIV Card Application.
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    [PublicAPI]
    public static PivStatusWord PivLogoutOfCardApplication(ICard card) => PivOk;

    /// <summary>
    ///     Perform a cryptographic operation, such as encryption or signing on a sequence of bytes.  SP 800-73-5 Part 1,
    ///     Appendix C describes recommended procedures for PIV algorithm identifier discovery.
    /// </summary>
    /// <param name="card"></param>
    /// <param name="algorithmIdentifier"></param>
    /// <param name="keyReference"></param>
    /// <param name="input"></param>
    /// <param name="output"></param>
    /// <returns></returns>
    [PublicAPI]
    public static PivStatusWord PivCrypt(ICard card, AlgorithmIdentifier algorithmIdentifier, KeyReference keyReference,
        byte[] input, out byte[]? output)
    {
        output = null;
        return PivOk;
    }

    /// <summary>
    ///     Replace the entire data content of the named data object with the provided data.
    /// </summary>
    /// <param name="card"></param>
    /// <param name="oid"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [PublicAPI]
    public static PivStatusWord PivPutData(ICard card, ObjectIdentifier oid, byte[] input) => PivOk;

    /// <summary>
    ///     Generates an asymmetric key pair in the currently selected card application.
    ///     If the provided key reference exists and the cryptographic mechanism associated
    ///     with the reference data identified by this key reference is the same as the
    ///     provided cryptographic mechanism, then the generated key pair replaces in
    ///     entirety the key pair currently associated with the key reference.
    /// </summary>
    /// <param name="card"></param>
    /// <param name="keyReference"></param>
    /// <param name="mechanism"></param>
    /// <param name="publicKey"></param>
    /// <returns></returns>
    [PublicAPI]
    public static PivStatusWord PivGenerateKeyPair(ICard card, KeyReference keyReference,
        CryptographicMechanism mechanism, out byte[]? publicKey)
    {
        publicKey = null;
        return PivOk;
    }
}
