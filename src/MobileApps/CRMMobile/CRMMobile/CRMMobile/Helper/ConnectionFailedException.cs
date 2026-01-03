using System;

public class ConnectionFailedException : Exception
{
    public string ConnectionMessage { get; set; }
    public bool IsInternetConnect { get; set; }
    public bool IsVPNConnect { get; set; }
}