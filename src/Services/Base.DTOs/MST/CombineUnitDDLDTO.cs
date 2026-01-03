using Base.DTOs.PRJ;
using Base.DTOs.SAL.Sortings;
using Base.DTOs.SAL;
using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.USR;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using models = Database.Models;
using Base.DTOs.USR;
using System.Reactive;
using Database.Models.MasterKeys;
using static Database.Models.DbQueries.DBQueryParam;
using Database.Models.DbQueries;
using PagingExtensions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.Data.SqlClient;

namespace Base.DTOs.MST
{
    public class CombineUnitDDLDTO : BaseDTO
    {
        public List<CombineUnitDTO> input { get; set; }
        public string txt { get; set; }
        public Guid? ProjectID { get; set; }
        public Guid? UnitID { get; set; }

    }
}
