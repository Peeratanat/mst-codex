using Base.DTOs.MST;
using Common.Helper.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MST_General.Services
{
    public interface IGLAccountTypeService : BaseInterfaceService
    {
        Task<List<GLAccountTypeDropdownDTO>> GetGLAccountTypeDropdownListAsync(string key, string name);

    }
}
