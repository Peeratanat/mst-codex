using System;
using System.Linq;
using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using PRJ_Unit.Params.Filters;
using PRJ_Unit.Params.Inputs;
using PRJ_Unit.Services;
using Xunit;
using models = Database.Models;
using OfficeOpenXml;

namespace PRJ_Unit.UnitTests
{
    public class TitleDeedServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();

        public TitleDeedServiceTest()
        {
            Environment.SetEnvironmentVariable("minio_AccessKey", "XNTYE7HIMF6KK4BVEIXA");
            Environment.SetEnvironmentVariable("minio_DefaultBucket", "master-data");
            Environment.SetEnvironmentVariable("minio_PublicURL", "192.168.2.29:30050");
            Environment.SetEnvironmentVariable("minio_Endpoint", "192.168.2.29:9001");
            Environment.SetEnvironmentVariable("minio_SecretKey", "naD+esQ+uV7+xwfF3bPfAn5iC7C1XUyXeM8HkBlO");
            Environment.SetEnvironmentVariable("minio_TempBucket", "temp");
            Environment.SetEnvironmentVariable("minio_WithSSL", "false");
            Environment.SetEnvironmentVariable("report_SecretKey", "nIcHeoYiMNZiJMYz");
            Environment.SetEnvironmentVariable("report_Url", "http://192.168.4.160/rptcrmrevo/printform.aspx");

        }

        [Fact]
        public async Task GetTitleDeedListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        //Put unit test here

                        var service = new TitleDeedService(db);
                        var titleDeed = await db.Projects.FirstOrDefaultAsync(f => f.ProjectNo == "10019");


                        PageParam pageParam = new PageParam()
                        {
                            Page = 1,
                            PageSize = 1
                        };
                        TitleDeedListSortByParam sortByParam = new TitleDeedListSortByParam()
                        {
                            SortBy = TitleDeedListSortBy.Unit
                        };
                        var results = await service.GetTitleDeedListAsync(titleDeed.ID, new TitleDeedFilter(), pageParam, sortByParam);
                        Assert.NotEmpty(results.TitleDeeds);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetTitleDeedAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        //Put unit test here

                        var service = new TitleDeedService(db);

                        var titleDeed = await db.TitledeedDetails.FirstOrDefaultAsync();


                        var results = await service.GetTitleDeedAsync(titleDeed.ID);
                        Assert.NotNull(results);


                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateTitleDeedAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new TitleDeedService(db);


                        var query = from t1 in db.TitledeedDetails
                                    join t2 in db.Units on t1.UnitID equals t2.ID into t2Group
                                    from t2 in t2Group.DefaultIfEmpty()
                                    where t2 == null
                                    select t1;
                        var titledeedModel = await query.FirstOrDefaultAsync();
                        var project = await db.Projects.FirstOrDefaultAsync(f => f.ID == titledeedModel.ProjectID);
                        var query2 = from t1 in db.Units
                                     join t2 in db.TitledeedDetails on t1.ID equals t2.UnitID into t2Group
                                     from t2 in t2Group.DefaultIfEmpty()
                                     where t2 == null
                                     select t1;


                        var unit = await query2.FirstOrDefaultAsync();
                        var titledeed = new TitleDeedDTO
                        {
                            TitledeedNo = "999999999",
                            Project = ProjectDropdownDTO.CreateFromModel(project),
                            Unit = UnitDropdownDTO.CreateFromModel(unit)
                        };

                        var result = await service.CreateTitleDeedAsync(project.ID, titledeed);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateTitleDeedAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        //Put unit test here

                        var service = new TitleDeedService(db);


                        var query = from t1 in db.TitledeedDetails
                                    join t2 in db.Units on t1.UnitID equals t2.ID into t2Group
                                    from t2 in t2Group.DefaultIfEmpty()
                                    where t2 == null
                                    select t1;
                        var titledeedModel = await query.FirstOrDefaultAsync();
                        var project = await db.Projects.FirstOrDefaultAsync(f => f.ID == titledeedModel.ProjectID);
                        var query2 = from t1 in db.Units
                                     join t2 in db.TitledeedDetails on t1.ID equals t2.UnitID into t2Group
                                     from t2 in t2Group.DefaultIfEmpty()
                                     where t2 == null
                                     select t1;


