using AutoFixture;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using CustomAutoFixture;
using Auth_User.Services;
using Auth_User.Params.Filters;
using Base.DTOs.USR;
using Database.Models.PRJ;
using Database.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Auth_User.UnitTests
{
    public class UserServiceTest
    {
        private static readonly Fixture Fixture = new Fixture();


        [Fact]
        public async Task GetUserListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();

                var  DBQuery = factory.CreateDbQueryContext();
                
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new UserService(db , DBQuery);
                        UserFilter filter = FixtureFactory.Get().Build<UserFilter>().Create();
                        PageParam pageParam = new PageParam();
                        UserListSortByParam sortByParam = new UserListSortByParam();
                        var results = await service.GetUserListAsync(filter, pageParam, sortByParam);

                        var project = await db.Projects.FirstAsync();
                        filter = new UserFilter();
                        filter.AuthorizeProjectIDs = project.ID.ToString(); // when guid
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(UserListSortBy)).Cast<UserListSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new UserListSortByParam() { SortBy = item };
                            results = await service.GetUserListAsync(filter, pageParam, sortByParam);
                            Assert.NotEmpty(results.Users);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetUserForProjectListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var DBQuery = factory.CreateDbQueryContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new UserService(db , DBQuery);
                        var project = await db.Projects.FirstAsync();
                        UserFilter filter = FixtureFactory.Get().Build<UserFilter>().Create();
                        filter.AuthorizeProjectIDs = project.ID.ToString();
                        PageParam pageParam = new PageParam();
                        UserListSortByParam sortByParam = new UserListSortByParam();

                        var results = await service.GetUserForProjectListAsync(filter, pageParam, sortByParam);

                        filter = new UserFilter();
                        filter.AuthorizeProjectIDs = project.ID.ToString();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(UserListSortBy)).Cast<UserListSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new UserListSortByParam() { SortBy = item };
                            results = await service.GetUserForProjectListAsync(filter, pageParam, sortByParam);
                            Assert.NotEmpty(results.Users);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetUserDropdownListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var DBQuery = factory.CreateDbQueryContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new UserService(db, DBQuery);
                        string filter = FixtureFactory.Get().Build<string>().Create();
                        var user = await db.Users.FirstOrDefaultAsync();

                        var results = await service.GetUserDropdownListAsync(user.FirstName);
                        Assert.NotEmpty(results);
                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task GetKCashCardUserByProjectAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var DBQuery = factory.CreateDbQueryContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new UserService(db , DBQuery);
                        string filter = FixtureFactory.Get().Build<string>().Create();
                        var project = await db.Projects.FirstOrDefaultAsync();

                        var results = await service.GetKCashCardUserByProjectAsync(project.ID, null);
                        Assert.NotEmpty(results);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task GetUserAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var DBQuery = factory.CreateDbQueryContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new UserService(db , DBQuery);
                        string filter = FixtureFactory.Get().Build<string>().Create();
                        var user = await db.Users.FirstOrDefaultAsync();

                        var results = await service.GetUserAsync(user.ID);
                        Assert.NotNull(results);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task GetUserAppPermissionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var DBQuery = factory.CreateDbQueryContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new UserService(db , DBQuery);
                        string filter = FixtureFactory.Get().Build<string>().Create();
                        var user = await db.Users.FirstOrDefaultAsync();

                        var results = await service.GetUserAppPermissionAsync(user.ID);
                        Assert.NotNull(results);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
    }
}
