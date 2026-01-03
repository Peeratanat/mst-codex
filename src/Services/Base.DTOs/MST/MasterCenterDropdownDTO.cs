using Database.Models.MasterKeys;
using Database.Models.MST;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using models = Database.Models;
namespace Base.DTOs.MST
{
    public class MasterCenterDropdownDTO
    {
        public Guid Id { get; set; }
        /// <summary>
        /// ชื่อ ข้อมูลพื้นฐานทั่วไป
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// ชื่อ ข้อมูลพื้นฐานทั่วไป Eng
        /// </summary>
        public string NameEN { get; set; }
        /// <summary>
        /// ชื่อ ข้อมูลพื้นฐานทั่วไป FullName
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// รหัส ข้อมูลพื้นฐานทั่วไป
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// ลำดับ
        /// </summary>
        public int Order { get; set; }

        public static MasterCenterDropdownDTO CreateFromModel(MasterCenter model)
        {
            if (model != null)
            {
                var result = new MasterCenterDropdownDTO()
                {
                    Id = model.ID,
                    Name = model.Name,
                    NameEN = model.NameEN,
                    FullName = model.Name + ((!string.IsNullOrEmpty(model.NameEN)) ? "/" + model.NameEN : ""),
                    Key = model.Key,
                    Order = model.Order
                };
                return result;
            }
            else
            {
                return null;
            }
        }
        public static MasterCenterDropdownDTO CreateMasterLeadTypeFromModel(MasterCenter model)
        {
            if (model != null)
            {
                var result = new MasterCenterDropdownDTO()
                {
                    Id = model.ID,
                    Name = model.NameEN,
                    NameEN = model.NameEN,
                    FullName = model.NameEN,
                    Key = model.Key,
                    Order = model.Order
                };
                return result;
            }
            else
            {
                return null;
            }
        }
        public static MasterCenterDropdownDTO CreateForQuestionnairFromModel(MasterCenter model, models.DatabaseContext DB)
        {
            if (model != null)
            {
                string nameReuslt = string.Empty;
                if (model.Order == 3) //X/Y (X=คำถามที่ตอบแล้ว, Y=คำถามทั้งหมด)
                {

                    nameReuslt = model.Name;
                }
                else
                {
                    nameReuslt = model.Name;
                }

                var result = new MasterCenterDropdownDTO()
                {
                    Id = model.ID,
                    Name = nameReuslt,
                    NameEN = model.NameEN,
                    FullName = model.Name + ((!string.IsNullOrEmpty(model.NameEN)) ? "/" + model.NameEN : ""),
                    Key = model.Key,
                    Order = model.Order
                };

                return result;
            }
            else
            {
                return null;
            }
        }

        public static MasterCenterDropdownDTO CreateStatusQuestionaireCaseEmptyFromModel(models.DatabaseContext DB)
        {
            MasterCenter rs = DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.StatusQuestionaire && o.Key == "1").First();
            if (rs != null)
            {
                var result = new MasterCenterDropdownDTO()
                {
                    Id = rs.ID,
                    Name = rs.Name,
                    Key = rs.Key,
                    Order = rs.Order
                };
                return result;
            }
            else
            {
                return null;
            }
        }
        public static MasterCenterDropdownDTO CreateStatusQuestionaireCaseSelectFromDBLink(models.DatabaseContext DB, string Key)
        {
            MasterCenter rs = DB.MasterCenters.Where(o => o.MasterCenterGroupKey == MasterCenterGroupKeys.StatusQuestionaire && o.Key == Key).First();
            if (rs != null)
            {
                var result = new MasterCenterDropdownDTO()
                {
                    Id = rs.ID,
                    Name = rs.Name,
                    Key = rs.Key,
                    Order = rs.Order
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
