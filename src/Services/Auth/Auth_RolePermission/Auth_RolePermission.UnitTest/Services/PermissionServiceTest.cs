using AutoFixture;
using Base.DTOs;
using Base.DTOs.MST;
using Base.DTOs.PRJ;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PagingExtensions;
using Auth_RolePermission.Params.Filters;
using Auth_RolePermission.Services;
using OfficeOpenXml;
using CustomAutoFixture;
using Database.Models.DbQueries.IDT;
using static Database.Models.DbQueries.IDT.sqlMenuAction;

namespace Auth_RolePermission.UnitTests
{
    public class PermissionServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();


        [Fact]
        public async Task GetRoleDropdownListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new PermissionService(db );
                        string filter = FixtureFactory.Get().Build<string>().Create();
                        var user = await db.Roles.FirstOrDefaultAsync();

                        var results = await service.GetRoleDropdownListAsync(user.Code);
                        Assert.NotEmpty(results);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task GetModuleDropdownListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new PermissionService(db );
                        var user = await db.Modules.FirstOrDefaultAsync();
                        var results = await service.GetModuleDropdownListAsync(user.Code);
                        Assert.NotEmpty(results);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetMenuDropdownListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new PermissionService(db );

                        var menu = await db.Menus.FirstOrDefaultAsync();

                        var results = await service.GetMenuDropdownListAsync(new List<Guid?> { menu.ModuleID }, menu.MenuCode);
                        Assert.NotEmpty(results);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task GetMenuActionDropdownListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new PermissionService(db );
                        var menu = await db.Menus.FirstOrDefaultAsync();
                        var menuAction = await db.MenuActions.FirstOrDefaultAsync();

                        var results = await service.GetMenuActionDropdownListAsync(new List<Guid?> { menu.ModuleID }, null, menuAction.MenuActionCode);
                        Assert.NotEmpty(results);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task GetUserRolesAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new PermissionService(db );

                        var user = await db.Users.FirstOrDefaultAsync(f => f.EmployeeNo == "AP006316");
                        var results = await service.GetUserRolesAsync(user.ID, user.EmployeeNo);
                        Assert.NotEmpty(results);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetUserMenuAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new PermissionService(db );

                        var user = await db.Users.FirstOrDefaultAsync(f => f.EmployeeNo == "AP006316");

                        var results = await service.GetUserMenuAsync(user.ID, null);
                        Assert.NotEmpty(results);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task GetUserMenuActionsAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new PermissionService(db );

                        var user = await db.Users.FirstOrDefaultAsync(f => f.EmployeeNo == "AP006316");

                        var results = await service.GetUserMenuActionsAsync(user.ID, null);
                        Assert.NotEmpty(results);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task GetUserDashboardAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new PermissionService(db );

                        var user = await db.Users.FirstOrDefaultAsync(f => f.EmployeeNo == "AP006316"); 
                        var results = await service.GetUserDashboardAsync(user.ID, "Sale");
                        Assert.NotEmpty(results);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task GetPermissionByRoleAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new PermissionService(db );

                        PermissionByRoleFilter filter = FixtureFactory.Get().Build<PermissionByRoleFilter>().Create();
                        PageParam pageParam = new PageParam();
                        PermissionByRoleSortByParam sortByParam = new PermissionByRoleSortByParam();

                        var results = await service.GetPermissionByRoleAsync(filter, pageParam, sortByParam);

                        filter = new PermissionByRoleFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(PermissionByRoleSortBy)).Cast<PermissionByRoleSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new PermissionByRoleSortByParam() { SortBy = item };
                            results = await service.GetPermissionByRoleAsync(filter, pageParam, sortByParam);
                            Assert.NotNull(results.PermissionByRole);
                        }
                        await tran.RollbackAsync();
                    }
                });
            }
        }

    }
}
