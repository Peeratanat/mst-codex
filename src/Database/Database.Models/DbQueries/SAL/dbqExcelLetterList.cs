using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models.DbQueries.SAL
{
    public class dbqExcelLetterList
    {
        public string DisplayName { get; set; }
        public string ProjectNo { get; set; }
        public string PostTrackingNo { get; set; }
        public int? PostTrackingOrder { get; set; }
        public string CountryNameTH { get; set; }
        public string CountryCode { get; set; }
        public string PostalCode { get; set; }
        public string UnitNo { get; set; }
        public DateTime? LetterDate { get; set; }
        //public string LetterNo { get; set; }
    }
}
