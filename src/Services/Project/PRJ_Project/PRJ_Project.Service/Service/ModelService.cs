using Base.DTOs;
using Base.DTOs.PRJ;
using Common.Helper.Logging;
using Database.Models;
using Database.Models.CMS;
using Database.Models.MST;
using Database.Models.PRJ;
using ErrorHandling;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PagingExtensions;
using PRJ_Project.Params.Filters;
using PRJ_Project.Params.Outputs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Base.DTOs.ParamMail;

namespace PRJ_Project.Services
{
    public class ModelService : IModelService
    {
        public LogModel logModel { get; set; }
        private readonly DatabaseContext DB;

        public ModelService(DatabaseContext db)
        {
            logModel = new LogModel("ModelService", null);
            this.DB = db;
        }

        public async Task<List<ModelDropdownDTO>> GetModelDropdownListAsync(Guid? projectID = null, string name = null, CancellationToken cancellationToken = default)
        {
            var query = DB.Models.AsNoTracking();
            if (projectID != null)
            {
                query = query.Where(x => x.ProjectID == projectID);
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(o => o.NameTH.Contains(name));
            }

            var results = await query.OrderBy(o => o.NameTH).Take(100).Select(o => ModelDropdownDTO.CreateFromModel(o)).ToListAsync(cancellationToken);

            return results;
        }

