using Base.DTOs.USR;
using FileStorage;
using System;
using System.Linq;
using System.Threading.Tasks;
using models = Database.Models;

namespace Base.DTOs.CTM
{
    public class VisitorListDTO
    {
        /// <summary>
        /// ID ของ Visitor
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// ข้อมูลของ Contact
        /// </summary>
        public ContactListDTO Contact { get; set; }
        /// <summary>
        /// เลขที่รับ
        /// </summary>
        public string ReceiveNumber { get; set; }
        /// <summary>
        /// ชื่อ (ภาษาไทย)
        /// </summary>
        public string FirstNameTH { get; set; }
        /// <summary>
        /// นามสกุล (ภาษาไทย)
        /// </summary>
        public string LastNameTH { get; set; }
        /// <summary>
        /// เบอร์โทรศัพท์
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// เดินทางโดย
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=VisitBy
        /// </summary>
        public MST.MasterCenterDropdownDTO VisitBy { get; set; }
        /// <summary>
        /// รายละเอียด
        /// </summary>
        public string VehicleDescription { get; set; }
        /// <summary>
        /// สถานะ Walk
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=WalkStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO WalkStatus { get; set; }
        /// <summary>
        /// สถานะลูกค้า
        /// masterdata/api/MasterCenters/DropdownList?masterCenterGroupKey=ContactStatus
        /// </summary>
        public MST.MasterCenterDropdownDTO ContactStatus { get; set; }
        /// <summary>
        /// ผู้ดูแล
        /// </summary>
        public USR.UserListDTO Owner { get; set; }
        /// <summary>
        /// วันที่เข้าโครงการ
        /// </summary>
        public DateTime? VisitDateIn { get; set; }
        /// <summary>
        /// วันที่ออกโครงการ
        /// </summary>
        public DateTime? VisitDateOut { get; set; }
        /// <summary>
        /// ไฟล์แนบ
        /// </summary>
        public FileDTO File { get; set; }
        /// <summary>
        /// กลุ่มลูกค้าที่เยี่ยมโครงการ  1 รูปถ่ายบัตร, 2 ลูกค้าใหม่, 3 ลูกค้าเก่า
        /// </summary>
        public int CustomerVisitGroup { get; set; }

        /// <summary>
        /// เลขประจำตัวประชาชน
        /// </summary>
        public string CitizenIdentityNo { get; set; }

        public static async Task<VisitorListDTO> CreateFromModelAsync(VisitorQueryResult model, FileHelper fileHelper, models.DatabaseContext DB)
        {
            if (model != null)
            {
                VisitorListDTO result = new VisitorListDTO()
                {
                    Id = model.Visitor?.ID,
                    ReceiveNumber = model.Visitor?.VisitorRunning,
                    FirstNameTH = model.Visitor?.FirstNameTH,
                    LastNameTH = model.Visitor?.LastNameTH,
                    PhoneNumber = model.ContactPhone?.PhoneNumber,
                    VisitBy = MST.MasterCenterDropdownDTO.CreateFromModel(model.VisitBy),
                    VehicleDescription = model.Visitor?.VehicleDescription,
                    WalkStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.WalkStatus),
                    ContactStatus = MST.MasterCenterDropdownDTO.CreateFromModel(model.ContactStatus),
                    VisitDateIn = model.Visitor?.VisitDateIn,
                    VisitDateOut = model.Visitor?.VisitDateOut,
                    Contact = ContactListDTO.CreateFromModel(model.Contact, DB),
                    Owner = UserListDTO.CreateFromModel(model.Owner),
                };

                if (result.Contact != null)
                {
                    result.FirstNameTH = result.Contact?.FirstNameTH;
                    result.LastNameTH = result.Contact?.LastNameTH;
                    result.CustomerVisitGroup = 3; //ลูกค้าเก่า
                }
                else
                {
                    if (!string.IsNullOrEmpty(model.Visitor.CitizenIdentityNo))
                    {
                        result.CustomerVisitGroup = 2; //ลูกค้าใหม่
                    }
                    else
                    {
                        result.CustomerVisitGroup = 1; //รูปถ่ายบัตร
                        if (!string.IsNullOrEmpty(model.Visitor?.PersonalVisitImageFromCard))
                            result.File = await FileDTO.CreateFromFileNameAsync(model.Visitor?.PersonalVisitImageFromCard, fileHelper);
                    }
                }

                return result;
            }
            else
            {
                return null;
            }
        }

