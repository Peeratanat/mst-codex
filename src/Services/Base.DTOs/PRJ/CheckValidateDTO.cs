using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class CheckValidateDTO
    {
        public bool? checkProjectInfo { get; set; }
        public bool? checkAgreement { get; set; }
        public bool? checkModel { get; set; }
        public bool? checkTower { get; set; }
        public bool? checkUnit { get; set; }
        public bool? checkTitleDeedDetail { get; set; }
        public bool? checkProjectImage { get; set; }
        public bool? checkMinPrice { get; set; }
        public bool? checkFee { get; set; }
        public bool? checkBudgetPromotion { get; set; }
        public bool? checkWaiveQC { get; set; }
    }
}
