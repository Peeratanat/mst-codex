using Database.Models;
using Database.Models.MST;
using MST_General.Params.Filters;
using Base.DTOs.MST;
using Microsoft.EntityFrameworkCore;
using MST_General.Params.Outputs;
using PagingExtensions;
using ErrorHandling;
using System.ComponentModel;
using System.Reflection;
using LinqKit;
using Common.Helper.Logging;

namespace MST_General.Services
{
	public class BrandService : IBrandService
	{
		public LogModel logModel { get; set; }
		private readonly DatabaseContext DB;

		public BrandService(DatabaseContext db)
		{
			logModel = new LogModel("BrandService", null);
			DB = db;
		}

		public async Task<List<BrandDropdownDTO>> GetBrandDropdownListAsync(string name, CancellationToken cancellationToken = default)
		{
			IQueryable<Brand> query = DB.Brands.AsNoTracking();
			var predicate = PredicateBuilder.New<Brand>(true);
			if (!string.IsNullOrEmpty(name))
			{
				predicate = predicate.And(o => o.Name.Contains(name));
			}

			var results = await query.Where(predicate).OrderBy(o => o.Name).Take(1000)
					.Select(o => BrandDropdownDTO.CreateFromModel(o)).ToListAsync(cancellationToken);
			return results;
		}

		public async Task<BrandPaging> GetBrandListAsync(BrandFilter filter, PageParam pageParam, BrandSortByParam sortByParam, CancellationToken cancellationToken = default)
		{
			IQueryable<BrandQueryResult> query = DB.Brands.AsNoTracking()
												   .Select(o => new BrandQueryResult
												   {
													   Brand = o,
													   UnitNumberFormat = o.UnitNumberFormat,
													   UpdatedBy = o.UpdatedBy
												   });
			var predicate = PredicateBuilder.New<BrandQueryResult>(true);

			#region Filter
			if (!string.IsNullOrEmpty(filter.BrandNo))
			{
				predicate = predicate.And(x => x.Brand.BrandNo.Contains(filter.BrandNo));
			}
			if (!string.IsNullOrEmpty(filter.Name))
			{
				predicate = predicate.And(x => x.Brand.Name.Contains(filter.Name));
			}
			if (!string.IsNullOrEmpty(filter.UnitNumberFormatKey))
			{
				var unitNumberFormatMasterCenter = await DB.MasterCenters.AsNoTracking().FirstAsync(x => x.Key == filter.UnitNumberFormatKey
																	 && x.MasterCenterGroupKey == "UnitNumberFormat");
				predicate = predicate.And(x => x.UnitNumberFormat.ID == unitNumberFormatMasterCenter.ID);
			}
			if (!string.IsNullOrEmpty(filter.UpdatedBy))
			{
				predicate = predicate.And(x => x.Brand.UpdatedBy.DisplayName.Contains(filter.UpdatedBy));
			}
			if (filter.UpdatedFrom != null)
			{
				predicate = predicate.And(x => x.Brand.Updated >= filter.UpdatedFrom);
			}
			if (filter.UpdatedTo != null)
			{
				predicate = predicate.And(x => x.Brand.Updated <= filter.UpdatedTo);
			}
			if (filter.UpdatedFrom != null && filter.UpdatedTo != null)
			{
				predicate = predicate.And(x => x.Brand.Updated >= filter.UpdatedFrom && x.Brand.Updated <= filter.UpdatedTo);
			}
			#endregion
			query = query.Where(predicate);
			BrandDTO.SortBy(sortByParam, ref query);

			var pageOutput = PagingHelper.Paging(pageParam, ref query);
			var results = await query.Select(o => BrandDTO.CreateFromQueryResult(o)).ToListAsync(cancellationToken);

			return new BrandPaging()
			{
				PageOutput = pageOutput,
				Brands = results
			};
		}

		public async Task<BrandDTO> GetBrandAsync(Guid id, CancellationToken cancellationToken = default)
		{

			var model = await DB.Brands.AsNoTracking()
									   .Include(o => o.UnitNumberFormat)
									   .Include(o => o.UpdatedBy)
									   .FirstOrDefaultAsync(o => o.ID == id, cancellationToken: cancellationToken);
			var result = BrandDTO.CreateFromModel(model);
			return result;

		}

		public async Task<BrandDTO> CreateBrandAsync(BrandDTO input)
		{
			await input.ValidateAsync(DB);
			Brand model = new Brand();
			input.ToModel(ref model);

			await DB.Brands.AddAsync(model);
			await DB.SaveChangesAsync();

			var result = await GetBrandAsync(model.ID);

			return result;

		}

		public async Task<BrandDTO> UpdateBrandAsync(Guid id, BrandDTO input)
		{
			await input.ValidateUpdateAsync(DB);
			var model = await DB.Brands.FindAsync(id);
			input.ToModel(ref model);

			DB.Entry(model).State = EntityState.Modified;
			await DB.SaveChangesAsync();
			var result = await GetBrandAsync(model.ID);
			return result;

		}

		public async Task DeleteBrandAsync(Guid id)
		{
			#region Validate Check Used Data
			ValidateException ex = new ValidateException();

			var brandUsed = await DB.Projects.FirstOrDefaultAsync(o => o.BrandID == id);
			if (brandUsed != null)
			{
				var errMsg = await DB.ErrorMessages.FirstAsync(o => o.Key == "ERR0139");
				string desc = typeof(BrandDTO).GetProperty(nameof(BrandDTO.BrandNo)).GetCustomAttribute<DescriptionAttribute>().Description;
				var msg = errMsg.Message.Replace("[field]", desc);
				ex.AddError(errMsg.Key, msg, (int)errMsg.Type);
			}

			if (ex.HasError)
			{
				throw ex;
			}
			#endregion

			// await DB.Brands.Where(o => o.ID == id).ExecuteUpdateAsync(c =>
			// 			   c.SetProperty(col => col.IsDeleted, true)
			// 			   .SetProperty(col => col.Updated, DateTime.Now)
			// 		   );

			var model = await DB.Brands.FindAsync(id);
			model.IsDeleted = true;
			await DB.SaveChangesAsync();
		}
	}
}
