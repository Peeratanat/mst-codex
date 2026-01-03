using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Base.DTOs;
using Base.DTOs.PRJ;
using Common.Helper.HttpResultHelper;
using Database.Models;
using Database.UnitTestExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PRJ_Project.Services;
using Xunit;

namespace PRJ_Project.UnitTests
{
    public class ImagesControllerTests
    {
        public ImagesControllerTests()
        {

            Environment.SetEnvironmentVariable("minio_AccessKey", "XNTYE7HIMF6KK4BVEIXA");
            Environment.SetEnvironmentVariable("minio_DefaultBucket", "master-data");
            Environment.SetEnvironmentVariable("minio_PublicURL", "192.168.2.29:30050");
            Environment.SetEnvironmentVariable("minio_Endpoint", "192.168.2.29:9001");
            Environment.SetEnvironmentVariable("minio_SecretKey", "naD+esQ+uV7+xwfF3bPfAn5iC7C1XUyXeM8HkBlO");
            Environment.SetEnvironmentVariable("minio_TempBucket", "temp");
            Environment.SetEnvironmentVariable("minio_WithSSL", "false");
        }
        [Fact]
        public async Task GetProjectLogoAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var project = await db.Projects.AsNoTracking().FirstAsync(f => f.Logo != null);
                        var service = new ImageService(db);

                        var result = await service.GetProjectLogoAsync(project.ID);
                        Assert.NotNull(result.Url);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteProjectLogoAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var project = await db.Projects.AsNoTracking().FirstAsync(f => f.Logo != null);
                        var service = new ImageService(db);

                        await service.DeleteProjectLogoAsync(project.ID);
                        bool afterDelete = db.Projects.Any(o => o.ID == project.ID && o.Logo != null);
                        Assert.False(afterDelete);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetFloorPlanImagesAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var project = await db.FloorPlanImages.AsNoTracking().FirstAsync();
                        var service = new ImageService(db);

                        var result = await service.GetFloorPlanImagesAsync(project.ProjectID, null);
                        Assert.NotEmpty(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetFloorPlanDetailAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var project = await db.FloorPlanImages.AsNoTracking().FirstAsync();
                        var service = new ImageService(db);

                        var result = await service.GetFloorPlanDetailAsync(project.ProjectID, null, null, null);
                        Assert.NotEmpty(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetRoomPlanDetailAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var project = await db.FloorPlanImages.AsNoTracking().FirstAsync();
                        var service = new ImageService(db);

                        var result = await service.GetRoomPlanDetailAsync(project.ProjectID, null, null, null);
                        Assert.NotEmpty(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetRoomPlanImagesAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var project = await db.FloorPlanImages.AsNoTracking().FirstAsync();
                        var service = new ImageService(db);

                        var result = await service.GetRoomPlanImagesAsync(project.ProjectID, null);
                        Assert.NotEmpty(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }


    }
}
