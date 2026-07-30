using System;
using System.Collections.Generic;

public class PlaywireAdError
{
    public PlaywireAdErrorCode Code { get; }
    public string Name { get; }
    public string Message { get; }
    public Dictionary<string, object> Metadata { get; }

    internal PlaywireAdError(int code, string name, string message, Dictionary<string, object> metadata)
    {
        Code = (PlaywireAdErrorCode)code;
        Name = name;
        Message = message;
        Metadata = metadata ?? new Dictionary<string, object>();
    }
}