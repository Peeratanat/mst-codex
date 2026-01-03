using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models
{
    public static class PRRequestJobStatusKeys
    {
        public static string Syncing = "1";
        public static string Success = "2";
        public static string Retrying = "3";
        public static string Terminated = "4";
    }

    public static class PRCancelJobStatusKeys
    {
        public static string Syncing = "1";
        public static string Success = "2";
        public static string Retrying = "3";
        public static string Terminated = "4";
    }
}
