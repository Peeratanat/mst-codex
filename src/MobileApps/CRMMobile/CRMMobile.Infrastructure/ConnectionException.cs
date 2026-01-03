using System;
namespace CRMMobile.Infrastructure
{
    public class ConnectionException : Exception
    {
        public string NetworkState { get; set; }
    }
}
