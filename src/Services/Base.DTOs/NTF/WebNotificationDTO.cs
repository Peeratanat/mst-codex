using System;
using Database.Models.NTF;

namespace Base.DTOs.NTF
{
    public class WebNotificationDTO
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string Action { get; set; }
        public string Params { get; set; }
        public SendStatus Status { get; set; }
        public DateTime? Created { get; set; }

        public static WebNotificationDTO CreateFromModel(WebNotification model)
        {
            if (model != null)
            {
                var result = new WebNotificationDTO()
                {
                    Id = model.ID,
                    Message = model.Message,
                    Action = model.Action,
                    Params = model.Params,
                    Status = model.Status,
                    Created = model.Created
                };

                return result;
            }
            else
            {
                return null;
            }
        }

    }
}
