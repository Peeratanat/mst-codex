using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRJ;
using Base.DTOs.USR;
using Common.Helper.Logging;
using Database.Models;
using Database.Models.PRJ;
using Common.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using FileStorage;
namespace PRJ_Project.Services
{
	public class ImageService : IImageService
	{
		private readonly DatabaseContext DB;
		private FileHelper FileHelper;
		public LogModel logModel { get; set; }

		public ImageService(DatabaseContext db)
		{
			logModel = new LogModel("ImageService", null);
			DB = db;

			var minioEndpoint = Environment.GetEnvironmentVariable("minio_Endpoint");
			var minioAccessKey = Environment.GetEnvironmentVariable("minio_AccessKey");
			var minioSecretKey = Environment.GetEnvironmentVariable("minio_SecretKey");
			var minioWithSSL = Environment.GetEnvironmentVariable("minio_WithSSL");
			var minioTempBucketName = Environment.GetEnvironmentVariable("minio_TempBucket");
			var minioBucketName = Environment.GetEnvironmentVariable("minio_DefaultBucket");
			var publicURL = Environment.GetEnvironmentVariable("minio_PublicURL");

			FileHelper = new FileHelper(minioEndpoint, minioAccessKey, minioSecretKey, minioBucketName, minioTempBucketName, publicURL, Convert.ToBoolean(minioWithSSL));
		}

		public async Task<FileDTO> UpdateProjectLogoAsync(Guid projectID, FileDTO input)
		{
			var projectNo = await DB.Projects.FirstOrDefaultAsync(o => o.ID == projectID);

			if (input.IsTemp)
			{
				string logoName = $"Images/{projectNo.ProjectNo}/logo/{input.Name}";
				await FileHelper.MoveTempFileAsync(input.Name, logoName);

				string url = await FileHelper.GetFileUrlAsync(logoName);

				var result = new FileDTO()
				{
					Name = logoName,
					Url = url
				};

				var project = await DB.Projects.FirstAsync(o => o.ID == projectID);
				var pictureDataStatusMasterCenterID = await PicturesDataStatus(projectID);
				project.Logo = logoName;
				project.PictureDataStatusMasterCenterID = pictureDataStatusMasterCenterID;
				await DB.SaveChangesAsync();

				return result;
			}
			else
			{
				return input;
			}
		}

		public async Task DeleteProjectLogoAsync(Guid projectID)
		{
			var project = await DB.Projects.FirstAsync(o => o.ID == projectID);
			var pictureDataStatusMasterCenterID = await PicturesDataStatus(projectID);
			project.PictureDataStatusMasterCenterID = pictureDataStatusMasterCenterID;
			project.Logo = null;
			await DB.SaveChangesAsync();
		}

		public async Task<FileDTO> GetProjectLogoAsync(Guid projectID, CancellationToken cancellationToken = default)
		{
			var project = await DB.Projects.AsNoTracking().FirstOrDefaultAsync(o => o.ID == projectID, cancellationToken);
			if (!string.IsNullOrEmpty(project?.Logo))
			{
				string url = await FileHelper.GetFileUrlAsync(project.Logo);

				var result = new FileDTO()
				{
					Name = project.Logo,
					Url = url
				};

				return result;
			}
			else
			{
				return null;
			}
		}

		public async Task<List<FloorPlanImageDTO>> GetFloorPlanImagesAsync(Guid projectID, string name, CancellationToken cancellationToken = default)
		{
			var query = DB.FloorPlanImages.AsNoTracking().Include(o => o.UpdatedBy).Where(o => o.ProjectID == projectID);
			if (!string.IsNullOrEmpty(name))
			{
				query = query.Where(o => o.Name.Contains(name));
			}
			var floorPlanImages = await query.ToListAsync(cancellationToken);
			var results = floorPlanImages
					.Select(async o => await FloorPlanImageDTO.CreateFromModelAsync(o, FileHelper))
					.Select(o => o.Result).ToList();
			return results;
		}

		public async Task<List<FloorPlanDetailDTO>> GetFloorPlanDetailAsync(Guid projectID, Guid? unitID, Guid? floorID, Guid? towerID, CancellationToken cancellationToken = default)
		{
		 
			var query = DB.Units.Include(o => o.Tower).Include(o => o.Floor)
									.Include(o => o.UnitStatus)
									.Where(o => o.ProjectID == projectID);

			if (unitID != null)
			{
				query = query.Where(o => o.ID == unitID);
			}

			if (floorID != null)
			{
				query = query.Where(o => o.FloorID == floorID);
			}

			if (towerID != null)
			{
				query = query.Where(o => o.TowerID == towerID);
			}

			var floorPlanDetail = await query.ToListAsync();
			var results = floorPlanDetail
					.Select(async o => await FloorPlanDetailDTO.CreateFromModelAsync(o, FileHelper, DB))
					.Select(o => o.Result).ToList();

			return results;
		}

