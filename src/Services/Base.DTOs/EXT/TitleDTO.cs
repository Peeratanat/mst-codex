using Database.Models;
using Database.Models.DbQueries.EXT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using models = Database.Models;

namespace Base.DTOs.EXT
{
	public class TitleDTO
	{

		[Description("titleid")]
		public string titleid { get; set; }

		[Description("titlenameth")]
		public string titlenameth { get; set; }

		[Description("titlenameen")]
		public string titlenameen { get; set; }

		public void ToModel(ref dbqGetTitleList model)
		{
			//model.projectid = this.projectid;
			//model.project_name = this.project_name;
			//model.title_name = this.title_name;
			//model.first_name = this.first_name;
			//model.last_name = this.last_name;
			//model.email = this.email;
			//model.mobile_no = this.mobile_no;

		}

		public static TitleDTO CreateFromQuery(dbqGetTitleList model, DatabaseContext db)
		{
			if (model != null)
			{
				var result = new TitleDTO();
				result.titleid = model.titleid;
				result.titlenameth = model.titlenameth;
				result.titlenameen = model.titlenameen;

				return result;
			}
			else
			{
				return null;
			}
		}

	}
}
