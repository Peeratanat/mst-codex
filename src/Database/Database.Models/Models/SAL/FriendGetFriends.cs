using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Database.Models.SAL
{
    [Description("FGF")]
    [Table("FriendGetFriends", Schema = Schema.SALE)]
    public class FriendGetFriends : BaseEntityWithoutMigrate
    {
        public DateTime? RecommenderRegisterDate { get; set; }
        public string RecommenderTitleName { get; set; }
        public string RecommenderFirstName { get; set; }
        public string RecommenderLastName { get; set; }
        public string RecommenderPhoneNumber { get; set; }
        public string RecommenderEmail { get; set; }
        public Guid? ContactID { get; set; }
        public string FGFCode { get; set; }
        public string ReferralTitleName { get; set; }
        public string ReferralFirstName { get; set; }
        public string ReferralLastName { get; set; }
        public string ReferralPhoneNumber { get; set; }
        public string ReferralEmail { get; set; }
        public string ProjectNo { get; set; }
        public string ProjectName { get; set; }
        public Guid? BookingID { get; set; }
        public bool? IsEmployee { get; set; }
        public string EmployeeCode { get; set; }
        public bool? IsFGFCallBack { get; set; }
        public bool? IsAPDirectMkt { get; set; }
        public bool? IsSendToBC { get; set; }
        public bool? IsAcceptPaymentFGF { get; set; }
        public bool? IsUsed { get; set; }
        public string RecommenderCitizenIdentityNo { get; set; }
    }
}