		public async Task<List<RoomPlanDetailDTO>> GetRoomPlanDetailAsync(Guid projectID, Guid? unitID, Guid? floorID, Guid? towerID, CancellationToken cancellationToken = default)
		{
			var query = DB.Units.AsNoTracking().Include(o => o.Tower).Include(o => o.Floor)
									.Include(o => o.UnitStatus)
									.Where(o => o.ProjectID == projectID);

			if (unitID != null)
			{
				query = query.Where(o => o.ID == unitID);
			}

			if (floorID != null)
			{
				query = query.Where(o => o.FloorID == floorID);
			}

			if (towerID != null)
			{
				query = query.Where(o => o.TowerID == towerID);
			}

			var roomPlanDetail = await query.ToListAsync(cancellationToken);
			var results = roomPlanDetail
					.Select(async o => await RoomPlanDetailDTO.CreateFromModelAsync(o, FileHelper, DB))
					.Select(o => o.Result).ToList();

			return results;
		}

		public async Task<List<RoomPlanImageDTO>> GetRoomPlanImagesAsync(Guid projectID, string name, CancellationToken cancellationToken = default)
		{
			IQueryable<RoomPlanImage> query = DB.RoomPlanImages.AsNoTracking().Include(o => o.UpdatedBy).Where(o => o.ProjectID == projectID);
			if (!string.IsNullOrEmpty(name))
			{
				query = query.Where(o => o.Name.Contains(name));
			}
			var roomPlanImages = await query.ToListAsync(cancellationToken);
			var results = roomPlanImages
				.Select(async o => await RoomPlanImageDTO.CreateFromModelAsync(o, FileHelper))
				.Select(o => o.Result).ToList();
			return results;
		}

		public async Task<List<FloorPlanImageDTO>> SaveFloorPlanImagesAsync(Guid projectID, List<FloorPlanImageDTO> inputs)
		{
			var addingList = new List<FloorPlanImage>();
			var updatingList = new List<FloorPlanImage>();
			var deletingList = new List<FloorPlanImage>();

			var allFloorPlanImages = await DB.FloorPlanImages.Where(o => o.ProjectID == projectID).ToListAsync();
			var projectNo = await DB.Projects.Where(o => o.ID == projectID).Select(o => o.ProjectNo).FirstOrDefaultAsync();

			//find add and update items
			foreach (var item in inputs)
			{
				var existingImageName = allFloorPlanImages.Where(o => o.ID != item.Id && o.Name == item.Name).Any();
				if (existingImageName) //Same
				{
					continue;
				}

				var existingItem = allFloorPlanImages.SingleOrDefault(o => o.ID == item.Id);

				if (existingItem != null)
				{
					if (item.File.IsTemp)
					{
						string floorPlanName = $"Images/{projectNo}/floorPlans/{item.File.Name}";
						await FileHelper.MoveTempFileAsync(item.File.Name, floorPlanName);
						item.File.Name = floorPlanName;
					}

					item.ToModel(ref existingItem);
					existingItem.FileName = $"Images/{projectNo}/floorPlans/{item.File.Name}";
					updatingList.Add(existingItem);
				}
				else
				{
					string floorPlanName = $"Images/{projectNo}/floorPlans/{item.File.Name}";
					await FileHelper.MoveTempFileAsync(item.File.Name, floorPlanName);
					item.File.Name = floorPlanName;

					existingItem = new FloorPlanImage();
					item.ToModel(ref existingItem);
					existingItem.ProjectID = projectID;
					addingList.Add(existingItem);
				}
			}
			//find delete items
			foreach (var item in allFloorPlanImages)
			{
				var existingInput = inputs.SingleOrDefault(o => o.Id == item.ID);
				if (existingInput == null)
				{
					item.IsDeleted = true;
					deletingList.Add(item);
				}
			}



			//save to database
			DB.UpdateRange(updatingList);
			DB.UpdateRange(deletingList);
			await DB.AddRangeAsync(addingList);
			await DB.SaveChangesAsync();

			var project = await DB.Projects.Where(o => o.ID == projectID).FirstOrDefaultAsync();
			var pictureDataStatusMasterCenterID = await PicturesDataStatus(projectID);
			project.PictureDataStatusMasterCenterID = pictureDataStatusMasterCenterID;
			DB.UpdateRange(project);
			await DB.SaveChangesAsync();

			allFloorPlanImages = await DB.FloorPlanImages.Where(o => o.ProjectID == projectID).ToListAsync();
			var results = allFloorPlanImages
				.Select(async o => await FloorPlanImageDTO.CreateFromModelAsync(o, FileHelper))
				.Select(o => o.Result).ToList();

			return results;

		}

