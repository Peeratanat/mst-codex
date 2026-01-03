using Base.DTOs.SAL;
using PagingExtensions; 
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Services
{
    public interface IKafkaService
    {
        Task ProduceBooking(Guid? BookingID);
        Task ProduceCancelBooking(Guid? BookingID);
        Task ProduceTransfers(Guid? BookingID);
        Task ProduceAgreement(Guid? AgreementID);
    }
}