                        var unit = await query2.FirstOrDefaultAsync();
                        var titledeed = new TitleDeedDTO
                        {
                            TitledeedNo = "999999999",
                            Project = ProjectDropdownDTO.CreateFromModel(project),
                            Unit = UnitDropdownDTO.CreateFromModel(unit)
                        };

                        var result = await service.CreateTitleDeedAsync(project.ID, titledeed);

                        result.TitledeedNo = "123456";
                        var resultUpdate = await service.UpdateTitleDeedAsync(project.ID, result.Id.Value, result);

                        Assert.NotNull(result);
                        Assert.Equal(resultUpdate.TitledeedNo, "123456");

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteTitleDeedAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        //Put unit test here

                        var service = new TitleDeedService(db);

                        var TitledeedDetail = await db.TitledeedDetails.FirstOrDefaultAsync();

                        await service.DeleteTitleDeedAsync((Guid)TitledeedDetail.ProjectID, TitledeedDetail.ID);
                        bool afterDelete = db.TitledeedDetails.Any(o => o.ID == TitledeedDetail.ID && o.IsDeleted == false);
                        Assert.False(afterDelete);
                        await tran.RollbackAsync();
                    }
                });
            }
        }



        [Fact]
        public async Task UpdateMultipleHouseNosAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new TitleDeedService(db);
                        var project = await db.Projects.FirstAsync(o => o.ProjectNo == "40017");
                        var fromUnit = await db.Units.FirstAsync(o => o.ProjectID == project.ID && o.UnitNo == "N05B02");
                        var toUnit = await db.Units.FirstAsync(o => o.ProjectID == project.ID && o.UnitNo == "N05B10");
                        var input = new UpdateMultipleHouseNoParam();
                        input.FromUnit = UnitDropdownDTO.CreateFromModel(fromUnit);
                        input.ToUnit = UnitDropdownDTO.CreateFromModel(toUnit);
                        input.FromHouseNo = "16/2";
                        await service.UpdateMultipleHouseNosAsync(project.ID, input);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateMultipleLandOfficesAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new TitleDeedService(db);
                        var input = new UpdateMultipleLandOfficeParam();
                        var project = await db.Projects.FirstAsync(o => o.ProjectNo == "40017");
                        var fromUnit = await db.Units.FirstAsync(o => o.ProjectID == project.ID && o.UnitNo == "N05B02");
                        var toUnit = await db.Units.FirstAsync(o => o.ProjectID == project.ID && o.UnitNo == "N05B10");
                        var landOffice = await db.LandOffices.FirstAsync();
                        input.FromUnit = UnitDropdownDTO.CreateFromModel(fromUnit);
                        input.ToUnit = UnitDropdownDTO.CreateFromModel(toUnit);
                        input.LandOffice = LandOfficeListDTO.CreateFromModel(landOffice);
                        await service.UpdateMultipleLandOfficesAsync(project.ID, input);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task ImportTitleDeedAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new TitleDeedService(db);
                        var project = await db.Projects.FirstOrDefaultAsync(o => o.ProjectNo == "40017");
                        FileDTO fileInput = new FileDTO()
                        {
                            Url = "http://192.168.2.29:9001/xunit-tests/ProjectID_TitleDeed.xlsx",
                            Name = "ProjectID_TitleDeed.xlsx"
                        };
                        var result = await service.ImportTitleDeedAsync(project.ID, fileInput);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task ExportExcelTitleDeedAsync()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new TitleDeedService(db);
                        TitleDeedFilter filter = new TitleDeedFilter();
                        TitleDeedListSortByParam sortByParam = new TitleDeedListSortByParam();
                        var project = await db.Projects.Where(o => o.ProjectNo == "40017").FirstOrDefaultAsync();
                        var result = await service.ExportExcelTitleDeedAsync(project.ID);
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
    }
}
