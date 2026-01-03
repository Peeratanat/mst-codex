using System;

namespace Base.DTOs
{
    public class BaseDTO
    {
        public Guid? Id { get; set; } = new Guid();
        public string UpdatedBy { get; set; }
        public DateTime? Updated { get; set; }
    }
}
