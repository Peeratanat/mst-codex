using Base.DTOs.MST;
using Database.Models;
using Database.Models.MST;
using Microsoft.EntityFrameworkCore;
using Common.Helper.Logging;

namespace MST_Event.Services
{
    public class EventService : IEventService
    {
        private readonly DatabaseContext DB;
        public LogModel logModel { get; set; }

        public EventService(DatabaseContext db)
        {
            logModel = new LogModel("EventService", null);
            this.DB = db;

        }


        public async Task<List<EventDTO>> GetEventDropownList(CancellationToken cancellationToken = default)
        {
            var events = await DB.Event
                .Include(o => o.CreatedBy)
                .Include(o => o.UpdatedBy)
                .Where(o => o.Isactive == true)
                .ToListAsync(cancellationToken);

            var result = new List<EventDTO>();

            if (events.Any())
            {
                foreach (var e in events)
                {
                    var item = await EventDTO.CreateFromModel(e);
                    result.Add(item);
                }
            }

            return result;
        }

        public async Task<PaymentGatewayConfig> GetPaymentOnlineConfig(Guid ProjectID, CancellationToken cancellationToken = default)
        {
            return await DB.PaymentGatewayConfigs.FirstOrDefaultAsync(o => o.ProjectID == ProjectID, cancellationToken) ?? new();
        }



    }
}
