using System;
namespace Database.Models.MasterKeys
{
    public static class PostGLDocumentTypeKeys
    {
        /// <summary>
        /// RV
        /// </summary>
        public const string RV = "RV";

        /// <summary>
        /// JV
        /// </summary>
        public const string JV = "JV";

        /// <summary>
        /// PI
        /// </summary>
        public const string PI = "PI";

        /// <summary>
        /// UN
        /// </summary>
        public const string UN = "UN";

        /// <summary>
        /// CA
        /// </summary>
        public const string CA = "CA";
        /// <summary>
        /// KR => KM
        /// </summary>
        public const string KM = "KM";
        /// <summary>
        /// RR
        /// </summary>
        public const string RR = "RR";
        /// <summary>
        /// RF
        /// </summary>
        public const string RF = "RF";
    }

    public static class SAPGLDocKeys
    {
        public static string YB = "YB"; // RV
        public static string YC = "YC"; // PI UN
        public static string YD = "YD"; // KR RR
        public static string YE = "YE"; // JV CA
        public static string KM = "KM"; // KM
    }
}
