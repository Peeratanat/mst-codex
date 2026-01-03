using Moq;
using Xunit;
using System.Threading.Tasks;
using PRJ_ProjectInfo.Services;
using Database.UnitTestExtensions;
using Microsoft.EntityFrameworkCore;
using PagingExtensions;
using PRJ_ProjectInfo.Params.Outputs;
using static PRJ_ProjectInfo.Params.Outputs.ProjectInformationPaging;
using Database.Models.PRJ;
namespace PRJ_ProjectInfo.Service.UnitTests
{
    public class ProjectServiceTest
    {
        private readonly Mock<IProjectService> _mockProjectService;
        private readonly IProjectService _projectService;

        public ProjectServiceTest()
        {
            // Create the mock instance
            _mockProjectService = new Mock<IProjectService>();

            // Use the mock object in your tests
            _projectService = _mockProjectService.Object;
        }

        [Fact]
        public async Task GetProjectInformationListAsync()
        {
            PageParam pageParam = new PageParam();
            ProjectInformationPaging.SortByParam sortByParam = new ProjectInformationPaging.SortByParam()
            {
                SortBy = SortBy.ProjectType
            };

            _mockProjectService.Setup(service =>
               service.GetProjectInformationListAsync(
                    It.IsAny<ProjectInformationPaging.Filter>(),
                    It.IsAny<ProjectInformationPaging.SortByParam>(),
                    It.IsAny<PageParam>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ProjectInformationPaging.APIResult
                {
                    DataResult = new List<Result> { new Result() }
                });
            // Act
            var results = await _projectService.GetProjectInformationListAsync(new ProjectInformationPaging.Filter(), sortByParam, pageParam);

            // Assert
            _mockProjectService.Verify(service =>
                service.GetProjectInformationListAsync(
                    It.IsAny<ProjectInformationPaging.Filter>(),
                    It.IsAny<ProjectInformationPaging.SortByParam>(),
                    It.IsAny<PageParam>(),
                    It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotNull(results.DataResult);
        }

        [Fact]
        public async Task GetProjectInformationDetailAsync()
        {
            PageParam pageParam = new PageParam();
            ProjectInformationPaging.SortByParam sortByParam = new ProjectInformationPaging.SortByParam()
            {
                SortBy = SortBy.ProjectType
            };

            _mockProjectService.Setup(service =>
               service.GetProjectInformationDetailAsync(
                   It.IsAny<Guid>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(new ProjectInformationModel.ResultProjectInformation());
            // Act
            var result = await _mockProjectService.Object.GetProjectInformationDetailAsync(Guid.NewGuid(), CancellationToken.None);

            // Assert
            _mockProjectService.Verify(service =>
                service.GetProjectInformationDetailAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotNull(result);
        }


        [Fact]
        public async Task GetProjectBrandDDLAsync()
        {
            PageParam pageParam = new PageParam();
            ProjectInformationPaging.SortByParam sortByParam = new ProjectInformationPaging.SortByParam()
            {
                SortBy = SortBy.ProjectType
            };

            var resultMock = new List<DropdownListModel>();
            _mockProjectService.Setup(service =>
               service.GetProjectBrandDDLAsync(
                   It.IsAny<Guid>(),
                   It.IsAny<string>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(new List<DropdownListModel>());
            // Act
            var result = await _mockProjectService.Object.GetProjectBrandDDLAsync(Guid.NewGuid(), "test", CancellationToken.None);

            // Assert
            _mockProjectService.Verify(service =>
                service.GetProjectBrandDDLAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(resultMock, result);

        }

        [Fact]
        public void GetProjectTypeDDL()
        {
            PageParam pageParam = new PageParam();
            ProjectInformationPaging.SortByParam sortByParam = new ProjectInformationPaging.SortByParam()
            {
                SortBy = SortBy.ProjectType
            };

            var resultMock = new List<DropdownListModel>();
            _mockProjectService.Setup(service =>
               service.GetProjectTypeDDL(
                   It.IsAny<string>()))
               .Returns(new List<DropdownListModel>());
            // Act
            var result = _mockProjectService.Object.GetProjectTypeDDL("test");

            // Assert
            _mockProjectService.Verify(service =>
                service.GetProjectTypeDDL(
                    It.IsAny<string>()), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(resultMock, result);
        }

        [Fact]
        public async Task GetProjectStatusDDLAsync()
        {
            PageParam pageParam = new PageParam();
            ProjectInformationPaging.SortByParam sortByParam = new ProjectInformationPaging.SortByParam()
            {
                SortBy = SortBy.ProjectType
            };

            var resultMock = new List<DropdownListModel>();
            _mockProjectService.Setup(service =>
               service.GetProjectStatusDDLAsync(
                   It.IsAny<string>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(new List<DropdownListModel>());
            // Act
            var result = await _mockProjectService.Object.GetProjectStatusDDLAsync("test", CancellationToken.None);

            // Assert
            _mockProjectService.Verify(service =>
                service.GetProjectStatusDDLAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(resultMock, result);

        }

        [Fact]
        public async Task GetProjectZoneDDLAsync()
        {
            PageParam pageParam = new PageParam();
            ProjectInformationPaging.SortByParam sortByParam = new ProjectInformationPaging.SortByParam()
            {
                SortBy = SortBy.ProjectType
            };

            var resultMock = new List<DropdownListModel>();
            _mockProjectService.Setup(service =>
               service.GetProjectZoneDDLAsync(
                   It.IsAny<string>(),
                   It.IsAny<CancellationToken>()))
               .ReturnsAsync(new List<DropdownListModel>());
            // Act
            var result = await _mockProjectService.Object.GetProjectZoneDDLAsync("test", CancellationToken.None);

            // Assert
            _mockProjectService.Verify(service =>
                service.GetProjectZoneDDLAsync(
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(resultMock, result);

        }

        [Fact]
        public async Task UpdateProjectAdminDescriptionAsync()
        {
            PageParam pageParam = new PageParam();
            ProjectInformationPaging.SortByParam sortByParam = new ProjectInformationPaging.SortByParam()
            {
                SortBy = SortBy.ProjectType
            };

            var resultMock = new bool();
            _mockProjectService.Setup(service =>
               service.UpdateProjectAdminDescriptionAsync(
                   It.IsAny<UpdateProjectInfoModel>()))
               .ReturnsAsync(new bool());
            // Act
            var result = await _mockProjectService.Object.UpdateProjectAdminDescriptionAsync(new UpdateProjectInfoModel());

            // Assert
            _mockProjectService.Verify(service =>
                service.UpdateProjectAdminDescriptionAsync(
                    It.IsAny<UpdateProjectInfoModel>()), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(resultMock, result);

        }
    }
}
