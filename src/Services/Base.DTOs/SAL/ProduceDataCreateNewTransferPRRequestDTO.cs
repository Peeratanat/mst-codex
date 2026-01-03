using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Base.DTOs.USR;
using Database.Models;
using Database.Models.MasterKeys;
using Database.Models.MST;
using Database.Models.PRJ;
using Database.Models.SAL;
using Database.Models.USR;
using FileStorage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.DTOs.SAL
{
//     project_id = transfers?.ProjectID,
//    unit_id = transfers?.UnitID,
//    booking_id = transfers?.Agreement?.BookingID,
//    transfer_id = transfers?.ID,
//    actualtransfer_date = transfers?.ActualTransferDate.GetValueOrDefault().ToString("yyyy-MM-dd HH:mm:ss.fff")

    public class ProduceDataCreateNewPRRequestDTO
    {
        public Guid? project_id { get; set; }
        public Guid? unit_id { get; set; }
        public Guid? transfer_id { get; set; }
        public DateTime? actualtransfer_date { get; set; }
    }


    public class ConsumeDefectUnitReceive
    {
        public DateTime produceDate { get; set; }
        public string projectCode { get; set; }
        public string wbsNo { get; set; }
        public string unitNo { get; set; }
        public DateTime receiveDate { get; set; }
        public string receiveCustomerContactNo { get; set; }
        public string receiveCustomerNameTH { get; set; }
        public string receiveCustomerNameEN { get; set; }

    }

    public class ConsumeDefectCancelUnitReceive
    {
        public DateTime produceDate { get; set; }
        public string projectCode { get; set; }
        public string wbsNo { get; set; }
        public string unitNo { get; set; }
        public DateTime cancelReceiveDate { get; set; }
        public string cancelReceiveEmpcode { get; set; }
        public string cancelReceiveBy { get; set; }
    }

    public class ConsumeMemoRequestWaiveQCTopic
    {
        public DateTime? produceDate { get; set; }
        public string projectNo { get; set; }
        public string wbsNo { get; set; }
        public string unitNo { get; set; }
        public DateTime? waiveQCDate { get; set; }
        public string waiveQCEmpcode { get; set; }
        public string waiveQCBy { get; set; }
        public string memoNo { get; set; }
    }

    public class ConsumeMemoRequestWaiveQCCancelTopic
    {
        public DateTime produceDate { get; set; }
        public string projectNo { get; set; }
        public string wbsNo { get; set; }
        public string unitNo { get; set; }
        public DateTime cancelWaiveQCDate { get; set; }
        public string cancelWaiveQCEmpcode { get; set; }
        public string cancelWaiveQCBy { get; set; }
        public string cancelRemark { get; set; }
        public string memoNo { get; set; }
    }

    public class ConsumeMemoRequestWaiveReceiveTopic
    {
        public DateTime produceDate { get; set; }
        public string projectNo { get; set; }
        public string wbsNo { get; set; }
        public string unitNo { get; set; }
        public DateTime waiveReceiveDate { get; set; }
        public string waiveReceiveEmpcode { get; set; }
        public string waiveReceiveBy { get; set; }
        public string memoNo { get; set; }
    }

    public class ConsumeMemoRequestWaiveReceiveCancelTopic
    {
        public DateTime produceDate { get; set; }
        public string projectNo { get; set; }
        public string wbsNo { get; set; }
        public string unitNo { get; set; }
        public DateTime cancelWaiveReceiveDate { get; set; }
        public string cancelWaiveReceiveEmpcode { get; set; }
        public string cancelWaiveReceiveBy { get; set; }
        public string cancelRemark { get; set; }
        public string memoNo { get; set; }
    }
}
