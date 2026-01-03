using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.MasterKeys
{
    public class ChangeAgreementOwnerStatusKey
    {
        public static Guid WaitForLCMApprove = new Guid("c26fc536-5d43-4938-87c3-3eebe5292385");
        public static Guid WaitForAGApprovePrint = new Guid("e92f5e58-d6f9-46f4-8a5a-1e5d9b71ddb4");
        public static Guid WaitForAGApprove = new Guid("625989c2-8c3e-4404-ba3a-1a7490173d6b");
        public static Guid Success = new Guid("3e149bba-f8a4-4043-817d-12942fec8fc8");
        public static Guid LCMReject = new Guid("0c9de58f-b653-4be6-9121-2c37fc7289be");
        public static Guid AGReject = new Guid("2114ba36-98ac-45a3-8b4c-7214ac48cca8");
    }
}
