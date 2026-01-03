using System;

namespace Base.DTOs.EXT
{
	internal class ContactEmailDTO
	{
		public Guid Id { get; set; }
		public string Email { get; set; }
		public bool IsMain { get; set; }
	}
}