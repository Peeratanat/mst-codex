using Database.Models.PRJ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.PRJ
{
    public class PublicAppointmentDTO 
    {
        public DateTime? appointmentDatetime { get; set; }
        public int appointmentID { get; set; }
        public string inspectionStatus { get; set; }
        public string inspectionStatusCode { get; set; }
        public string projectCode { get; set; }
        public int? roundOfInspect { get; set; }
        public string tagColor { get; set; }
        public string title { get; set; }
        public string titleFullProject { get; set; }
        public string unitNo { get; set; }
    }

    public class DefecttrackingResult
    {
        public string requestID { get; set; }
        public string message { get; set; } 
        public DefecttrackingDataResult data { get; set; }
    }
    public class DefecttrackingDataResult
    {
        public int? total { get; set; }
        public List<PublicAppointmentDTO> sourceData { get; set; }
    }

    public class CreateAppointmentDTO
    {
        public int appointmentID { get; set; }
        public string projectCode { get; set; }
        public string unitNo { get; set; }
        public string inspectCode { get; set; }
        public string inspectTypeCode { get; set; }
        public int roundOfInspect { get; set; }
        public DateTime? appointmentDate { get; set; }
        public string appointmentTimeStartHour { get; set; }
        public string appointmentTimeStartMinute { get; set; }
        public string customerPhoneNo { get; set; }
        public string customerName { get; set; }
        public string createdBy { get; set; }
        public string createdByName { get; set; }
        public string seResponsibleCode { get; set; }
        public string seResponsibleName { get; set; }
        public string remark { get; set; }
        public string sourceTypeCreated { get; set; }
        public bool? saveAndSendSMS { get; set; }
    }

    public class DefecttrackingCreateResult
    { 
        public string message { get; set; }
        public string data { get; set; }
    }

    public class PublicAppointmentListDTO
    {
        public int appointmentID { get; set; }
        public string appointmentCode { get; set; }
        public string projectCode { get; set; }
        public string projectName { get; set; }
        public string projectFullName { get; set; }
        public string houseNo { get; set; }
        public string unitNo { get; set; }
        public string agreementCustomerNameTH { get; set; }
        public string appointmentStatus { get; set; }
        public string roundOfInspect { get; set; }
        public string seResponsibleCode { get; set; }
        public string seResponsibleName { get; set; }
        public string inspectStatusCode { get; set; }
        public string inspectTypeCode { get; set; }
        public string customerPhoneNo { get; set; }
        public string customerName { get; set; }
        public DateTime? appointmentDate { get; set; }
        public string appointmentTimeStartHour { get; set; }
        public string appointmentTimeStartMinute { get; set; }
        public string appointmentTimeEndHour { get; set; }
        public string appointmentTimeEndMinute { get; set; }
        public string remark { get; set; }
        public string inspectStatusTitle { get; set; }
        public string inspectTypeTitle { get; set; }
        public DateTime? createdDate { get; set; }
        public DateTime? updatedDate { get; set; }
        public string crmContactNo { get; set; }
        public string agreementNo { get; set; }
        public string bookingNo { get; set; }
        public DateTime? deletedDate { get; set; }
        public string badgeStatus { get; set; }
        public string createdBy { get; set; }
        public string createdByName { get; set; }
        public string updatedBy { get; set; }
        public string updatedByName { get; set; }
        public string currentCustomerNameTH { get; set; }
    }

    public class DefecttrackingListResult
    {
        public string requestID { get; set; }
        public string message { get; set; }
        public DefecttrackingDataListResult data { get; set; }
    }
    public class DefecttrackingDataListResult
    {
        public int? total { get; set; }
        public List<PublicAppointmentListDTO> sourceData { get; set; }
    }
    public class DeleteDefecttrackingInput
    {
        public int appointmentID { get; set; }
        public string deletedBy { get; set; }
        public string deletedByName { get; set; }
    }
    public class SeDTO
    {
        public string displayFullName { get; set; }
        public string empCode { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string titleName { get; set; }
        public string fullName { get; set; }
    }
    public class SEDataResult
    {
        public int? total { get; set; }
        public List<SeDTO> sourceData { get; set; }
    }
    public class SEListResult
    {
        public string requestID { get; set; }
        public string message { get; set; }
        public SEDataResult data { get; set; }
    }
    public class InspectionTypeDTO
    {
        public int inspectionTypeId { get; set; }
        public string code { get; set; }
        public string titleTH { get; set; }
        public string titleEN { get; set; }
    } 
    public class InspectionTypeListResult
    {
        public string requestID { get; set; }
        public string message { get; set; }
        public List<InspectionTypeDTO> data { get; set; }
    }


}
