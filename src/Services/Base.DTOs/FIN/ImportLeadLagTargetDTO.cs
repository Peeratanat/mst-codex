using System.Collections.Generic;

namespace Base.DTOs.FIN
{
    public class ImportLeadLagTargetDTO : BaseDTO
    {
        public int Total { get; set; }  
        public int Valid { get; set; }  
        public int Invalid { get; set; }  
        public List<ImportLeadLagTargetListDTO> ImportLeadLagTargetList { get; set; }
       
    }
     
}
