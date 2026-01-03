using System.Collections.Generic;

namespace Base.DTOs.PRM
{
    public class ImportMappingAgreementDTO 
    {
        public List<MappingAgreementDTO> MappingAgreementList { get; set; }
        public int TotalSuccess { get; set; } 
        public int TotalError { get; set; }
    } 
}
