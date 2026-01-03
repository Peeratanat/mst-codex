using Database.Models.MasterKeys;
using Database.Models.MST;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using models = Database.Models;
namespace Base.DTOs.MST
{
    public class GLAccountTypeDTO : BaseDTO
    {
        public int Order { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        public string Remark { get; set; }

        public Guid? PostGLFormatTextFileHeaderID { get; set; }

        public bool IsActive { get; set; }

        public static GLAccountTypeDTO CreateFromModel(GLAccountType model)
        {
            if (model != null)
            {
                var result = new GLAccountTypeDTO()
                {
                    Id = model.ID,
                    Name = model.Name,
                    Key = model.Key,
                    Order = model.Order,
                    Remark = model.Remark,
                    PostGLFormatTextFileHeaderID = model.PostGLFormatTextFileHeaderID,
                    IsActive = model.IsActive
                };
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
