using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Threading.Tasks;
using WasteRecyclingManagementApi.Controllers;
using WasteRecyclingManagementApi.Core;
using WasteRecyclingManagementApi.Core.Services;
using WasteRecyclingManagementApi.Data;
using WasteRecyclingManagementApi.Services;
using WasteRecyclingManagementApi.ViewModels;
using Moq;
using Xunit;

namespace WasteRecyclingManagementApi.Tests.ControllerTests
{
    public class EmployeeControllerTests
    {
        private const int EMPLOYEE_ID = 2;
        private readonly EmployeesController _employeeController;

       // private readonly Mock<IAdminService> _adminOperationService = new();

        public EmployeeControllerTests()
        {
            var tokenReaderServiceMock = new Mock<ITokenReaderService>();
            tokenReaderServiceMock.Setup(t => t.GetUserId("Bearer token")).Returns(EMPLOYEE_ID);

            var connString = Helper.Instance.GetConnectionString();
            
            var services = new ServiceCollection();
            services.AddDbContext<RecyclingDbContext>(options => options.UseSqlServer(connString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

            var serviceProvider = services.BuildServiceProvider();

            var httpContextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
            httpContextAccessor.HttpContext = new DefaultHttpContext();
            httpContextAccessor.HttpContext.Request.Headers.Authorization = "Bearer token";

            _employeeController = new EmployeesController(serviceProvider.GetService<IEmployeeService>(), 
                                                          tokenReaderServiceMock.Object,
                                                          httpContextAccessor);
        }

        [Fact]
        public async Task CleanContainer()
        {
            EmployeeOperationViewModel employeeOperationViewModel = new EmployeeOperationViewModel
            {
                ContainerWasteType = "Paper",
                RecyclingPointName = "Recycling Point Test"
            };

            ObjectResult result = (ObjectResult)await _employeeController.CleanContainer(employeeOperationViewModel);

            //_adminOperationService.Verify(x => x.ChangeUserRoleAsync(), Times.Once);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Fact]
        public async Task NotCleanContainerWasteTypeError()
        {
            EmployeeOperationViewModel employeeOperationViewModel = new EmployeeOperationViewModel
            {
                ContainerWasteType = "Inexistent",
                RecyclingPointName = "Recycling Point Test"
            };

            ObjectResult result = (ObjectResult)await _employeeController.CleanContainer(employeeOperationViewModel);

            Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
