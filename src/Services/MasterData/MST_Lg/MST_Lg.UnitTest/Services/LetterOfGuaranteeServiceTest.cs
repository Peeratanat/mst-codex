using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.MST;
using Database.UnitTestExtensions;
using MST_Lg.Params.Filters;
using MST_Lg.Services;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using Confluent.Kafka;
using Common.Helper;

namespace MST_Lg.UnitTests
{
    public class LetterOfGuaranteeServiceTest
    {
        public LetterOfGuaranteeServiceTest()
        {

            Environment.SetEnvironmentVariable("minio_AccessKey", "XNTYE7HIMF6KK4BVEIXA");
            Environment.SetEnvironmentVariable("minio_DefaultBucket", "master-data");
            Environment.SetEnvironmentVariable("minio_PublicURL", "192.168.2.29:30050");
            Environment.SetEnvironmentVariable("minio_Endpoint", "192.168.2.29:9001");
            Environment.SetEnvironmentVariable("minio_SecretKey", "naD+esQ+uV7+xwfF3bPfAn5iC7C1XUyXeM8HkBlO");
            Environment.SetEnvironmentVariable("minio_TempBucket", "temp");
            Environment.SetEnvironmentVariable("minio_WithSSL", "false");


            Environment.SetEnvironmentVariable("Kafka_Bootstrapservers", "uatkafka01.ap-thai.com:29092,uatkafka02.ap-thai.com:29092,uatkafka03.ap-thai.com:29092");
            Environment.SetEnvironmentVariable("Kafka_SaslUsername", "metricsreporter");
            Environment.SetEnvironmentVariable("Kafka_SaslPassword", "password");
            Environment.SetEnvironmentVariable("Kafka_SslCaPem", "MIIDqTCCApGgAwIBAgIUBz5Yv61TUt7304B7QXnyUJxpWoMwDQYJKoZIhvcNAQELBQAwYzEeMBwGA1UEAwwVY2ExLnRlc3QuY29uZmx1ZW50LmlvMQ0wCwYDVQQLDARURVNUMRIwEAYDVQQKDAlDT05GTFVFTlQxETAPBgNVBAcMCFBhbG9BbHRvMQswCQYDVQQGEwJVUzAgFw0yMzA5MDEwNzI1MzZaGA8yMDUxMDExNjA3MjUzNlowYzEeMBwGA1UEAwwVY2ExLnRlc3QuY29uZmx1ZW50LmlvMQ0wCwYDVQQLDARURVNUMRIwEAYDVQQKDAlDT05GTFVFTlQxETAPBgNVBAcMCFBhbG9BbHRvMQswCQYDVQQGEwJVUzCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBALzq8Pgk7zf3YbC+ti/RlfkHz7pZW1UV1WeKfZ5h5EvrSnMcMFhEYpRHEazywbUGj1TEkpcaF60/pDa6jYgS2PMndBB0TDQAjeSV9s2S1DTw+63hxowqBa0rl18UzYNvDpX2zuOhXYdmhOd0u1J/aZ1KcHiEtmR4tnYlAV+Txb0BzjHAZ8G1WgUe3nUaOxk9PU0ty5xPQ1G/zEkDAzPMFMxHNYKTqQ2fwNebZEVsyhnNhtXQwcU+2yFil5Q+hbIy836aKYzLyphChw+bQ5XB2sjPh2NocCivh1VMRwk5yFQ9EFqqoodnL+wQHZUdlTKPfas6XrNip2V/DuW1K8jwEpsCAwEAAaNTMFEwHQYDVR0OBBYEFG/Zp4CwcXmPLlf1qHAQ9zJNiNX3MB8GA1UdIwQYMBaAFG/Zp4CwcXmPLlf1qHAQ9zJNiNX3MA8GA1UdEwEB/wQFMAMBAf8wDQYJKoZIhvcNAQELBQADggEBADsK+dcUYfjeBM/i1354Ayg0mj0rTfsQLiwuxnDwCNUmWqAXC+q0rwtGBRxnkJ76FkU6KGUEp0JaDMEQlJF7I/+FmpMnwhBucl4jmNKXH6vTvCIcIGRgyUgHMb5CKVB1ozdLo0ANoU0SHpmxZUuM1Bpd6d3FhZg89NZ8jrs/xDMr4Laiqk4PuIDmUjv6owK9YzvHagdvC8UbCFd5NQ1UgxkmC9bMeRkdzF4v+9wrRWiak4YamYE7e3wg92H2FbvoaN6bdNKfV1V6wsnUDcDc4GSBKaOA+8WpkeJNB5o0kHEJodsWKtRBFTCWumCcei0aVZQW1XM7QC/9ttAW428UaN4=");
            Environment.SetEnvironmentVariable("Kafka_SslCertificatePem", "MIIDTjCCAjYCFDmklSv7evYdswgxsPUZ9XmGh69kMA0GCSqGSIb3DQEBCwUAMGMxHjAcBgNVBAMMFWNhMS50ZXN0LmNvbmZsdWVudC5pbzENMAsGA1UECwwEVEVTVDESMBAGA1UECgwJQ09ORkxVRU5UMREwDwYDVQQHDAhQYWxvQWx0bzELMAkGA1UEBhMCVVMwIBcNMjMwOTAxMDc0NDI5WhgPMjA1MTAxMTYwNzQ0MjlaMGIxCzAJBgNVBAYTAlVTMQswCQYDVQQIDAJDQTESMBAGA1UEBwwJUGFsbyBBbHRvMRIwEAYDVQQKDAlDT05GTFVFTlQxDTALBgNVBAsMBFRFU1QxDzANBgNVBAMMBmRvdG5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAOisUel6ajk8WAnJ//RtIzl/8vvl50+A78ozzRUGuxcox+GFP/LTDKqMZyfnXvA90Ni9IuN/Nxr/gZ1sR9nIItAQS+ljkTsNhZQEVIS9NLloY056n7nHRFbTw6s62gn0NvfxIAhmI+ZgOC55sDyIBeKh1mrKFOhSI90bOZim2e+ON6Ph/ZzfiMjBfh6pqg0N2k19LUYuUGliHzqEyGEaf26+HJYIoi9wqQEW3y5PYNQdSv7K8LB48b1j//cUGXzkmAmzPpjFrkvVguctJRLmgPeqRW376QrhZBQ08liENZ3dLfAccDY3FtDWc/1o/6O7+dBkN08rg8BGwHtSnAhtLesCAwEAATANBgkqhkiG9w0BAQsFAAOCAQEAgyVMnp8X/8teQq13ohTJ3GPK9UHzVv+Ozkd2/FASGvjg1XIqQfr1sew17KfrrV/v0yBGzR7XSKsSIEQaBs76KlTvm+9vQ4xeH00CSEsEBlDp53jydLa3hFH+fOuKSrhwE2+xDQ/UZED9zifndY9/Qm9ztTDgxySZqTE2oLZEMT3OtKg5NQUkZry2IaFgH2o0v6XGdI6BoVQJoEOIpbo1hp7Efzetzgmgz9El89lWNBLXnUHzQ1r1kbHkaNQiJUeX/8I/VVAF0TJqBYiDaZoKsZrbTUcjUwz79M66CtiV62N/Q/RvoC8oUhCK1D3ebGEpbfBQrRanlC/WSjxK4tJ9Hw==");
            Environment.SetEnvironmentVariable("Kafka_SslKeyPem", "MIIEvQIBADANBgkqhkiG9w0BAQEFAASCBKcwggSjAgEAAoIBAQDorFHpemo5PFgJyf/0bSM5f/L75edPgO/KM80VBrsXKMfhhT/y0wyqjGcn517wPdDYvSLjfzca/4GdbEfZyCLQEEvpY5E7DYWUBFSEvTS5aGNOep+5x0RW08OrOtoJ9Db38SAIZiPmYDguebA8iAXiodZqyhToUiPdGzmYptnvjjej4f2c34jIwX4eqaoNDdpNfS1GLlBpYh86hMhhGn9uvhyWCKIvcKkBFt8uT2DUHUr+yvCwePG9Y//3FBl85JgJsz6Yxa5L1YLnLSUS5oD3qkVt++kK4WQUNPJYhDWd3S3wHHA2NxbQ1nP9aP+ju/nQZDdPK4PARsB7UpwIbS3rAgMBAAECggEADHfvi8t7lcWeEig5BygyEwrAQq93TdNjBjrwaogJzpvo2jUB0ElvDFSp0oGbnNQzNOotzHMsyM2YbI846nrHIs88wi0Lw1ZeKihxPwvEn4rwh2wa0yHSj7SZb7ufo5jquS66wLh4hAfz+smaoCPJ7PMvQlnB9RExys+CrqOCWkvykVXrz7GkZ1rZirG0IFmOCYGOyqHg8uhZhP2sVZLM3ZSn+IanrzkT+9EzrbkQ9vQOPZp+fNLc2MwfjMETenfYVeHj/XO5qbqjSGi345DeJqeH2wHsK/6E/mGDSty0MCcPyTsYM/YScYAqb78YUXOKY2n6gPSukfD+2ORl9WX7QQKBgQD+U7szzfIbJCG1CJpfGjbmCNTa5W9f0QDtpJpZSUQfXA/z6A0Z7Ovr2zv/YxaA1qn3jeP4fetYyl8cYj9eaILwJfStY7WCKJPB8IMSFZOs8PyGpT31IBlKgIowea0gQaCzceHP8cVRLSLyJWtvEtjjKAhDoCmOAKeN91RGrimiFQKBgQDqNB//oaC84L4LKREg0AUj3OMylS4PJye5EZndEhGtmnEbeQRDmoSBT3acOkG3wHAIGDX6lGw8i7tjOdChvwARBWeK1zKkc2rKp6X4b5CEK5Xx6m9JyoM3iwda6xYh1eBYpkGE7a6JBg/wpbp/nW9YoQsL9jXk8A3v5k7kF3+P/wKBgB++a7tHlVR16g8ih8IfD4MezxKTWJdpCuiehvVmA662Wvdim2AFBl1l+9Mglwp1wLk5aJ73eIyYlc6BJM+v08gNtMB/lYQtdGPclT+ImeoYGizkKxuRaha2fIkYZteD2X8MU9cUokBDlf0LVqGChLe1o3JtZ0JmKoO5vzcjPecRAoGBAJHnPzgF/Rkt+bcCMoV6knkxFulgPn16KykEahJrO1AnsucmkR18maflVUMml+JdpG2mh5o/9N4TPv4l+m+JigUQlNzOC+KfdJwjrSYEesecT6GSJxqVawGjP0XIxtT1ZPpVOOQTHnSGrk+BL5po/gD88uDU1eZ2FnipxeQbL4EJAoGAKsEeDRiDyGFU1sw3oig8evu9hCBO5zPrlByfNNCPXCtWUqwrZNhl40wzaPLyL6wrYZU+0Sxq5azZdC8kPGsZ26lZQm/ahZPN7gwzMYEtWS1Nu4AgR2wCEFOxeDLHdW6bAj2p3JQ1OEw3KnNWaqllyiDo18JYKSdunZDKR6y4XGw=");
            Environment.SetEnvironmentVariable("KAFKA_SCHEMAREGISTRYCONFIGURL", "uatschemaregistry01.ap-thai.com:8081");
            Environment.SetEnvironmentVariable("KAFKA_TOPIC_NEW_LG", "crm-new-lg");
            Environment.SetEnvironmentVariable("KAFKA_TOPIC_EDIT_LG", "crm-edit-lg");
            Environment.SetEnvironmentVariable("KAFKA_TOPIC_INACTIVE_LG", "crm-inactive-lg");
            Environment.SetEnvironmentVariable("KAFKA_TOPIC_ACTIVE_LG", "crm-active-lg");
            Environment.SetEnvironmentVariable("KAFKA_TOPIC_DELETE_LG", "crm-delete-lg");
        }
        [Fact]
        public async Task GetLetterOfGuaranteeAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                         

