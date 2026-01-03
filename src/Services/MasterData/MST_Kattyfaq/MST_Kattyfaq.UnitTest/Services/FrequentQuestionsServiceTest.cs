using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.MST;
using Database.UnitTestExtensions;
using MST_Kattyfaq.Params.Filters;
using MST_Kattyfaq.Services;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;

namespace MST_Kattyfaq.UnitTests
{
    public class FrequentQuestionsServiceTest
    {
        public FrequentQuestionsServiceTest()
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
        public async Task GetQuestionFAQListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new FrequentQuestionsService(db);
                        QuestopnFAQFilter filter = FixtureFactory.Get().Build<QuestopnFAQFilter>().Create();
                        PageParam pageParam = new PageParam();
                        QuestionFAQSortByParam sortByParam = new QuestionFAQSortByParam();

                        var results = await service.GetQuestionFAQListAsync(filter, pageParam, sortByParam);

                        filter = new QuestopnFAQFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(QuestionFAQSortBy)).Cast<QuestionFAQSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new QuestionFAQSortByParam() { SortBy = item };
                            results = await service.GetQuestionFAQListAsync(filter, pageParam, sortByParam);
                            Assert.NotEmpty(results.FrequentQuestions);
                        }

                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task AddQuestionAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                QuestionFAQDTO input = FixtureFactory.Get().Build<QuestionFAQDTO>().Create();
                var service = new FrequentQuestionsService(db);
                var result = await service.AddQuestionAsync(input);
                Assert.NotNull(result);

                await tran.RollbackAsync();
            });
        }
        [Fact]
        public async Task EditLetterOfGuaranteeAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                var service = new FrequentQuestionsService(db);
                QuestionFAQDTO input = FixtureFactory.Get().Build<QuestionFAQDTO>().Create();
                var result = await service.AddQuestionAsync(input);
                result.Remark = "UnitTest";
                var resultEdit = await service.EditQuestionAsync(result);
                Assert.NotNull(result);
                Assert.Equal("UnitTest", result.Remark);

                await tran.RollbackAsync();
            });
        }
        [Fact]
        public async Task DeleteQuestionAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                var model = await db.AnswerFAQ.FirstOrDefaultAsync();

                var service = new FrequentQuestionsService(db);
                await service.DeleteQuestionAsync(new QuestionFAQDTO
                {
                    AnswerFAQ = new AnswerFAQDTO
                    {
                        ID = model.ID
                    }
                });
                bool afterDelete = db.AnswerFAQ.Any(o => o.ID == model.ID && o.IsDeleted == false);
                Assert.False(afterDelete);

                await tran.RollbackAsync();
            });
        }


        [Fact]
        public async Task GenTextFileQuestionAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new FrequentQuestionsService(db);


                        var answerFAQ = await db.AnswerFAQ.FirstOrDefaultAsync();


                        var result = await service.GenTextFileQuestionAsync(new List<Guid> { (Guid)answerFAQ.QuestionID });
                        Assert.NotNull(result);
                        await tran.RollbackAsync();
                    }
                });
            }
        }
    }
}
