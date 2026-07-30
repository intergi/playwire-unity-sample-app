using System;

public enum PlaywireAdErrorCode : int
{
    Unknown = 1000,
    NoFill = 1001,
    InvalidRequest = 1002,
    NetworkError = 1003,
    Timeout = 1004,
    ServerError = 1005,
    InternalError = 1006,
    DisplayError = 1007,
    MediationError = 1008,
    PreconditionFailed = 1009
}