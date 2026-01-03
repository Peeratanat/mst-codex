using Database.Models.MasterKeys;
using Database.Models.MST;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using models = Database.Models;
using Database.Models.PRJ;
using FileStorage;
using System.Threading.Tasks;

namespace Base.DTOs.MST
{
    public class EventDTO
    {
        public Guid ID { get; set; }

        public string NameTH { get; set; }

        public string NameEN { get; set; }

        public DateTime? EventDateFrom { get; set; }

        public DateTime? EventDateTo { get; set; }


        public static async Task<EventDTO> CreateFromModel(Event model)
        {
            if (model != null)
            {
                EventDTO result = new EventDTO()
                {

                    ID = model.ID,
                    NameTH = model.NameTH,
                    NameEN = model.NameEN,
                    EventDateFrom = model.EventDateFrom,
                    EventDateTo = model.EventDateTo,

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