        public static void SortBy(VisitorListSortByParam sortByParam, ref IQueryable<VisitorQueryResult> query)
        {
            IOrderedQueryable<VisitorQueryResult> orderQuery;
            if (sortByParam.SortBy != null)
            {
                switch (sortByParam.SortBy.Value)
                {
                    case VisitorListSortBy.ReceiveNumber:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Visitor.VisitorRunning);
                        else orderQuery = query.OrderByDescending(o => o.Visitor.VisitorRunning);
                        break;
                    case VisitorListSortBy.ContactNo:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Contact.ContactNo);
                        else orderQuery = query.OrderByDescending(o => o.Contact.ContactNo);
                        break;
                    case VisitorListSortBy.FullName:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Visitor.FirstNameTH).ThenBy(o => o.Visitor.LastNameTH);
                        else orderQuery = query.OrderByDescending(o => o.Visitor.FirstNameTH).ThenByDescending(o => o.Visitor.LastNameTH);
                        break;
                    case VisitorListSortBy.PhoneNumber:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.ContactPhone.PhoneNumber);
                        else orderQuery = query.OrderByDescending(o => o.ContactPhone.PhoneNumber);
                        break;
                    case VisitorListSortBy.VisitBy:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Visitor.VisitBy.Order);
                        else orderQuery = query.OrderByDescending(o => o.Visitor.VisitBy.Order);
                        break;
                    case VisitorListSortBy.WalkStatus:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Visitor.WalkStatus.Order);
                        else orderQuery = query.OrderByDescending(o => o.Visitor.WalkStatus.Order);
                        break;
                    case VisitorListSortBy.ContactStatus:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Visitor.ContactStatus.Order);
                        else orderQuery = query.OrderByDescending(o => o.Visitor.ContactStatus.Order);
                        break;
                    case VisitorListSortBy.VehicleDescription:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Visitor.VehicleDescription);
                        else orderQuery = query.OrderByDescending(o => o.Visitor.VehicleDescription);
                        break;
                    case VisitorListSortBy.Owner:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Visitor.Owner.FirstName);
                        else orderQuery = query.OrderByDescending(o => o.Visitor.Owner.FirstName);
                        break;
                    case VisitorListSortBy.VisitDateIn:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Visitor.VisitDateIn);
                        else orderQuery = query.OrderByDescending(o => o.Visitor.VisitDateIn);
                        break;
                    case VisitorListSortBy.VisitDateOut:
                        if (sortByParam.Ascending) orderQuery = query.OrderBy(o => o.Visitor.VisitDateOut);
                        else orderQuery = query.OrderByDescending(o => o.Visitor.VisitDateOut);
                        break;
                    default:
                        orderQuery = query.OrderBy(o => o.Visitor.VisitorRunning);
                        break;
                }
            }
            else
            {
                orderQuery = query.OrderBy(o => o.Visitor.VisitorRunning);
            }

            orderQuery.ThenBy(o => o.Visitor.ID);
            query = orderQuery;
        }
    }

    public class VisitorQueryResult
    {
        public models.CTM.Visitor Visitor { get; set; }
        public models.CTM.Contact Contact { get; set; }
        public models.PRJ.Project Project { get; set; }
        public models.MST.MasterCenter VisitBy { get; set; }
        public models.MST.MasterCenter ContactStatus { get; set; }
        public models.MST.MasterCenter WalkStatus { get; set; }
        public models.MST.MasterCenter Vehicle { get; set; }
        public models.USR.User Owner { get; set; }
        public models.CTM.ContactPhone ContactPhone { get; set; }
    }
}