		public async Task<List<RoomPlanImageDTO>> SaveRoomPlanImagesAsync(Guid projectID, List<RoomPlanImageDTO> inputs)
		{
			var addingList = new List<RoomPlanImage>();
			var updatingList = new List<RoomPlanImage>();
			var deletingList = new List<RoomPlanImage>();

			var allRoomPlanImages = await DB.RoomPlanImages.Where(o => o.ProjectID == projectID).ToListAsync();
			var projectNo = await DB.Projects.Where(o => o.ID == projectID).Select(o => o.ProjectNo).FirstOrDefaultAsync();

			//find add and update items
			foreach (var item in inputs)
			{
				var existingImageName = allRoomPlanImages.Where(o => o.ID != item.Id && o.Name == item.Name).Any();
				if (existingImageName) //Same
				{
					continue;
				}

				var existingItem = allRoomPlanImages.SingleOrDefault(o => o.ID == item.Id);
				if (existingItem != null)
				{
					if (item.File.IsTemp)
					{
						string roomPlanName = $"Images/{projectNo}/roomPlans/{item.File.Name}";
						await FileHelper.MoveTempFileAsync(item.File.Name, roomPlanName);
						item.File.Name = roomPlanName;
					}
					item.ToModel(ref existingItem);
					existingItem.FileName = $"Images/{projectNo}/roomPlans/{item.File.Name}";

					updatingList.Add(existingItem);
				}
				else
				{
					string roomPlanName = $"Images/{projectNo}/roomPlans/{item.File.Name}";
					await FileHelper.MoveTempFileAsync(item.File.Name, roomPlanName);
					item.File.Name = roomPlanName;

					existingItem = new RoomPlanImage();
					item.ToModel(ref existingItem);
					existingItem.ProjectID = projectID;
					addingList.Add(existingItem);
				}
			}
			//find delete items
			foreach (var item in allRoomPlanImages)
			{
				var existingInput = inputs.SingleOrDefault(o => o.Id == item.ID);
				if (existingInput == null)
				{
					item.IsDeleted = true;
					deletingList.Add(item);
				}
			}

			//save to database
			DB.UpdateRange(updatingList);
			DB.UpdateRange(deletingList);
			await DB.AddRangeAsync(addingList);
			await DB.SaveChangesAsync();

			var project = await DB.Projects.Where(o => o.ID == projectID).FirstOrDefaultAsync();
			var pictureDataStatusMasterCenterID = await PicturesDataStatus(projectID);
			project.PictureDataStatusMasterCenterID = pictureDataStatusMasterCenterID;
			DB.UpdateRange(project);
			await DB.SaveChangesAsync();

			allRoomPlanImages = await DB.RoomPlanImages.Where(o => o.ProjectID == projectID).ToListAsync();
			var results = allRoomPlanImages
				.Select(async o => await RoomPlanImageDTO.CreateFromModelAsync(o, FileHelper))
				.Select(o => o.Result).ToList();

			return results;

		}

		private async Task<Guid> PicturesDataStatus(Guid projectID)
		{
			var project = await DB.Projects.Include(o => o.ProductType).FirstOrDefaultAsync(p => p.ID == projectID);

			var floors = await DB.FloorPlanImages.Where(o => o.ProjectID == projectID && o.IsDeleted == false).ToListAsync();
			var rooms = await DB.RoomPlanImages.Where(o => o.ProjectID == projectID && o.IsDeleted == false).ToListAsync();

			var picutureDataStatusPrepareMasterCenterID = (await DB.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Draft)).ID; //อยู่ระหว่างจัดเตรียม
			var picutureDataStatusSaleMasterCenterID = (await DB.MasterCenters.FirstAsync(o => o.MasterCenterGroupKey == "ProjectDataStatus" && o.Key == ProjectDataStatusKeys.Sale)).ID; //พร้อมขาย

			var pictureDataStatusMasterCenterID = picutureDataStatusPrepareMasterCenterID;

			if (project.ProductType.Key == ProductTypeKeys.HighRise)
			{
				if (project == null || floors.Count == 0 || rooms.Count == 0)
				{
					return pictureDataStatusMasterCenterID;
				}

				if (!string.IsNullOrEmpty(project.Logo) && floors.Count > 0 && rooms.Count > 0)
				{
					pictureDataStatusMasterCenterID = picutureDataStatusSaleMasterCenterID;
				}
			}
			else
			{
				pictureDataStatusMasterCenterID = picutureDataStatusSaleMasterCenterID;
			}

			return pictureDataStatusMasterCenterID;
		}


	}
}
