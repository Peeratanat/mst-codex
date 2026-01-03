using Database.Models;
using Database.Models.DbQueries.PRM;
using Database.Models.PRM;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace Base.DTOs.PRM
{
    public class MappingAgreementDTO : BaseDTO
    {
        /// <summary>
        /// Running Number
        /// </summary>
        public int No { get; set; }
        /// <summary>
        /// สถานะความถูกต้องชองข้อมูล
        /// ถ้ามี Field ครบ ถือว่าถูกต้อง (ยกเว้น Remark)
        /// </summary>
        public bool IsValidData { get; set; }
        /// <summary>
        /// เลขที่ Agreement เดิม
        /// </summary>
        public string OldAgreement { get; set; }
        /// <summary>
        /// เลขที่ Item เดิม
        /// </summary>
        public string OldItem { get; set; }
        /// <summary>
        /// เลขที่ Material Code เดิม
        /// </summary>
        public string OldMaterialCode { get; set; }
        /// <summary>
        /// Material Name เดิม
        /// </summary>
        public string OldMaterialName { get; set; }
        /// <summary>
        /// Material Type เดิม
        /// </summary>
        public string OldMaterialType { get; set; }
        /// <summary>
        /// เลขที่ Agreement ใหม่
        /// </summary>
        public string NewAgreement { get; set; }
        /// <summary>
        /// เลขที่ Item ใหม่
        /// </summary>
        public string NewItem { get; set; }
        /// <summary>
        /// เลขที่ Material Code ใหม่
        /// </summary>
        public string NewMaterialCode { get; set; }
        /// <summary>
        /// หมายเหตุ
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// Material Name ใหม่
        /// </summary>
        public string NewMaterialName { get; set; }
        /// <summary>
        /// Material Type ใหม่
        /// </summary>
        public string NewMaterialType { get; set; }
        public string CreateBy { get; set; }
        public DateTime? Created { get; set; }

        public string MsgError { get; set; }

        public static MappingAgreementDTO CreateFromModel(MappingAgreement model)
        {
            if (model != null)
            {
                var result = new MappingAgreementDTO()
                {
                    Id = model.ID,
                    OldAgreement = model.OldAgreement,
                    OldItem = model.OldItem,
                    OldMaterialCode = model.OldMaterialCode,
                    NewAgreement = model.NewAgreement,
                    NewItem = model.NewItem,
                    NewMaterialCode = model.NewMaterialCode,
                    Remark = model.Remark,
                    Updated = model.Updated,
                    UpdatedBy = model.UpdatedBy?.DisplayName
                };

                return result;
            }
            else
            {
                return null;
            }
        }
        public static MappingAgreementDTO CreateFromQuery(dbqMappingAgreement model)
        {
            if (model != null)
            {
                var result = new MappingAgreementDTO()
                {
                    Id = model.MaID,
                    OldAgreement = model.OldAgreement,
                    OldItem = model.OldItem,
                    OldMaterialCode = model.OldMaterialCode,
                    OldMaterialName = model.OldName,
                    OldMaterialType= model.OldType,
                    NewAgreement = model.NewAgreement,
                    NewItem = model.NewItem,
                    NewMaterialCode = model.NewMaterialCode, 
                    NewMaterialName = model.NewName, 
                    NewMaterialType = model.NewType, 
                    CreateBy = model.CreateBy,
                    Created = model.Created,
                    Remark = model.Remark
                };

                return result;
            }
            else
            {
                return null;
            }
        }
        public void ToModel(ref MappingAgreement model)
        {
            model.OldAgreement = this.OldAgreement;
            model.OldItem = this.OldItem;
            model.OldMaterialCode = this.OldMaterialCode;
            model.NewAgreement = this.NewAgreement;
            model.NewItem = this.NewItem;
            model.NewMaterialCode = this.NewMaterialCode;
            model.Remark = this.Remark;
        }
        public static void SortBy(MappingAgreementSortByParam sortByParam, ref IQueryable<MappingAgreementQueryResult> query)
        {
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case MappingAgreementSortBy.OldAgreement:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MappingAgreements.OldAgreement).ThenByDescending(x => x.MappingAgreements.Created);
                        else query = query.OrderByDescending(o => o.MappingAgreements.OldAgreement).ThenByDescending(x => x.MappingAgreements.Created);
                        break;
                    case MappingAgreementSortBy.OldItem:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MappingAgreements.OldItem).ThenByDescending(x => x.MappingAgreements.Created);
                        else query = query.OrderByDescending(o => o.MappingAgreements.OldItem).ThenByDescending(x => x.MappingAgreements.Created);
                        break;
                    case MappingAgreementSortBy.OldMaterialCode:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MappingAgreements.OldMaterialCode).ThenByDescending(x => x.MappingAgreements.Created);
                        else query = query.OrderByDescending(o => o.MappingAgreements.OldMaterialCode).ThenByDescending(x => x.MappingAgreements.Created);
                        break;
                    case MappingAgreementSortBy.NewAgreement:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MappingAgreements.NewAgreement).ThenByDescending(x => x.MappingAgreements.Created);
                        else query = query.OrderByDescending(o => o.MappingAgreements.NewAgreement).ThenByDescending(x => x.MappingAgreements.Created);
                        break;
                    case MappingAgreementSortBy.NewItem:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MappingAgreements.NewItem).ThenByDescending(x => x.MappingAgreements.Created);
                        else query = query.OrderByDescending(o => o.MappingAgreements.NewItem).ThenByDescending(x => x.MappingAgreements.Created);
                        break;
                    case MappingAgreementSortBy.NewMaterialCode:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MappingAgreements.NewMaterialCode).ThenByDescending(x => x.MappingAgreements.Created);
                        else query = query.OrderByDescending(o => o.MappingAgreements.NewMaterialCode).ThenByDescending(x => x.MappingAgreements.Created);
                        break;
                    case MappingAgreementSortBy.Created:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MappingAgreements.Created);
                        else query = query.OrderByDescending(o => o.MappingAgreements.Created);
                        break;
                    case MappingAgreementSortBy.CreateBy:
                        if (sortByParam.Ascending) query = query.OrderBy(o => o.MappingAgreements.CreatedBy.DisplayName).ThenByDescending(x => x.MappingAgreements.Created);
                        else query = query.OrderByDescending(o => o.MappingAgreements.CreatedBy.DisplayName).ThenByDescending(x => x.MappingAgreements.Created);
                        break;
                    default:
                        query = query.OrderByDescending(o => o.MappingAgreements.Created);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(o => o.MappingAgreements.Created);
            } 
        }
        public static void QueryOrder(MappingAgreementSortByParam sortByParam, ref string query)
        {
            query += @" order by ";
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case MappingAgreementSortBy.OldAgreement:
                        query += @" a.OldAgreement ";
                        break;
                    case MappingAgreementSortBy.OldItem:
                        query += @" a.OldItem ";
                        break;
                    case MappingAgreementSortBy.OldMaterialCode:
                        query += @" a.OldMaterialCode ";
                        break;
                    case MappingAgreementSortBy.NewAgreement:
                        query += @" a.NewAgreement ";
                        break;
                    case MappingAgreementSortBy.NewItem:
                        query += @" a.NewItem ";
                        break;
                    case MappingAgreementSortBy.NewMaterialCode:
                        query += @" a.NewMaterialCode ";
                        break;
                    case MappingAgreementSortBy.Created:
                        query += @" a.Created ";
                        break;
                    case MappingAgreementSortBy.CreateBy:
                        query += @" u.DisplayName ";
                        break;
                    default: 
                        break;
                }
                query += sortByParam.Ascending ? "asc ,": "desc ,"; 
            } 
            query += @" a.Created desc ";
        }
        public async Task ValidateAsync(DatabaseContext db)
        {
            ValidateException ex = new ValidateException();
            this.OldItem = this.OldItem.PadLeft(5, '0');
            this.NewItem = this.NewItem.PadLeft(5, '0');
            if (this.NewMaterialCode.IndexOf("-") <= 0)
                this.NewMaterialCode = this.NewMaterialCode.PadLeft(18, '0');
            if (this.OldMaterialCode.IndexOf("-") <= 0)
                this.OldMaterialCode = this.OldMaterialCode.PadLeft(18, '0');

            var oldAg = await db.MappingAgreements.Where(x => x.NewAgreement == this.NewAgreement
            && x.NewItem == this.NewItem
            && x.NewMaterialCode == this.NewMaterialCode
            && x.OldAgreement == this.OldAgreement
            && x.OldItem == this.OldItem
            && x.OldMaterialCode == this.OldMaterialCode
            ).FirstOrDefaultAsync();

            var materialGroupKey_Old = await db.PromotionMaterials.Where(o => o.Code == this.OldMaterialCode).OrderByDescending(o => o.Created).FirstOrDefaultAsync();
            var materialGroupKey_New = await db.PromotionMaterials.Where(o => o.Code == this.NewMaterialCode).OrderByDescending(o => o.Created).FirstOrDefaultAsync();

            if (oldAg != null)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync(); 
                var msg = "พบข้อมูล MappingAgreement ในระบบแล้ว กรุณาระบุด้วยข้อมูลใหม่";
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
            }
            if (this.OldAgreement == this.NewAgreement
                        && this.OldItem == this.NewItem
                        )
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
                var msg = "ข้อมูล Old Agreement,Old Item ซ้ำกับ New Agreement,New Item";
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type); 
            }
            if (this.OldMaterialCode.Length != this.NewMaterialCode.Length)
            {
                var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
                var msg = "Old MaterialCode ไม่เท่ากับ New MaterialCode";
                ex.AddError(errMsg.Key, msg, (int)errMsg.Type); 
            }

            if (this.OldMaterialCode.Length == this.NewMaterialCode.Length)
            {
                if (materialGroupKey_Old != null && materialGroupKey_New != null)
                {
                    if (materialGroupKey_Old.MaterialGroupKey != materialGroupKey_New.MaterialGroupKey)
                    {
                        var errMsg = await db.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
                        var msg = "Old MaterialCode :" + materialGroupKey_Old.MaterialGroupKey + " และ New MaterialCode : " + materialGroupKey_New.MaterialGroupKey + " ไม่สามารถ Mapping Agreement ได้ เนื่องจาก MaterialCode ไม่ตรงกัน";
                        ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                    }
                }
            }

            if (ex.HasError)
            {
                throw ex;
            }
        }

    }
    public class MappingAgreementQueryResult
    {
        public MappingAgreement MappingAgreements { get; set; }
        public PromotionMaterial PromotionMaterialsOld { get; set; }
        public PromotionMaterial PromotionMaterialsNew { get; set; }
    }
}
