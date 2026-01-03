using System;
namespace Database.Models.MasterKeys
{
    public static class ReadyToTransferStatusKeys
    {
         
        /// <summary>
        /// เตรียมโอน
        /// </summary>
        public static string Ready = "Ready";

        /// <summary>
        /// ยังไม่เตรียมโอน
        /// </summary>
        public static string NotReady = "NotReady";

        /// <summary>
        /// โอนแล้ว
        /// </summary>
        public static string Transferred = "Transferred";
    }
}
