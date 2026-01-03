using System;
using System.Collections.Generic;
using System.Text;

namespace CRMMobile.Infrastructure
{
    public class UserNotLoginException : Exception
    {
        public string Username { get; set; }
    }
}
