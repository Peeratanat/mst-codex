using System;
using System.Collections.Generic;
using System.Text;

namespace PRJ_Unit.Services.Sap
{
    public class UnitSap
    {
        /// <summary>
        /// WBS Element
        /// </summary>
        public string PSPNR { get; set; }
        /// <summary>
        /// Relative day in period
        /// </summary>
        public string POSID { get; set; }
        /// <summary>
        /// Current number of the appropriate project
        /// </summary>
        public string PSPHI { get; set; }
        /// <summary>
        /// Date on Which Record Was Created
        /// </summary>
        public string ERDAT { get; set; }
        /// <summary>
        /// Changed On
        /// </summary>
        public string AEDAT { get; set; }
        /// <summary>
        /// Insurance Number (Obsolete with ZVMP)
        /// </summary>
        public string VERNR { get; set; }
        /// <summary>
        /// Name of responsible person (Project manager)
        /// </summary>
        public string VERNA { get; set; }
        /// <summary>
        /// Applicant number
        /// </summary>
        public string ASTNR { get; set; }
        /// <summary>
        /// Applicant
        /// </summary>
        public string ASTNA { get; set; }
        /// <summary>
        /// Partner Company Code
        /// </summary>
        public string PBUKR { get; set; }
        /// <summary>
        /// Profit Center
        /// </summary>
        public string PRCTR { get; set; }
        /// <summary>
        /// Percent type
        /// </summary>
        public string PRART { get; set; }
        /// <summary>
        /// Registered office postal code
        /// </summary>
        public string STUFE { get; set; }
        /// <summary>
        /// Increase or purge next screen memory
        /// </summary>
        public string POST1 { get; set; }
        /// <summary>
        /// Object number (forecast) กิ่ง P
        /// </summary>
        public string OBJNR { get; set; }
        /// <summary>
        /// Number of the 1. subordinate WBS element
        /// </summary>
        public string DOWN { get; set; }
        /// <summary>
        /// Plant
        /// </summary>
        public string WERKS { get; set; }
        /// <summary>
        /// 2nd user field 20 digits - WBS element
        /// </summary>
        public string HMTYP { get; set; }
        /// <summary>
        /// 3rd user-defined field 10 digits -WBS element
        /// </summary>
        public string CRMID { get; set; }
        /// <summary>
        /// WBS Promotion
        /// </summary>
        public string WBS { get; set; }
        /// <summary>
        /// WBS R Object
        /// </summary>
        public string WBSR { get; set; }
        /// <summary>
        /// Object number (forecast) กิ่ง R
        /// </summary>
        public string ROBJ { get; set; }
    }
}
