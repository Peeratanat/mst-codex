using AutoFixture;
using CustomAutoFixture;
using Base.DTOs.MST;
using Database.UnitTestExtensions;
using CTM_CustomerConsent.Params.Filters;
using CTM_CustomerConsent.Services;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using Base.DTOs.CTM;
using CTM_CustomerConsent.Services.CustomerConsentService;
using Newtonsoft.Json;

namespace CTM_CustomerConsent.UnitTests
{
    public class LetterOfGuaranteeServiceTest
    {


        [Fact]
        public async Task GetConSentList_Phone()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                var dbQuery = factory.CreateDbQueryContext();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var customerConsentService = new CustomerConsentService(db, dbQuery);


                        var filter = new ConsentFilter();
                        filter.Phonenumber = "0832458480";
                        var pageParam = new PageParam();
                        pageParam.Page = 1;
                        pageParam.PageSize = 10;
                        var sortByParam = new ConsentListSortByParam();
                        var result = await customerConsentService.GetConSentListAsync(filter, pageParam, sortByParam);
                        Assert.NotNull(result);


                        filter = new ConsentFilter();
                        filter.Email = "guestoftonign@hotmail.com";
                        pageParam = new PageParam();
                        pageParam.Page = 1;
                        pageParam.PageSize = 10;
                        sortByParam = new ConsentListSortByParam();
                        result = await customerConsentService.GetConSentListAsync(filter, pageParam, sortByParam);
                        Assert.NotNull(result);


                        filter = new ConsentFilter();
                        filter.FirstName = "วิฑูรย์";
                        pageParam = new PageParam();
                        pageParam.Page = 1;
                        pageParam.PageSize = 10;
                        result = await customerConsentService.GetConSentListAsync(filter, pageParam, sortByParam);
                        Assert.NotNull(result);


                        filter = new ConsentFilter();
                        filter.LastName = "จันทร์สว่าง";
                        pageParam = new PageParam();
                        pageParam.Page = 1;
                        pageParam.PageSize = 10;
                        result = await customerConsentService.GetConSentListAsync(filter, pageParam, sortByParam);
                        Assert.NotNull(result);

                        await tran.RollbackAsync();
                    }
                });
            }
        }

        [Fact]
        public async Task UpdateCustomerConsentAsync()
        {
            using (var factory = new UnitTestDbContextFactory())
            {
                var db = factory.CreateContext();
                var strategy = db.Database.CreateExecutionStrategy();
                var dbQuery = factory.CreateDbQueryContext();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var tran = await db.Database.BeginTransactionAsync())
                    {
                        var customerConsentService = new CustomerConsentService(db, dbQuery);

                        var json = "{'empoyeeName':'ttestet testt2','empoyeeNo':'XXXXX1','createBySystem':'User_Create1','consentType':{'id':'782925ac-6ce0-446f-b5c1-ad6c993ab61b','name':'ไม่ยินยอม','nameEN':'Not Consent','fullName':'ไม่ยินยอม/Not Consent','key':'NotConsent','order':1},'referentDataList':[{'referentID':'bd5998ad-cc37-4b9c-995a-421bc8d882ba','referentType':{'masterCenterGroup':null,'order':2,'name':'Contact','nameEN':'Contact','fullName':'Contact/Contact','key':'Contact','isActive':true,'id':'4b1a248b-963d-43eb-8084-5c80c1d6626c','updatedBy':null,'updated':'2020-12-01 10:00:37'}}]}";
                        var data = JsonConvert.DeserializeObject<UpdateCustomerConsentDTO>(json);

                        await customerConsentService.UpdateCustomerConsentAsync(data);

                        await tran.RollbackAsync();
                    }
                });
            }
        }


    }
}
