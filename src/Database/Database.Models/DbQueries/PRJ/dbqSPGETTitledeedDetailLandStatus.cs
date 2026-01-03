using Base.DbQueries;
using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text;

namespace Database.Models.DbQueries.PRJ
{
    public class dbqSPGETTitledeedDetailLandStatus
    {
        public Guid ID { get; set; }
        
        public string Name { get; set; }
        public string Key { get; set; }
        public bool IsActive { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public bool IsDeleted { get; set; }
        public int Order { get; set; }
        public string MasterCenterGroupKey { get; set; }
        public string NameEN { get; set; }



        
        

        
        
        
        








    }
}
