#region

using JetBrains.Annotations;

#endregion

namespace OpenPhysical.Middleware;

[PublicAPI]
public enum PivStatusWord : byte
{
    PivOk = 0x00,
    PivConnectionDescriptionMalformed = 0x01,
    PivConnectionFailure = 0x02,
    PivConnectionLocked = 0x03,
    PivInvalidCardHandle = 0x05,
    PivCardApplicationNotFound = 0x06,
    PivAuthenticatorMalformed = 0x07,
    PivAuthenticationFailure = 0x08,
    PivInvalidOid = 0x09,
    PivDataObjectNotFound = 0x0A,
    PivSecurityCondititionsNotSatisfied = 0x0B,
    PivInvalidKeyrefOrAlgorithm = 0x0D,
    PivInsufficientBuffer = 0x0E,
    PivInputBytesMalformed = 0x0F,
    PivInsufficientCardResource = 0x10,
    PivUnsupportedCryptographicMechanism = 0x13,
    PivCardReaderError = 0x16
}