using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class AddressDTO
    {
        /// <summary>
        /// ที่อยู่ที่ติดต่อได้
        /// </summary>
        public List<ContactAddressDTO> ContactAddress { get; set; }
        /// <summary>
        /// ที่อยู่ตามบัตรประชาชน
        /// </summary>
        public ContactAddressDTO CitizenAddress { get; set; }
        /// <summary>
        /// ที่อยู่ตามทะเบียนบ้าน
        /// </summary>
        public ContactAddressDTO HomeAddress { get; set; }
        /// <summary>
        /// ที่อยู่ที่ทำงาน
        /// </summary>
        public ContactAddressDTO WorkAddress { get; set; }

        public async static Task<AddressDTO> CreateFromModelAsync(List<models.CTM.ContactAddress> model, models.DatabaseContext DB)
        {
            var master = await DB.MasterCenters.Where(m => m.MasterCenterGroupKey == "ContactAddressType").ToListAsync();
            var contactMasterID = master.Where(x => x.Key == "0").Select(x => x.ID).First();
            var citizenMasterID = master.Where(x => x.Key == "1").Select(x => x.ID).First();
            var workMasterID = master.Where(x => x.Key == "3").Select(x => x.ID).First();
            var homeMasterID = master.Where(x => x.Key == "2").Select(x => x.ID).First();

            var citizenModel = model.Where(o => o.ContactAddressTypeMasterCenterID == citizenMasterID).FirstOrDefault();
            var workModel = model.Where(o => o.ContactAddressTypeMasterCenterID == workMasterID).FirstOrDefault();
            var homeModel = model.Where(o => o.ContactAddressTypeMasterCenterID == homeMasterID).FirstOrDefault();
            var contactModel = model.Where(o => o.ContactAddressTypeMasterCenterID == contactMasterID).ToList();

            var result = new AddressDTO();
            result.CitizenAddress = await ContactAddressDTO.CreateFromModelAsync(citizenModel, DB);
            result.HomeAddress = await ContactAddressDTO.CreateFromModelAsync(homeModel, DB);
            result.WorkAddress = await ContactAddressDTO.CreateFromModelAsync(workModel, DB);
            result.ContactAddress = contactModel.Select(o => ContactAddressDTO.CreateFromModelAsync(o, DB)).Select(x => x.Result).ToList();

            return result;
        }
    }
}
