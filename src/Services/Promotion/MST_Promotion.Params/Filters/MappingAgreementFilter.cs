using System;
using Database.Models.PRM;

namespace MST_Promotion.Params.Filters
{
    public class MappingAgreementFilter : BaseFilter
    {
        /// <summary>
        /// OldAgreement
        /// </summary> 
        public string OldAgreement { get; set; }
        /// <summary>
        /// OldItem
        /// </summary>
        public string OldItem { get; set; }
        /// <summary>
        /// NewAgreement
        /// </summary> 
        public string NewAgreement { get; set; }
        /// <summary>
        /// NewItem
        /// </summary>
        public string NewItem { get; set; }
        /// <summary>
        /// Material Code Old And New
        /// </summary> 
        public string MaterialCode { get; set; }
        /// <summary>
        /// Material Name Old And New
        /// </summary> 
        public string MaterialName { get; set; }
        /// <summary>
        /// Material Type Old And New
        /// </summary> 
        public string MaterialType { get; set; }
        /// <summary>
        /// Create
        /// </summary> 
        public string Create { get; set; }
        /// <summary>
        /// CreateDate
        /// </summary> 
        public DateTime? CreatedFrom { get; set; }
        public DateTime? CreatedTo { get; set; }
    }
}
