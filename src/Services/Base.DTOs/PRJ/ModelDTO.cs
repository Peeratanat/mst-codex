using Database.Models;
using Database.Models.MST;
using Database.Models.PRJ;
using ErrorHandling;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Base.DTOs.PRJ
{
    public class ModelDTO : BaseDTO
    {
        /// <summary>
        ///  รหัสแบบบ้าน
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        ///  ชื่อแบบบ้าน (TH)
        /// </summary>
        [Description("ชื่อแบบบ้าน (TH)")]
        public string NameTH { get; set; }
        /// <summary>
        ///  ชื่อแบบบ้าน (EN)
        /// </summary>
        [Description("ชื่อแบบบ้าน (EN)")]
        public string NameEN { get; set; }
        /// <summary>
        ///  ชื่อย่อ
        ///  Master/api/MasterCenters?masterCenterGroupKey=ModelShortName
        /// </summary>
        public MST.MasterCenterDropdownDTO ModelShortName { get; set; }
        /// <summary>
        ///  ลักษณะแบบบ้าน
        ///  Master/api/MasterCenters?masterCenterGroupKey=ModelUnitType
        /// </summary>
        public MST.MasterCenterDropdownDTO ModelUnitType { get; set; }
        /// <summary>
        ///  ประเภทแบบบ้าน
        ///  Master/api/TypeOfRealEstates//DropdownList
        /// </summary>
        public MST.TypeOfRealEstateDropdownDTO TypeOfRealEstate { get; set; }
        /// <summary>
        ///  ModelType
        ///  Master/api/MasterCenters?masterCenterGroupKey=ModelType
        /// </summary>
        public MST.MasterCenterDropdownDTO ModelType { get; set; }
        /// <summary>
        ///  หน้ากว้าง
        /// </summary>
        public double? FrontWidth { get; set; }
        /// <summary>
        ///  อัตราชำระคืน
        /// </summary>
        public double? PreferUnit { get; set; }
        /// <summary>
        ///  อัตราชำระคืนต่อพื้นที่
        /// </summary>
        public double? PreferUnitMinimum { get; set; }
        /// <summary>
        ///  จำนวนชำระคืนต่อหน่วย
        /// </summary>
        public double? PreferHouse { get; set; }
        /// <summary>
        /// WaterElectricMeterPrices
        /// </summary>
        public List<WaterElectricMeterPriceDTO> WaterElectricMeterPrices { get; set; }

        /// <summary>
        /// นำไปขายแล้ว
        /// </summary>
        public bool? IsSell { get; set; }

        public int? Bedroom { get; set; }
        public int? Bathroom { get; set; }
        public int? ParkingSpace { get; set; }

    public static ModelDTO CreateFromModel(Model model, List<WaterElectricMeterPriceDTO> waterElectricMeterPrices=null , bool bookingChk = false)
        {
            if (model != null)
            {
                var result = new ModelDTO()
                {
                    Id = model.ID,
                    Code = model.Code,
                    NameTH = model.NameTH,
                    NameEN = model.NameEN,
                    ModelShortName = MST.MasterCenterDropdownDTO.CreateFromModel(model.ModelShortName),
                    ModelUnitType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ModelUnitType),
                    ModelType = MST.MasterCenterDropdownDTO.CreateFromModel(model.ModelType),
                    WaterElectricMeterPrices = waterElectricMeterPrices,
                    TypeOfRealEstate = MST.TypeOfRealEstateDropdownDTO.CreateFromModel(model.TypeOfRealEstate),
                    FrontWidth = model.FrontWidth,
                    PreferUnit = model.PreferUnit,
                    PreferUnitMinimum = model.PreferUnitMinimum,
                    PreferHouse= model.PreferHouse,
                    IsSell = bookingChk,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName,
                    Bedroom = model.Bedroom,
                    Bathroom = model.Bathroom,
                    ParkingSpace = model.ParkingSpace
                };
                return result;
            }
            else
            {
                return null;
            }
        }
        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            if (!string.IsNullOrEmpty(this.NameEN))
            {
                if (!this.NameEN.CheckLang(false, true, true,false))
                {
                    var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0003").FirstAsync();
                    string desc = this.GetType().GetProperty(nameof(ModelDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }   
            if (ex.HasError)
            {
                throw ex;
            }
        }
        public void ToModel(ref Model model)
        {
            model.Code = this.Code;
            model.NameTH = this.NameTH;
            model.NameEN = this.NameEN;
            model.ModelShortNameMasterCenterID = this.ModelShortName?.Id;
            model.ModelUnitTypeMasterCenterID = this.ModelUnitType?.Id;
            model.ModelTypeMasterCenterID = this.ModelType?.Id;
            model.TypeOfRealEstateID = this.TypeOfRealEstate?.Id;
            model.PreferUnit = this.PreferUnit;
            model.PreferUnitMinimum = this.PreferUnitMinimum;
            model.PreferHouse = this.PreferHouse;
            model.FrontWidth = this.FrontWidth;
            model.Bedroom = this.Bedroom;
            model.Bathroom = this.Bathroom;
            model.ParkingSpace = this.ParkingSpace;
        }

    }
    public class ModelInput
    {
        public List<ModelDTO> Items { get; set; }

        public List<Guid> ModelId { get; set; }

        ModelInput()
        {
            Items = new List<ModelDTO>();
            ModelId = new List<Guid>();
        }
    }
}
