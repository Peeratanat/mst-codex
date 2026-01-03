using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.PRJ;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MST_Unitmeter.Params.Filters;
using MST_Unitmeter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using PagingExtensions;
using OfficeOpenXml;

namespace MST_Unitmeter.UnitTests
{
    public class UnitMeterServiceTest
    {
        public UnitMeterServiceTest()
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
        public async Task GetWaterMeterPriceDropdownListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                var unit = await db.Units.FirstAsync();
                var service = new UnitMeterService(db);

                var result = await service.GetWaterMeterPriceDropdownListAsync(unit.ID);
                Assert.NotEmpty(result);
                await tran.RollbackAsync();
            });
        }
        [Fact]
        public async Task GetElectricMeterPriceDropdownListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                var unit = await db.Units.FirstAsync();
                var service = new UnitMeterService(db);

                var result = await service.GetElectricMeterPriceDropdownListAsync(unit.ID);
                Assert.NotEmpty(result);
                await tran.RollbackAsync();
            });
        }
        [Fact]
        public async Task GetUnitMeterListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                var service = new UnitMeterService(db);

                UnitMeterFilter filter = new();
                PageParam pageParam = new()
                {
                    Page = 1,
                    PageSize = 1
                };
                UnitMeterListSortByParam sortByParam = new();

                var result = await service.GetUnitMeterListAsync(filter, pageParam, sortByParam);
                Assert.NotEmpty(result.ProjectUnitMeterLists);
                await tran.RollbackAsync();
            });
        }
        [Fact]
        public async Task GetUnitMeterAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                var unit = await db.Units.FirstAsync();
                var service = new UnitMeterService(db);

                UnitMeterFilter filter = new();
                PageParam pageParam = new();
                UnitMeterListSortByParam sortByParam = new();

                var result = await service.GetUnitMeterAsync(unit.ID);
                Assert.NotNull(result);
                await tran.RollbackAsync();
            });
        }
        [Fact]
        public async Task DeleteUnitMeterAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                var unit = await db.Units.FirstAsync();

                var service = new UnitMeterService(db);
                await service.DeleteUnitMeterAsync(unit.ID);
                bool afterDelete = db.Units.Any(o => o.ID == unit.ID && (
                o.ElectricMeter != null ||
                o.WaterMeter != null ||
                o.ElectricMeterPrice != null ||
                o.WaterMeterPrice != null ||
                o.IsTransferElectricMeter != null ||
                o.IsTransferWaterMeter != null ||
                o.ElectricMeterTransferDate != null ||
                o.WaterMeterTransferDate != null ||
                o.ElectricMeterTopic != null ||
                o.WaterMeterTopic != null ||
                o.ElectricMeterRemark != null ||
                o.WaterMeterRemark != null)
                );
                Assert.False(afterDelete);

                await tran.RollbackAsync();
            });
        }
        [Fact]
        public async Task UpdateUnitMeterAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                //Put unit test here
                var model = await db.Units.FirstAsync();
                var service = new UnitMeterService(db);

                var modelEdit = UnitMeterDTO.CreateFromModel(model);
                modelEdit.WaterMeter = "UnitTest001";

                var resultEdit = await service.UpdateUnitMeterAsync(model.ID, modelEdit);
                Assert.NotNull(resultEdit);
                Assert.Equal("UnitTest001", resultEdit.WaterMeter);

                await tran.RollbackAsync();
            });
        }
        [Fact]
        public async Task ExportUnitMeterExcelAsync()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var service = new UnitMeterService(db);
                    UnitMeterFilter filter = new();
                    UnitMeterListSortByParam sortByParam = new();
                    var model = await db.Projects.FirstAsync();
                    filter.ProjectIDs = model.ID.ToString();
                    FileDTO result = await service.ExportUnitMeterExcelAsync(filter, sortByParam);
                    Assert.NotNull(result.Url);
                });
            }
        }
        [Fact]
        public async Task ExportUnitMeterStatusExcelAsync()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    var service = new UnitMeterService(db);
                    UnitMeterFilter filter = new();
                    UnitMeterListSortByParam sortByParam = new();

                    FileDTO result = await service.ExportUnitMeterStatusExcelAsync(filter, sortByParam);
                    Assert.NotNull(result.Url);
                });
            }
        }

    }
}
