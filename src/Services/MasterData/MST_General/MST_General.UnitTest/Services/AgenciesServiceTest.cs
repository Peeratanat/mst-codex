using AutoFixture;
using Base.DTOs;
using Base.DTOs.MST;
using CustomAutoFixture;
using Database.Models.MST;
using Database.Models.USR;
using Database.UnitTestExtensions;
using MST_General.Params.Filters;
using MST_General.Services;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MST_General.UnitTests
{
    public class AgentsServiceTest
    {
        [Fact]
        public async Task GetAgentListAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                var province = await db.Provinces.FirstAsync(o => !o.IsDeleted);
                var district = await db.Districts.FirstAsync(o => !o.IsDeleted && o.ProvinceID == province.ID);
                var subdistrict = await db.SubDistricts.FirstAsync(o => !o.IsDeleted && o.DistrictID == district.ID);
                IList<Agent> agenciesList = new List<Agent>()
                {
                        FixtureFactory.Get().Build<Agent>()
                            .With(o => o.NameTH, "สมชาย")
                            .With(o => o.NameEN, "Somchai")
                            .With(o => o.DistrictID,district.ID)
                            .With(o => o.ProvinceID,province.ID)
                            .With(o => o.SubDistrictID,subdistrict.ID)
                            .With(o => o.IsDeleted, false)
                            .Create(),
                        FixtureFactory.Get().Build<Agent>()
                            .With(o => o.NameTH, "สมหญิง")
                            .With(o => o.NameEN, "Somying")
                            .With(o => o.DistrictID,district.ID)
                            .With(o => o.ProvinceID,province.ID)
                            .With(o => o.SubDistrictID,subdistrict.ID)
                            .With(o => o.IsDeleted, false)
                            .Create()
                };
                await db.AddRangeAsync(agenciesList);
                await db.SaveChangesAsync();

                // Act
                var service = new AgentsService(db);
                AgentFilter filter = FixtureFactory.Get().Build<AgentFilter>().Create();
                PageParam pageParam = new PageParam();
                AgentSortByParam sortByParam = new AgentSortByParam();
                var results = await service.GetAgentListAsync(filter, pageParam, sortByParam);
                pageParam = new PageParam() { Page = 1, PageSize = 10 };
                filter = new AgentFilter();

                var sortByParams = Enum.GetValues(typeof(AgentSortBy)).Cast<AgentSortBy>();
                foreach (var item in sortByParams)
                {
                    sortByParam = new AgentSortByParam() { SortBy = item };
                    results = await service.GetAgentListAsync(filter, pageParam, sortByParam);
                    Assert.NotEmpty(results.Agents);
                }

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task CreateAgentAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                var input = FixtureFactory.Get().Build<AgentDTO>().Create();
                input.NameTH = "สมปอง ";
                input.NameEN = "Sompong-";
                input.Province = null;
                input.District = null;
                input.SubDistrict = null;
                input.TelNo = string.Empty;
                input.FaxNo = string.Empty;

                var service = new AgentsService(db);
                var result = await service.CreateAgentAsync(input);


                Assert.NotNull(result);
                await tran.RollbackAsync();
            });

        }

        [Fact]
        public async Task GetAgentAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                var input = FixtureFactory.Get().Build<AgentDTO>().Create();
                input.NameTH = "สมปอง";
                input.NameEN = "Sompong";
                input.Province = null;
                input.District = null;
                input.SubDistrict = null;
                input.TelNo = string.Empty;
                input.FaxNo = string.Empty;

                var service = new AgentsService(db);
                var resultCreate = await service.CreateAgentAsync(input);
                Assert.NotNull(resultCreate);

                await tran.RollbackAsync();
            });

        }

        [Fact]
        public async Task UpdateAgentAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                var data = FixtureFactory.Get().Build<Agent>()
                       .With(o => o.NameTH, "สมปอง")
                       .With(o => o.NameEN, "Sompong")
                       .With(o => o.IsDeleted, false)
                       .Create();
                await db.AddAsync(data);
                await db.SaveChangesAsync();
                var service = new AgentsService(db);
                var input = FixtureFactory.Get().Build<AgentDTO>().Create();
                input.NameTH = "สมพงษ์";
                input.NameEN = "Sompong";
                input.Province = null;
                input.District = null;
                input.SubDistrict = null;
                input.Id = data.ID;
                input.TelNo = string.Empty;
                input.FaxNo = string.Empty;

                var result = await service.UpdateAgentAsync(input.Id.Value, input);

                Assert.NotNull(result);

                await tran.RollbackAsync();
            });

        }

        [Fact]
        public async Task DeleteAgentAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();
                var data = FixtureFactory.Get().Build<Agent>()
                      .With(o => o.NameTH, "สมพงษ์")
                      .With(o => o.NameEN, "Sompong")
                      .With(o => o.IsDeleted, false)
                      .Create();
                await db.AddAsync(data);
                await db.SaveChangesAsync();
                var service = new AgentsService(db);

                bool beforeDelete = db.Agents.Any(o => o.ID == data.ID && o.IsDeleted == false);
                Assert.True(beforeDelete);
                await service.DeleteAgentAsync(data.ID);

                bool afterDelete = db.Agents.Any(o => o.ID == data.ID && o.IsDeleted == false);
                Assert.False(afterDelete);
                bool afterSubDelete = db.AgentEmployees.Any(o => o.AgentID == data.ID && o.IsDeleted == false);
                Assert.False(afterSubDelete);

                await tran.RollbackAsync();
            });

        }


    }

}