                        var service = new LetterOfGuaranteeService(db);
                        LetterOfGuaranteeFilter filter = FixtureFactory.Get().Build<LetterOfGuaranteeFilter>().Create();
                        PageParam pageParam = new PageParam();
                        LetterOfGuaranteeSortByParam sortByParam = new LetterOfGuaranteeSortByParam();

                        var results = await service.GetLetterOfGuaranteeAsync(filter, pageParam, sortByParam);

                        filter = new LetterOfGuaranteeFilter();
                        pageParam = new PageParam() { Page = 1, PageSize = 10 };

                        var sortByParams = Enum.GetValues(typeof(LetterOfGuaranteeSortBy)).Cast<LetterOfGuaranteeSortBy>();
                        foreach (var item in sortByParams)
                        {
                            sortByParam = new LetterOfGuaranteeSortByParam() { SortBy = item };
                            results = await service.GetLetterOfGuaranteeAsync(filter, pageParam, sortByParam);
                            Assert.NotEmpty(results.LetterOfGuarantee);
                        }


                        await tran.RollbackAsync();
                    }
                });
            }
        }
        [Fact]
        public async Task AddLetterOfGuaranteeAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                var project = await db.Projects.FirstOrDefaultAsync();
                DateTime now = DateTime.Now;
                LetterOfGuaranteeDTO input = FixtureFactory.Get().Build<LetterOfGuaranteeDTO>().Create();
                var service = new LetterOfGuaranteeService(db);
                var result = await service.AddLetterOfGuaranteeAsync(input);
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

                var service = new LetterOfGuaranteeService(db);
                var user = await db.Users.FirstOrDefaultAsync();
                var project = await db.Projects.FirstOrDefaultAsync();
                DateTime now = DateTime.Now;
                LetterOfGuaranteeDTO input = FixtureFactory.Get().Build<LetterOfGuaranteeDTO>().Create();
                var result = await service.AddLetterOfGuaranteeAsync(input);
                result.ExpireDate = now;
                var resultEdit = await service.EditLetterOfGuaranteeAsync(input, user.ID);
                Assert.NotNull(result);
                Assert.Equal(result.ExpireDate, now);

                await tran.RollbackAsync();
            });
        }
        [Fact]
        public async Task DeleteLetterOfGuaranteeAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                var model = await db.LetterGuarantees.FirstOrDefaultAsync();

                var service = new LetterOfGuaranteeService(db);
                await service.DeleteLetterOfGuaranteeAsync(new LetterOfGuaranteeDTO
                {
                    Id = model.ID
                });
                bool afterDelete = db.LetterGuarantees.Any(o => o.ID == model.ID && o.IsDeleted == false);
                Assert.False(afterDelete);

                await tran.RollbackAsync();
            });
        }
        [Fact]
        public async Task CancelLetterOfGuaranteeAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                var service = new LetterOfGuaranteeService(db);
                var user = await db.Users.FirstOrDefaultAsync();
                var project = await db.Projects.FirstOrDefaultAsync();
                DateTime now = DateTime.Now;
                LetterOfGuaranteeDTO input = FixtureFactory.Get().Build<LetterOfGuaranteeDTO>().Create();
                var result = await service.AddLetterOfGuaranteeAsync(input);
                result.ExpireDate = now;
                result.CancelRemark = "UnitTest";
                result.RefundAmount = 99999;

                var resultEdit = await service.CancelLetterOfGuaranteeAsync(input, user.ID);
                Assert.NotNull(result);
                Assert.Equal(result.ExpireDate, now);
                Assert.Equal(result.CancelRemark, "UnitTest");
                Assert.Equal(result.RefundAmount, 99999);

                await tran.RollbackAsync();
            });
        }

        [Fact]
        public async Task CancelCancelLetterOfGuaranteeAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                var service = new LetterOfGuaranteeService(db);
                var user = await db.Users.FirstOrDefaultAsync();
                var project = await db.Projects.FirstOrDefaultAsync();
                DateTime now = DateTime.Now;
                LetterOfGuaranteeDTO input = FixtureFactory.Get().Build<LetterOfGuaranteeDTO>().Create();
                var result = await service.AddLetterOfGuaranteeAsync(input);
                result.ExpireDate = now;
                result.CancelRemark = "UnitTest";
                result.RefundAmount = 99999;

                var resultEdit = await service.CancelCancelLetterOfGuaranteeAsync(input, user.ID);
                Assert.NotNull(result);
                Assert.Equal(result.ExpireDate, now);
                Assert.Equal(result.CancelRemark, "UnitTest");
                Assert.Equal(result.RefundAmount, 99999);

                await tran.RollbackAsync();
            });
        }
        public async Task GetLetterGuaranteeFileListAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var service = new LetterOfGuaranteeService(db);
                        var model = await db.LetterGuaranteeFiles.FirstOrDefaultAsync();
                        var results = await service.GetLetterGuaranteeFileListAsync(model.ID);
                        Assert.NotEmpty(results);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task DeleteLetterGuaranteeFileAsync()
        {
            using var factory = new UnitTestDbContextFactory();
            var db = factory.CreateContext();
            var strategy = db.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var tran = await db.Database.BeginTransactionAsync();

                var model = await db.LetterGuaranteeFiles.FirstOrDefaultAsync();

                var service = new LetterOfGuaranteeService(db);
                await service.DeleteLetterGuaranteeFileAsync(model.ID);
                bool afterDelete = db.LetterGuaranteeFiles.Any(o => o.ID == model.ID && o.IsDeleted == false);
                Assert.False(afterDelete);

                await tran.RollbackAsync();
            });
        }
    }
}
