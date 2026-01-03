namespace CRMMobile.Helper
{
    public static class ContactAddressType
    {
        public static readonly string ContactAddress = "0";
        public static readonly string CitizenAddress = "1";
        public static readonly string Home = "2";
        public static readonly string OfficeWarking = "3";
    }

    public static class ContactType
    {
        public static readonly string Personal = "0";
        public static readonly string Corporate = "1";
    }

    public static class Gender
    {
        public static readonly string Male = "0";
        public static readonly string Female = "1";
    }

    public static class LeadActivityStatusType
    {
        public static readonly string CorrectNumber = "0";
        public static readonly string IncorrectNumber = "1";
        public static readonly string CannotAppointment = "2";
    }

    public static class LeadActivityFollowUpType
    {
        public static readonly string Disqualify = "0";
        public static readonly string FollowUp = "1";
    }

    public static class NationalType
    {
        public static readonly string ThaiNation = "T03";
        public static readonly string ThaiNationCode = "TH";
    }

    public static class PrefixType
    {
        public static readonly string Other = "-1";
    }

    public static class OpportunityKey
    {
        /// <summary>
        /// ประเมินโอกาสขาย
        /// </summary>
        public static readonly string EstimateSalesOpportunity = "EstimateSalesOpportunity";

        /// <summary>
        /// โอกาสขาย
        /// </summary>
        public static readonly string SalesOpportunity = "SalesOpportunity";

        /// <summary>
        ///
        /// </summary>
        public static readonly string OpportunityActivity = "OpportunityActivity";
    }

    public static class MasterCenterKey
    {
        public static readonly string ConvenientTime = "ConvenientTime";
        public static readonly string RevisitActivityType = "RevisitActivityType";
        public static readonly string ContactTitleTH = "ContactTitleTH";
        public static readonly string ContactTitleEN = "ContactTitleEN";
        public static readonly string PhoneType = "PhoneType";
        public static readonly string National = "National";
        public static readonly string ContactStatus = "ContactStatus";
        public static readonly string Vehicle = "Vehicle";
        public static readonly string VisitBy = "VisitBy";
        public static readonly string ActivityTaskOverdueStatus = "ActivityTaskOverdueStatus";
        public static readonly string ActivityTaskStatus = "ActivityTaskStatus";
        public static readonly string ActivityTaskTopic = "ActivityTaskTopic";
        public static readonly string ActivityTaskType = "ActivityTaskType";
    }

    public static class WalkActivityStatusType
    {
        public static readonly string ActivityResult = "0";
        public static readonly string ActivityEnd = "1";
    }

    public static class ActivityTaskOverdueStatus
    {
        public static readonly string NotDue = "1";
        public static readonly string Due = "2";
        public static readonly string Overdue = "3";
    }

    public static class ActivityTaskStatus
    {
        public static readonly string Overdue = "1";
        public static readonly string Completed = "2";
        public static readonly string Incomplete = "3";
    }

    public static class ActivityTaskTopic
    {
        public static readonly string Lead = "1";
        public static readonly string Walk = "2";
        public static readonly string Revisit = "3";
    }

    public static class ActivityTaskType
    {
        public static readonly string Plan = "1";
        public static readonly string Follow = "2";
        public static readonly string Revisit = "3";
        public static readonly string Question = "4";
        public static readonly string Appointment = "5";
    }

    public static class PhoneType
    {
        public static readonly string MobileTelNo = "0";
        public static readonly string HomeTelNo = "1";
        public static readonly string OfficeTelNo = "2";
        public static readonly string ForiegnTelNo = "3";
    }
}