        public async Task<ModelPaging> GetModelListAsync(Guid projectID, ModelsFilter filter, PageParam pageParam, ModelListSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<ModelQueryResult> query = DB.Models.AsNoTracking().Where(o => o.ProjectID == projectID).Select(o => new ModelQueryResult
            {
                Model = o,
                ModelShortName = o.ModelShortName,
                ModelType = o.ModelType,
                ModelUnitType = o.ModelUnitType,
                TypeOfRealEstate = o.TypeOfRealEstate,
                WaterElectricMeterPrice = DB.WaterElectricMeterPrices.Where(b => b.ModelID == o.ID).OrderByDescending(b => b.Version).FirstOrDefault(),
                UpdatedBy = o.UpdatedBy
            });

            var unitUsedModels = await DB.Units.Where(o => o.ProjectID == projectID).Select(o => o.ModelID).ToListAsync(cancellationToken);

            #region Filter
            if (!string.IsNullOrEmpty(filter.Code))
            {
                query = query.Where(x => x.Model.Code.Contains(filter.Code));
            }
            if (!string.IsNullOrEmpty(filter.NameTH))
            {
                query = query.Where(x => x.Model.NameTH.Contains(filter.NameTH));
            }
            if (!string.IsNullOrEmpty(filter.NameEN))
            {
                query = query.Where(x => x.Model.NameEN.Contains(filter.NameEN));
            }
            if (filter.TypeOfRealEstateID != null && filter.TypeOfRealEstateID != Guid.Empty)
            {
                query = query.Where(x => x.Model.TypeOfRealEstateID == filter.TypeOfRealEstateID);
            }
            if (!string.IsNullOrEmpty(filter.ModelShortNameKey))
            {
                var modelShortNameKeyID = (await DB.MasterCenters.FirstAsync(x => x.Key == filter.ModelShortNameKey
                                                                 && x.MasterCenterGroupKey == "ModelShortName")
                                                                ).ID;
                query = query.Where(x => x.Model.ModelShortNameMasterCenterID == modelShortNameKeyID);
            }
            if (!string.IsNullOrEmpty(filter.ModelTypeKey))
            {
                var modelTypeID = (await DB.MasterCenters.FirstAsync(x => x.Key == filter.ModelTypeKey
                                                                && x.MasterCenterGroupKey == "ModelType")
                                                               ).ID;
                query = query.Where(x => x.Model.ModelTypeMasterCenterID == modelTypeID);
            }
            if (!string.IsNullOrEmpty(filter.ModelUnitTypeKey))
            {
                var modelUnitTypeID = (await DB.MasterCenters.FirstAsync(x => x.Key == filter.ModelUnitTypeKey
                                                                 && x.MasterCenterGroupKey == "ModelUnitType")
                                                                ).ID;
                query = query.Where(x => x.Model.ModelUnitTypeMasterCenterID == modelUnitTypeID);
            }
            if (filter.ElectricMeterPriceFrom != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.ElectricMeterPrice >= filter.ElectricMeterPriceFrom);
            }
            if (filter.ElectricMeterPriceTo != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.ElectricMeterPrice <= filter.ElectricMeterPriceTo);
            }
            if (filter.ElectricMeterPriceFrom != null && filter.ElectricMeterPriceTo != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.ElectricMeterPrice >= filter.ElectricMeterPriceFrom
                                   && x.WaterElectricMeterPrice.ElectricMeterPrice <= filter.ElectricMeterPriceTo);
            }
            if (filter.WaterMeterPriceFrom != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.WaterMeterPrice >= filter.WaterMeterPriceFrom);
            }
            if (filter.WaterMeterPriceTo != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.WaterMeterPrice <= filter.WaterMeterPriceTo);
            }
            if (filter.WaterMeterPriceFrom != null && filter.WaterMeterPriceTo != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.WaterMeterPrice >= filter.WaterMeterPriceFrom
                                   && x.WaterElectricMeterPrice.WaterMeterPrice <= filter.WaterMeterPriceTo);
            }

            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.Model.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Model.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Model.Updated >= filter.UpdatedFrom && x.Model.Updated <= filter.UpdatedTo);
            }
            #endregion

            ModelListDTO.SortBy(sortByParam, ref query);

            var pageOutput = PagingHelper.Paging<ModelQueryResult>(pageParam, ref query);

            var results = await query.Select(o => ModelListDTO.CreateModelChkUsedFromQueryResult(o, unitUsedModels)).ToListAsync(cancellationToken);

            return new ModelPaging()
            {
                PageOutput = pageOutput,
                Models = results
            };
        }

        public async Task<ModelPaging> GetModelListAllAsync(Guid projectID, ModelsFilter filter, PageParam pageParam, ModelListSortByParam sortByParam, CancellationToken cancellationToken = default)
        {
            IQueryable<ModelQueryResult> query = DB.Models.AsNoTracking().Where(o => o.ProjectID == projectID).Select(o => new ModelQueryResult
            {
                Model = o,
                ModelShortName = o.ModelShortName,
                ModelType = o.ModelType,
                ModelUnitType = o.ModelUnitType,
                TypeOfRealEstate = o.TypeOfRealEstate,
                WaterElectricMeterPrice = DB.WaterElectricMeterPrices.Where(b => b.ModelID == o.ID).OrderByDescending(b => b.Version).FirstOrDefault(),
                UpdatedBy = o.UpdatedBy
            });
            var unitUsedModels = await DB.Units.Where(o => o.ProjectID == projectID).Select(o => o.ModelID).ToListAsync(cancellationToken);

            #region Filter
            if (!string.IsNullOrEmpty(filter.Code))
            {
                query = query.Where(x => x.Model.Code.Contains(filter.Code));
            }
            if (!string.IsNullOrEmpty(filter.NameTH))
            {
                query = query.Where(x => x.Model.NameTH.Contains(filter.NameTH));
            }
            if (!string.IsNullOrEmpty(filter.NameEN))
            {
                query = query.Where(x => x.Model.NameEN.Contains(filter.NameEN));
            }
            if (filter.TypeOfRealEstateID != null && filter.TypeOfRealEstateID != Guid.Empty)
            {
                query = query.Where(x => x.Model.TypeOfRealEstateID == filter.TypeOfRealEstateID);
            }
            if (!string.IsNullOrEmpty(filter.ModelShortNameKey))
            {
                var modelShortNameKeyID = (await DB.MasterCenters.FirstAsync(x => x.Key == filter.ModelShortNameKey
                                                                 && x.MasterCenterGroupKey == "ModelShortName")
                                                                ).ID;
                query = query.Where(x => x.Model.ModelShortNameMasterCenterID == modelShortNameKeyID);
            }
            if (!string.IsNullOrEmpty(filter.ModelTypeKey))
            {
                var modelTypeID = (await DB.MasterCenters.FirstAsync(x => x.Key == filter.ModelTypeKey
                                                                && x.MasterCenterGroupKey == "ModelType")
                                                               ).ID;
                query = query.Where(x => x.Model.ModelTypeMasterCenterID == modelTypeID);
            }
            if (!string.IsNullOrEmpty(filter.ModelUnitTypeKey))
            {
                var modelUnitTypeID = (await DB.MasterCenters.FirstAsync(x => x.Key == filter.ModelUnitTypeKey
                                                                 && x.MasterCenterGroupKey == "ModelUnitType")
                                                                ).ID;
                query = query.Where(x => x.Model.ModelUnitTypeMasterCenterID == modelUnitTypeID);
            }
            if (filter.ElectricMeterPriceFrom != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.ElectricMeterPrice >= filter.ElectricMeterPriceFrom);
            }
            if (filter.ElectricMeterPriceTo != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.ElectricMeterPrice <= filter.ElectricMeterPriceTo);
            }
            if (filter.ElectricMeterPriceFrom != null && filter.ElectricMeterPriceTo != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.ElectricMeterPrice >= filter.ElectricMeterPriceFrom
                                   && x.WaterElectricMeterPrice.ElectricMeterPrice <= filter.ElectricMeterPriceTo);
            }
            if (filter.WaterMeterPriceFrom != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.WaterMeterPrice >= filter.WaterMeterPriceFrom);
            }
            if (filter.WaterMeterPriceTo != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.WaterMeterPrice <= filter.WaterMeterPriceTo);
            }
            if (filter.WaterMeterPriceFrom != null && filter.WaterMeterPriceTo != null)
            {
                query = query.Where(x => x.WaterElectricMeterPrice.WaterMeterPrice >= filter.WaterMeterPriceFrom
                                   && x.WaterElectricMeterPrice.WaterMeterPrice <= filter.WaterMeterPriceTo);
            }

            if (!string.IsNullOrEmpty(filter.UpdatedBy))
            {
                query = query.Where(x => x.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
            }
            if (filter.UpdatedFrom != null)
            {
                query = query.Where(x => x.Model.Updated >= filter.UpdatedFrom);
            }
            if (filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Model.Updated <= filter.UpdatedTo);
            }
            if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
            {
                query = query.Where(x => x.Model.Updated >= filter.UpdatedFrom && x.Model.Updated <= filter.UpdatedTo);
            }
            #endregion

            ModelListDTO.SortBy(sortByParam, ref query);

            //var pageOutput = PagingHelper.Paging<ModelQueryResult>(pageParam, ref query);

            var results = await query
            .Select(o => ModelListDTO.CreateModelChkUsedFromQueryResult(o, unitUsedModels)).ToListAsync(cancellationToken);

            return new ModelPaging()
            {
                //PageOutput = pageOutput,
                Models = results
            };
        }

        public async Task<ModelDTO> GetModelAsync(Guid projectID, Guid id, CancellationToken cancellationToken = default)
        {
            var model = await DB.Models.AsNoTracking()
                                       .Include(o => o.ModelShortName)
                                       .Include(o => o.ModelUnitType)
                                       .Include(o => o.ModelType)
                                       .Include(o => o.TypeOfRealEstate)
                                       .Include(o => o.UpdatedBy)
                                       .FirstAsync(o => o.ID == id, cancellationToken);

            var waterElectricMeterPriceList = await DB.WaterElectricMeterPrices.Where(o => o.ModelID == model.ID)
                                             .OrderBy(o => o.Version)
                                             .Include(o => o.UpdatedBy)
                                             .Select(o => WaterElectricMeterPriceDTO.CreateFromModel(o))
                                             .ToListAsync(cancellationToken);

            waterElectricMeterPriceList = waterElectricMeterPriceList.Count == 0 ? null : waterElectricMeterPriceList;
            //แบบบ้าน จองหรือยัง
            bool bookingChk = DB.Bookings.Include(x => x.Unit).Any(x => x.ProjectID == projectID && x.Unit.ModelID == id && x.IsCancelled == false);
            var result = ModelDTO.CreateFromModel(model, waterElectricMeterPriceList, bookingChk);
            return result;
        }

        public async Task<ModelDTO> CreateModelAsync(Guid projectID, ModelDTO input)
        {

            await input.ValidateAsync(DB);
            await this.ValidateModel(projectID, input);

            var project = await DB.Projects.FirstOrDefaultAsync(o => o.ID == projectID);

            Model model = new Model();
            input.ToModel(ref model);
            model.ProjectID = projectID;

            var key = project.ProjectNo;
            var type = "PRJ.Model";

            var runningno = await DB.RunningNumberCounters.FirstOrDefaultAsync(o => o.Key == key && o.Type == type);
            if (runningno == null)
            {
                var runningNumberCounter = new RunningNumberCounter
                {
                    Key = key,
                    Type = type,
                    Count = 1
                };
                await DB.RunningNumberCounters.AddAsync(runningNumberCounter);
                await DB.SaveChangesAsync();

                model.Code = key + runningNumberCounter.Count.ToString("000");
                runningNumberCounter.Count++;
                DB.Entry(runningNumberCounter).State = EntityState.Modified;
                await DB.SaveChangesAsync();
            }
            else
            {
                model.Code = key + runningno.Count.ToString("000");
                runningno.Count++;
                DB.Entry(runningno).State = EntityState.Modified;
                await DB.SaveChangesAsync();
            }

            var listWaterElectricMeterPriceAdd = new List<WaterElectricMeterPrice>();

            if (input.WaterElectricMeterPrices != null)
            {
                if (input.WaterElectricMeterPrices.Count > 0)
                {
                    var allWaterElectricMeterPrice = await DB.WaterElectricMeterPrices.Where(o => o.ModelID == model.ID).ToListAsync();
                    foreach (var item in input.WaterElectricMeterPrices)
                    {
                        var existingItem = allWaterElectricMeterPrice.Find(o => o.ID == item.Id);
                        if (existingItem == null)
                        {
                            var version = await DB.WaterElectricMeterPrices.Where(o => o.ModelID == model.ID).Select(o => o.Version).OrderByDescending(o => o.Value).FirstOrDefaultAsync();
                            WaterElectricMeterPrice newmodel = new WaterElectricMeterPrice();
                            item.ToModel(ref newmodel);
                            newmodel.ModelID = model.ID;
                            if (version == null && listWaterElectricMeterPriceAdd.Count == 0)
                            {
                                newmodel.Version = version == null ? 1 : version + 1;
                            }
                            else if (listWaterElectricMeterPriceAdd.Count != 0)
                            {
                                var newversion = listWaterElectricMeterPriceAdd.Where(o => o.ModelID == model.ID).Select(o => o.Version).OrderByDescending(o => o.Value).FirstOrDefault();
                                newmodel.Version = newversion == null ? 1 : newversion + 1;
                            }
                            else
                            {
                                newmodel.Version = version + 1;
                            }
                            listWaterElectricMeterPriceAdd.Add(newmodel);
                        }
                    }
                }
            }

            await DB.Models.AddAsync(model);
            await DB.AddRangeAsync(listWaterElectricMeterPriceAdd);
            await DB.SaveChangesAsync();

            var modelDataStatusMasterCenterID = await this.ModelDataStatus(projectID);
            project.ModelDataStatusMasterCenterID = modelDataStatusMasterCenterID;
            DB.Projects.Update(project);
            await DB.SaveChangesAsync();

#if !DEBUG

            var rateFixSaleModel = await DB.RateSettingFixSaleModels
                                                   .FirstOrDefaultAsync(x => x.IsActive == true
                                                            && x.ProjectID == project.ID)
                                                   ;
            var rateFixTransferModel = await DB.RateSettingFixTransferModels
                                                    .FirstOrDefaultAsync(x => x.IsActive == true
                                                            && x.ProjectID == project.ID)
                                                    ;

            var roleHCPMID = (await DB.Roles.FirstOrDefaultAsync(x => x.Code.Equals("HCPM")))?.ID;
            var emailList = await DB.UserRoles.Include(x => x.User).Where(x => x.RoleID == roleHCPMID).Select(x => x.User.Email).ToListAsync();
            var nameList = await DB.UserRoles.Include(x => x.User).Where(x => x.RoleID == roleHCPMID).Select(x => x.User.DisplayName).ToListAsync();
            string emailHR = String.Join(";", emailList);
            string nameHR = String.Join(", ", nameList);

            var isTest = bool.Parse($"{Environment.GetEnvironmentVariable("email_isTest")}");
            var test_MailTo = $"{Environment.GetEnvironmentVariable("email_Test_mailTo")}";
            var mailFrom = $"{Environment.GetEnvironmentVariable("email_mailFrom")}";

            // ส่งเมลแจ้ง มีการเพิ่มแบบบ้านใหม่ กรุณากำหนดค่า Commission
            if (rateFixSaleModel != null)
            {
                #region Sent Mail
                string Topic = "For Action: มีการเพิ่มแบบบ้านใหม่ กรุณากำหนดค่า Commission ให้กับแบบบ้าน";
                string Content = @"<div style=""font-family:Tahoma; font-size:12px;"">
                                เรียน คุณ [HRName]
                                <BR />
                                <BR />
                                ได้มีการเพิ่มแบบบ้านใหม่
                                <BR />
                                <B>ชื่อแบบบ้าน : </B> [ModelNameTH]                       
                                <BR />
                                <B>วัน-เวลาที่สร้าง : </B> [CreateDate]   
                                <BR />
                                ในโครงการ [ProjectName] ที่มี Commission Setting ""Fix ตามแบบบ้านขาย"" Active อยู่ 
                                <BR />
                                กรุณาระบุค่า Commission ให้กับแบบบ้านที่เพิ่มมาใหม่
                                <BR />
                                <BR />
                                <HR>
                                <BR />
                                ส่งโดย CRM Application System
                                </div>
                                <div>
                                <table border='0' cellspacing='0' cellpadding='0' style='font-family:AP;font-size:12px'>
                                  <tr>
                                    <td colspan='3' valign='top'></td>
                                  </tr>
                                  <tr>
                                    <td width='140' valign='top'></td>
                                    <td colspan='2' valign='top'></td>
                                  </tr>
                                  <tr>
                                    <td width='140' rowspan='5' valign='top'><p><img width='100' height='120' src='https://happyrefund.apthai.com/datashare/maillogo/aplogo.png' border='0' /></p>      <div align='right'></div>      <div align='right'></div>      <div align='right'></div>      <div align='right'></div>      <div align='right'></div></td>
                                    <td colspan='2' valign='top'><p><strong>AP (Thailand)&nbsp; Public Company Limited</strong> <br />
                                      170/57 18th Fl. Ocean Tower 1 , Ratchadapisek Rd. <br />
                                      Khongtoey Bangkok 10110</p></td>
                                  </tr>
                                  <tr>
                                    <td valign='top'><div align='left'>Sale Consult</div></td>
                                    <td valign='top'>: &nbsp;02-261-2518 Ext.649, 477, 648, 647</td>
                                  </tr>
                                  <tr>
                                    <td valign='top'><div align='left'>Account/FI Consult</div></td>
                                    <td valign='top'>: &nbsp;02-261-2518 Ext.373, 647</td>
                                    </tr>
                                  <tr>
                                    <td width='127' valign='top'><div align='left'>Email</div></td>
                                    <td width='266' valign='top'>: &nbsp;<a href='mailto:crmsale@apthai.com'>crmsale@apthai.com</a></td>
                                  </tr>
                                </table>
                                </div>";

                Content = Content.Replace("[HRName]", nameHR);
                Content = Content.Replace("[ModelNameTH]", model.NameTH);
                Content = Content.Replace("[ProjectName]", project.ProjectNo + " - " + project.ProjectNameTH);
                Content = Content.Replace("[CreateDate]", string.Format("{0: d/MM/yyyy HH:mm:ss}", model.Created));

                var mail = new Param_Mail();
                mail.MailFrom = mailFrom;
                mail.MailTo = (isTest) ? test_MailTo : emailHR;
                mail.MailBCC = test_MailTo;
                mail.Topic = ((isTest) ? "[DEV] " : "") + Topic;
                mail.Detail = Content;
                mail.MailType = "CreateModel";
                mail.Key1 = model.Code;
                mail.Key2 = model.NameTH;
                mail.Key3 = "";
                mail.Key4 = "";
                mail.MailTime = DateTime.Now;

                var respon = await SendMail(mail);

                //if (!respon)
                //{
                //    ValidateException ex = new ValidateException();
                //    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
                //    var msg = "ไม่สามารถส่ง E-Mail ได้ กรุณาติดต่อผู้ดูแลระบบ";
                //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                //    throw ex;
                //}
                #endregion
            }

            if (rateFixTransferModel != null)
            {
                #region Sent Mail
                string Topic = "For Action: มีการเพิ่มแบบบ้านใหม่ กรุณากำหนดค่า Commission ให้กับแบบบ้าน";
                string Content = @"<div style=""font-family:Tahoma; font-size:12px;"">
                                เรียน คุณ [HRName]
                                <BR />
                                <BR />
                                ได้มีการเพิ่มแบบบ้านใหม่
                                <BR />
                                <B>ชื่อแบบบ้าน : </B> [ModelNameTH]                        
                                <BR />
                                <B>วัน-เวลาที่สร้าง : </B> [CreateDate]  
                                <BR />
                                ในโครงการ [ProjectName] ที่มี Commission Setting ""Fix ตามแบบบ้านโอน"" Active อยู่ 
                                <BR />
                                กรุณาระบุค่า Commission ให้กับแบบบ้านที่เพิ่มมาใหม่
                                <BR />
                                <BR />
                                <HR>
                                <BR />
                                ส่งโดย CRM Application System
                                </div>
                                <div>
                                <table border='0' cellspacing='0' cellpadding='0' style='font-family:AP;font-size:12px'>
                                  <tr>
                                    <td colspan='3' valign='top'></td>
                                  </tr>
                                  <tr>
                                    <td width='140' valign='top'></td>
                                    <td colspan='2' valign='top'></td>
                                  </tr>
                                  <tr>
                                    <td width='140' rowspan='5' valign='top'><p><img width='100' height='120' src='https://happyrefund.apthai.com/datashare/maillogo/aplogo.png' border='0' /></p>      <div align='right'></div>      <div align='right'></div>      <div align='right'></div>      <div align='right'></div>      <div align='right'></div></td>
                                    <td colspan='2' valign='top'><p><strong>AP (Thailand)&nbsp; Public Company Limited</strong> <br />
                                      170/57 18th Fl. Ocean Tower 1 , Ratchadapisek Rd. <br />
                                      Khongtoey Bangkok 10110</p></td>
                                  </tr>
                                  <tr>
                                    <td valign='top'><div align='left'>Sale Consult</div></td>
                                    <td valign='top'>: &nbsp;02-261-2518 Ext.649, 477, 648, 647</td>
                                  </tr>
                                  <tr>
                                    <td valign='top'><div align='left'>Account/FI Consult</div></td>
                                    <td valign='top'>: &nbsp;02-261-2518 Ext.373, 647</td>
                                    </tr>
                                  <tr>
                                    <td width='127' valign='top'><div align='left'>Email</div></td>
                                    <td width='266' valign='top'>: &nbsp;<a href='mailto:crmsale@apthai.com'>crmsale@apthai.com</a></td>
                                  </tr>
                                </table>
                                </div>";

                Content = Content.Replace("[HRName]", nameHR);
                Content = Content.Replace("[ModelNameTH]", model.NameTH);
                Content = Content.Replace("[ProjectName]", project.ProjectNo + " - " + project.ProjectNameTH);
                Content = Content.Replace("[CreateDate]", string.Format("{0: d/MM/yyyy HH:mm:ss}", model.Created));

                var mail = new Param_Mail();
                mail.MailFrom = mailFrom;
                mail.MailTo = (isTest) ? test_MailTo : emailHR;
                mail.MailBCC = test_MailTo;
                mail.Topic = ((isTest) ? "[DEV] " : "") + Topic;
                mail.Detail = Content;
                mail.MailType = "CreateModel";
                mail.Key1 = model.Code;
                mail.Key2 = model.NameTH;
                mail.Key3 = "";
                mail.Key4 = "";
                mail.MailTime = DateTime.Now;

                var respon = await SendMail(mail);

                //if (!respon)
                //{
                //    ValidateException ex = new ValidateException();
                //    var errMsg = await DB.ErrorMessages.Where(o => o.Key == "ERR0001").FirstAsync();
                //    var msg = "ไม่สามารถส่ง E-Mail ได้ กรุณาติดต่อผู้ดูแลระบบ";
                //    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                //    throw ex;
                //}                
                #endregion
            }

#endif

            var result = await this.GetModelAsync(projectID, model.ID);
            return result;
        }

        public async Task<ModelDTO> UpdateModelAsync(Guid projectID, Guid id, ModelDTO input)
        {
            await input.ValidateAsync(DB);
            await this.ValidateModel(projectID, input);

            var project = await DB.Projects.FindAsync(projectID);
            var model = await DB.Models.FindAsync(id);
            input.ToModel(ref model);
            model.ProjectID = projectID;



            var listWaterElectricMeterPriceAdd = new List<WaterElectricMeterPrice>();
            var listWaterElectricMeterPriceUpdate = new List<WaterElectricMeterPrice>();
            var listWaterElectricMeterPriceDelete = new List<WaterElectricMeterPrice>();
            if (input.WaterElectricMeterPrices != null)
            {
                if (input.WaterElectricMeterPrices.Count > 0)
                {
                    var allWaterElectricMeterPrice = await DB.WaterElectricMeterPrices.Where(o => o.ModelID == model.ID).ToListAsync();
                    foreach (var item in input.WaterElectricMeterPrices)
                    {
                        var existingItem = allWaterElectricMeterPrice.Find(o => o.ID == item.Id);
                        if (existingItem == null) //add
                        {
                            var version = await DB.WaterElectricMeterPrices.Where(o => o.ModelID == model.ID).Select(o => o.Version).OrderByDescending(o => o.Value).FirstOrDefaultAsync();
                            WaterElectricMeterPrice newmodel = new WaterElectricMeterPrice();
                            item.ToModel(ref newmodel);
                            newmodel.ModelID = model.ID;
                            if (version == null && listWaterElectricMeterPriceAdd.Count == 0)
                            {
                                newmodel.Version = version == null ? 1 : version + 1;
                            }
                            else if (listWaterElectricMeterPriceAdd.Count != 0)
                            {
                                var newversion = listWaterElectricMeterPriceAdd.Where(o => o.ModelID == model.ID).Select(o => o.Version).OrderByDescending(o => o.Value).FirstOrDefault();
                                newmodel.Version = newversion == null ? 1 : newversion + 1;
                            }
                            else
                            {
                                newmodel.Version = version + 1;
                            }
                            listWaterElectricMeterPriceAdd.Add(newmodel);
                        }
                        else //update
                        {
                            var waterMeterPrice = item.WaterMeterPrice;
                            var electricMeterPrice = item.ElectricMeterPrice;
                            var electricMeterSize = item.ElectricMeterSize;
                            var waterMeterSize = item.WaterMeterSize;
                            if (waterMeterPrice != existingItem.WaterMeterPrice || electricMeterPrice != existingItem.ElectricMeterPrice
                                                                                || !electricMeterSize.Equals(existingItem.ElectricMeterSize)
                                                                                || !waterMeterSize.Equals(existingItem.WaterMeterSize))
                            {
                                item.ToModel(ref existingItem);
                                listWaterElectricMeterPriceUpdate.Add(existingItem);
                            }
                        }
                    }
                    foreach (var item in allWaterElectricMeterPrice)
                    {
                        var existingInput = input.WaterElectricMeterPrices?.Find(o => o.Id == item.ID);
                        if (existingInput == null)
                        {
                            item.IsDeleted = true;
                            listWaterElectricMeterPriceDelete.Add(item);
                        }
                    }
                }
            }
            else
            {
                var allWaterElectricMeterPrice = await DB.WaterElectricMeterPrices.Where(o => o.ModelID == model.ID).ToListAsync();
                foreach (var item in allWaterElectricMeterPrice)
                {
                    var existingInput = input.WaterElectricMeterPrices?.Find(o => o.Id == item.ID);
                    if (existingInput == null)
                    {
                        item.IsDeleted = true;
                        listWaterElectricMeterPriceDelete.Add(item);
                    }
                }
            }
            DB.Entry(model).State = EntityState.Modified;
            DB.UpdateRange(listWaterElectricMeterPriceUpdate);
            DB.UpdateRange(listWaterElectricMeterPriceDelete);
            await DB.AddRangeAsync(listWaterElectricMeterPriceAdd);
            await DB.SaveChangesAsync();

            var reversions = await DB.WaterElectricMeterPrices.Where(o => o.ModelID == model.ID).ToListAsync();
            var reversion = 1;
            foreach (var item in reversions.OrderBy(o => o.Created))
            {
                item.Version = reversion;
                reversion++;
            }
            await DB.SaveChangesAsync();

            var modelDataStatusMasterCenterID = await this.ModelDataStatus(projectID);
            project.ModelDataStatusMasterCenterID = modelDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            var result = await this.GetModelAsync(projectID, model.ID);

            return result;
        }

        public async Task<ModelDTO> UpdateModelListAsync(Guid projectID, List<ModelDTO> inputs, List<Guid> ids)
        {
            var models = await DB.Models.Where(o => o.ProjectID == projectID).ToListAsync();

            foreach (Guid modelID in ids)
            {
                var model = models.Find(o => o.ID == modelID);
                if (model is not null)
                {
                    model.PreferUnit = inputs[0].PreferUnit != null ? inputs[0].PreferUnit : 0;
                    model.PreferUnitMinimum = inputs[0].PreferUnitMinimum != null ? inputs[0].PreferUnitMinimum : 0;
                    model.PreferHouse = inputs[0].PreferHouse != null ? inputs[0].PreferHouse : 0;

                    DB.Entry(model).State = EntityState.Modified;
                    await DB.SaveChangesAsync();
                }
            }
            var result = new ModelDTO();
            return result;
        }

        public async Task<Model> DeleteModelAsync(Guid projectID, Guid id)
        {
            var project = await DB.Projects.FirstAsync(o => o.ID == projectID);
            var model = await DB.Models.FirstAsync(o => o.ID == id);
            model.IsDeleted = true;
            await DB.SaveChangesAsync();


            var modelDataStatusMasterCenterID = await this.ModelDataStatus(projectID);
            project.ModelDataStatusMasterCenterID = modelDataStatusMasterCenterID;
            await DB.SaveChangesAsync();

            return model;
        }

        private async Task<Guid> ModelDataStatus(Guid projectID)
        {
            var modelDataStatusSaleMasterCenterID = (await DB.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Sale)).ID; //พร้อมขาย
            var modelDataStatusPrepareMasterCenterID = (await DB.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Draft)).ID; //อยู่ระหว่างจัดเตรียม
            var allModel = await DB.Models.Where(o => o.ProjectID == projectID).ToListAsync();
            var modelID = allModel.Select(o => (Guid?)o.ID).ToList();
            var allWaterElectricMeterPrices = await DB.WaterElectricMeterPrices.Where(o => modelID.Contains(o.ModelID)).ToListAsync();

            var modelDataStatusMasterCenterID = modelDataStatusPrepareMasterCenterID;

            if (allModel.Count == 0 || allWaterElectricMeterPrices.Count == 0)
            {
                return modelDataStatusPrepareMasterCenterID;
            }

            if (allModel.TrueForAll(o =>
                     !string.IsNullOrEmpty(o.Code)
                     && !string.IsNullOrEmpty(o.NameTH)
                     //&& !string.IsNullOrEmpty(o.NameEN)
                     && o.ModelUnitTypeMasterCenterID != null
                     && o.TypeOfRealEstateID != null
                            )
                     && allWaterElectricMeterPrices.TrueForAll(o =>
                                 o.Version != null
                                 && o.WaterMeterPrice != null
                                 && o.WaterMeterSize != null
                                 && o.ElectricMeterPrice != null
                                 && o.ElectricMeterSize != null
                                                        )
                )
            {
                modelDataStatusMasterCenterID = modelDataStatusSaleMasterCenterID;
            }
            else
            {
                modelDataStatusMasterCenterID = modelDataStatusPrepareMasterCenterID;
            }
            return modelDataStatusMasterCenterID;
        }

        private async Task ValidateModel(Guid projectID, ModelDTO input)
        {
            ValidateException ex = new ValidateException();
            //validate unique
            if (!string.IsNullOrEmpty(input.NameTH))
            {
                var checkUniqueNameTH = input.Id != (Guid?)null
               ? DB.Models.Any(o => o.ProjectID == projectID && o.ID != input.Id && o.NameTH == input.NameTH && o.Code == input.Code)
               : DB.Models.Any(o => o.ProjectID == projectID && o.NameTH == input.NameTH && o.Code == input.Code);
                if (checkUniqueNameTH)
                {
                    var errMsg = await DB.ErrorMessages.FirstAsync(o => o.Key == "ERR0042");
                    string desc = input.GetType().GetProperty(nameof(ModelDTO.NameTH)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", input.NameTH);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (!string.IsNullOrEmpty(input.NameEN))
            {
                var checkUniqueNameEN = input.Id != (Guid?)null
               ? DB.Models.Any(o => o.ProjectID == projectID && o.ID != input.Id && o.NameEN == input.NameEN && o.Code == input.Code)
               : DB.Models.Any(o => o.ProjectID == projectID && o.NameEN == input.NameEN && o.Code == input.Code);
                if (checkUniqueNameEN)
                {
                    var errMsg = await DB.ErrorMessages.FirstAsync(o => o.Key == "ERR0042");
                    string desc = input.GetType().GetProperty(nameof(ModelDTO.NameEN)).GetCustomAttribute<DescriptionAttribute>().Description;
                    var msg = errMsg.Message.Replace("[field]", desc);
                    msg = msg.Replace("[value]", input.NameEN);
                    ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
                }
            }
            if (ex.HasError)
            {
                throw ex;
            }
        }

        private async Task<bool> SendMail(Param_Mail mail)
        {
            var requestUrl = $"{Environment.GetEnvironmentVariable("email_HostEmail")}";
            var param_Mails = new List<Param_Mail>
            {
                mail
            };

            using (var httpClient = new HttpClient())
            {
                using (var stringContent = new StringContent(JsonConvert.SerializeObject(param_Mails), System.Text.Encoding.UTF8, "application/json"))
                using (var response = await httpClient.PostAsync(requestUrl, stringContent))
                {
                    var r = response;
                    if (r.StatusCode != HttpStatusCode.OK)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
