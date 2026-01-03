using Database.Models;
using Database.Models.DbQueries.EXT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using models = Database.Models;

namespace Base.DTOs.EXT
{
	public class ProvinceDTO
	{

		[Description("code")]
		public string code { get; set; }

		[Description("name")]
		public string name { get; set; }

		public void ToModel(ref dbqGetProvinceList model)
		{
			//model.projectid = this.projectid;
			//model.project_name = this.project_name;
			//model.title_name = this.title_name;
			//model.first_name = this.first_name;
			//model.last_name = this.last_name;
			//model.email = this.email;
			//model.mobile_no = this.mobile_no;

		}

		public static ProvinceDTO CreateFromQuery(dbqGetProvinceList model, DatabaseContext db)
		{
			if (model != null)
			{
				var result = new ProvinceDTO();
				result.code = model.code;
				result.name = model.name;

				return result;
			}
			else
			{
				return null;
			}
		}

	}
}
