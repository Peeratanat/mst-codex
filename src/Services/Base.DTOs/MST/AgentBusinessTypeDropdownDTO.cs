using Database.Models.MST;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DTOs.MST
{
    public class AgentBusinessTypeDropdownDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public static AgentBusinessTypeDropdownDTO CreateFromModel(AgentBusinessTypeDropdownDTO model)
        {
            if (model != null)
            {
                var result = new AgentBusinessTypeDropdownDTO()
                {
                    Id = model.Id,
                    Name = model.Name,
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
