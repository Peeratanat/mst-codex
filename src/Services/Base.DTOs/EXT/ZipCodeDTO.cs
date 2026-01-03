using Database.Models;
using Database.Models.DbQueries.EXT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using models = Database.Models;

namespace Base.DTOs.EXT
{
	public class ZipCodeDTO
	{

		[Description("subdistrict_code")]
		public string subdistrict_code { get; set; }

		[Description("code")]
		public string code { get; set; }

		[Description("zibcode")]
		public string zibcode { get; set; }

		public void ToModel(ref dbqGetZipCodeList model)
		{
			//model.projectid = this.projectid;
			//model.project_name = this.project_name;
			//model.title_name = this.title_name;
			//model.first_name = this.first_name;
			//model.last_name = this.last_name;
			//model.email = this.email;
			//model.mobile_no = this.mobile_no;

		}

		public static ZipCodeDTO CreateFromQuery(dbqGetZipCodeList model, DatabaseContext db)
		{
			if (model != null)
			{
				var result = new ZipCodeDTO();
				result.subdistrict_code = model.subdistrict_code;
				result.code = model.code;
				result.zibcode = model.zipcode;

				return result;
			}
			else
			{
				return null;
			}
		}

	}
}
