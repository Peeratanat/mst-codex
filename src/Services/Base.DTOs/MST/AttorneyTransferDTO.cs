using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class AttorneyTransferDTO : BaseDTO
    {
        ///<summary>
        ///ลำดับ
        ///</summary>
        public int Seqn_No { get; set; }
        ///<summary>
        ///ชื่อ - นามสกุล
        ///</summary>
        public string Atty_FullName { get; set; }
        ///<summary>
        ///รหัสพนักงาน
        ///</summary>
        public string Atty_EmployeeCode { get; set; }
        ///<summary>
        ///วันเกิด
        ///</summary>
        public DateTime Atty_DateOfBirth { get; set; }
        ///<summary>
        ///อายุ
        ///</summary>
        public int Atty_Ages { get; set; }
        ///<summary>
        ///สัญชาติ
        ///</summary>
        public string Atty_Nationality { get; set; }
        ///<summary>
        ///ชื่อบิดา - มารดา
        ///</summary>
        public string Atty_Parent { get; set; }
        ///<summary>
        ///ที่อยู่
        ///</summary>
        public string Atty_Address { get; set; }

        ///<summary>
        ///หมู่บ้าน
        ///</summary>
        public string Atty_Village { get; set; }
        ///<summary>
        ///บ้านเลขที่
        ///</summary>
        public string Atty_HouseNo { get; set; }
        ///<summary>
        ///หมู่ที่
        ///</summary>
        public string Atty_Moo { get; set; }
        ///<summary>
        ///ซอย
        ///</summary>
        public string Atty_Soi { get; set; }
        ///<summary>
        ///ถนน
        ///</summary>
        public string Atty_Road { get; set; }
        ///<summary>
        ///ตำบล
        ///</summary>
        public string Atty_Subdistrict { get; set; }
        ///<summary>
        ///อำเภอ
        ///</summary>
        public string Atty_District { get; set; }
        ///<summary>
        ///จังหวัด
        ///</summary>
        public string Atty_Province { get; set; }


        public static AttorneyTransferDTO CreateFromModel(AttorneyTransfer model)
        {
            if (model != null)
            {
                var result = new AttorneyTransferDTO()
                {
                    Id = model.ID,
                    Seqn_No = model.Seqn_No,
                    Atty_FullName = model.Atty_FullName,
                    Atty_EmployeeCode = model.Atty_EmployeeCode,
                    Atty_DateOfBirth = model.Atty_DateOfBirth,
                    Atty_Ages = model.Atty_Ages,
                    Atty_Nationality = model.Atty_Nationality,
                    Atty_Parent = model.Atty_Parent,
                    Atty_Address = model.Atty_Address,
                    Atty_District = model.Atty_District,
                    Atty_HouseNo = model.Atty_HouseNo,
                    Atty_Moo = model.Atty_Moo,
                    Atty_Province = model.Atty_Province,
                    Atty_Road = model.Atty_Road,
                    Atty_Soi = model.Atty_Soi,
                    Atty_Subdistrict = model.Atty_Subdistrict,
                    Atty_Village = model.Atty_Village
                };
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
