using AutoFixture;
using CustomAutoFixture;
using Base.DTOs;
using Base.DTOs.MST;
using Database.Models.MST;
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
    public class AgentEmployeesServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();
        [Fact]
        public async Task GetAgentEmployeeListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        // Act
                        var service = new AgentEmployeeService(db);

                        AgentEmployeeFilter filter = FixtureFactory.Get().Build<AgentEmployeeFilter>().Create();
                        PageParam pageParam = new PageParam();
                        AgentEmployeeSortByParam sortByParam = new AgentEmployeeSortByParam();

                        var results = await service.GetAgentEmployeeListAsync(filter, pageParam, sortByParam);

                        pageParam = new PageParam() { Page = 1, PageSize = 10 };
                        filter = new AgentEmployeeFilter();
                        var sortByParams = Enum.GetValues(typeof(AgentEmployeeSortBy)).Cast<AgentEmployeeSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new AgentEmployeeSortByParam() { SortBy = item };
                            results = await service.GetAgentEmployeeListAsync(filter, pageParam, sortByParam);
                            Assert.NotEmpty(results.AgentEmployees);
                        }


                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task CreateAgentEmployeeAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var ainput = FixtureFactory.Get().Build<AgentDTO>().Create();
                        ainput.NameTH = "สมปอง";
                        ainput.NameEN = "Sompong";
                        ainput.Province = null;
                        ainput.District = null;
                        ainput.SubDistrict = null;
                        ainput.TelNo = string.Empty;
                        ainput.FaxNo = string.Empty;

                        var aservice = new AgentsService(db);
                        var aresult = await aservice.CreateAgentAsync(ainput);

                        var input = FixtureFactory.Get().Build<AgentEmployeeDTO>().Create();
                        input.FirstName = "สมปอง";
                        input.LastName = "ใจหมาย";
                        input.AgentID = aresult.Id;

                        var service = new AgentEmployeeService(db);
                        var result = await service.CreateAgentEmployeeAsync(input);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }

        }

        [Fact]
        public async Task GetAgentEmployeeAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var ainput = FixtureFactory.Get().Build<AgentDTO>().Create();
                        ainput.NameTH = "สมปอง";
                        ainput.NameEN = "Sompong";
                        ainput.Province = null;
                        ainput.District = null;
                        ainput.SubDistrict = null;
                        ainput.TelNo = string.Empty;
                        ainput.FaxNo = string.Empty;

                        var aservice = new AgentsService(db);
                        var aresult = await aservice.CreateAgentAsync(ainput);

                        var input = FixtureFactory.Get().Build<AgentEmployeeDTO>().Create();
                        input.FirstName = "สมปอง";
                        input.LastName = "ใจหมาย";
                        input.AgentID = aresult.Id;

                        var service = new AgentEmployeeService(db);
                        var resultCreate = await service.CreateAgentEmployeeAsync(input);
                        var result = await service.GetAgentEmployeeAsync(resultCreate.Id.Value);
                        Assert.NotNull(result);


                        await tran.RollbackAsync();
                    }
                });
            }

        }

        [Fact]
        public async Task UpdateAgentEmployeeAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var ainput = FixtureFactory.Get().Build<AgentDTO>().Create();
                        ainput.NameTH = "สมปอง";
                        ainput.NameEN = "Sompong";
                        ainput.Province = null;
                        ainput.District = null;
                        ainput.SubDistrict = null;
                        ainput.TelNo = string.Empty;
                        ainput.FaxNo = string.Empty;

                        var aservice = new AgentsService(db);
                        var aresult = await aservice.CreateAgentAsync(ainput);

                        var data = FixtureFactory.Get().Build<AgentEmployee>()
                               .With(o => o.FirstName, "สมปอง")
                               .With(o => o.LastName, "ใจหมาย")
                               .With(o => o.IsDeleted, false)
                               .Create();

                        await db.AgentEmployees.AddAsync(data);
                        await db.SaveChangesAsync();

                        var service = new AgentEmployeeService(db);
                        var input = FixtureFactory.Get().Build<AgentEmployeeDTO>().Create();
                        input.FirstName = "สมพงษ์";
                        input.LastName = "ใจหมาย";
                        input.AgentID = aresult.Id;
                        input.Id = data.ID;

                        var result = await service.UpdateAgentEmployeeAsync(input.Id.Value, input);
                        Assert.NotNull(result);


                        await tran.RollbackAsync();
                    }
                });
            }

        }

        [Fact]
        public async Task DeleteAgentEmployeeAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var data = FixtureFactory.Get().Build<AgentEmployee>()
                              .With(o => o.FirstName, "สมปอง")
                               .With(o => o.LastName, "ใจหมาย")
                               .With(o => o.IsDeleted, false)
                               .Create();

                        await db.AgentEmployees.AddAsync(data);
                        await db.SaveChangesAsync();
                        var service = new AgentEmployeeService(db);


                        bool beforeDelete = db.AgentEmployees.Any(o => o.ID == data.ID && o.IsDeleted == false);
                        Assert.True(beforeDelete);
                        await service.DeleteAgentEmployeeAsync(data.ID);
                        bool afterDelete = db.AgentEmployees.Any(o => o.ID == data.ID && o.IsDeleted == false);
                        Assert.False(afterDelete);


                        await tran.RollbackAsync();
                    }
                });
            }

        }
    }
}
