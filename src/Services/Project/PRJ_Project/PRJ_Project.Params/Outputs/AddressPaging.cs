using Base.DTOs.PRJ;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PRJ_Project.Params.Outputs
{
    public class AddressPaging
    {
        public List<ProjectAddressListDTO> ProjectAddresses { get; set; }
        public PageOutput PageOutput { get; set; }
    }
}
