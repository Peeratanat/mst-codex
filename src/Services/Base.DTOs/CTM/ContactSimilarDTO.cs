using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models.MasterKeys;
using Microsoft.EntityFrameworkCore;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class ContactSimilarDTO
    {
        /// <summary>
        /// ข้อมูล Contact
        /// </summary>
        public ContactListDTO Contact { get; set; }
        /// <summary>
        /// ที่อยู่
        /// </summary>
        public ContactAddressDTO Address { get; set; }

        public async static Task<ContactSimilarDTO> CreateFromModelAsync(models.CTM.Contact model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                var result = new ContactSimilarDTO();
                result.Contact = ContactListDTO.CreateFromModel(model, DB);

                var master = await DB.MasterCenters.Where(m => m.MasterCenterGroupKey == MasterCenterGroupKeys.ContactAddressType).ToListAsync();
                var contactMasterID = master.Where(x => x.Key == ContactAddressTypeKeys.Contact).Select(x => x.ID).First();
                var addressModel = await DB.ContactAddresses
                    .Include(o => o.Country)
                    .Include(o => o.Province)
                    .Include(o => o.District)
                    .Include(o => o.SubDistrict)
                    .Include(o => o.ContactAddressType)
                    .Where(c => c.ContactID == model.ID && c.ContactAddressTypeMasterCenterID == contactMasterID)
                    .FirstOrDefaultAsync();

                result.Address = await ContactAddressDTO.CreateFromModelAsync(addressModel, DB);

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
