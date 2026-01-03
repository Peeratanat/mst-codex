using Base.DTOs;
using Base.DTOs.MST;
using Database.Models.MST;
using MST_Event.Params.Filters;
using MST_Event.Params.Outputs;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MST_Event.Services
{
    public interface IEventService : BaseInterfaceService
    {
        Task<List<EventDTO>> GetEventDropownList(CancellationToken cancellationToken = default);
        Task<PaymentGatewayConfig> GetPaymentOnlineConfig(Guid ProjectID, CancellationToken cancellationToken = default);
    }
}
