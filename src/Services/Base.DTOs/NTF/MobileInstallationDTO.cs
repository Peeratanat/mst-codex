using System;
using Base.DTOs.USR;
using Database.Models.NTF;

namespace Base.DTOs.NTF
{
    public class MobileInstallationDTO
    {
        public Guid Id { get; set; }
        public UserListDTO User { get; set; }
        public string InstallationID { get; set; }
        public MobileDeviceType DeviceType { get; set; }
        public DateTime? Updated { get; set; }

        public static MobileInstallationDTO CreateFromModel(MobileInstallation model)
        {
            if (model != null)
            {
                var result = new MobileInstallationDTO()
                {
                    Id = model.ID,
                    InstallationID = model.InstallationID,
                    DeviceType = model.DeviceType,
                    Updated = model.Updated
                };
                result.User = UserListDTO.CreateFromModel(model.User);

                return result;
            }
            else
            {
                return null;
            }
        }

        public void ToModel(ref MobileInstallation model)
        {
            model.UserID = this.User.Id;
            model.InstallationID = this.InstallationID;
            model.DeviceType = this.DeviceType;
        }

    }
}
