using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WasteRecyclingManagementApi.Controllers;
using WasteRecyclingManagementApi.Core;
using WasteRecyclingManagementApi.Core.Services;
using WasteRecyclingManagementApi.Data;
using WasteRecyclingManagementApi.Services;
using WasteRecyclingManagementApi.ViewModels;
using Xunit;

namespace WasteRecyclingManagementApi.Tests.ControllerTests
{
    public class UserControllerTests
    {
        private const int USER_ID = 1;

        private readonly UsersController _usersController;
        private readonly IUnitOfWork _unitOfWork;

        public UserControllerTests()
        {
            var tokenReaderServiceMock = new Mock<ITokenReaderService>();
            tokenReaderServiceMock.Setup(t => t.GetUserId("Bearer token")).Returns(USER_ID);

            var connString = Helper.Instance.GetConnectionString();

            var services = new ServiceCollection();
            services.AddDbContext<RecyclingDbContext>(options => options.UseSqlServer(connString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILocationReaderService, LocationReaderService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

            var serviceProvider = services.BuildServiceProvider();

            var contextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
            contextAccessor.HttpContext = new DefaultHttpContext();
            contextAccessor.HttpContext.Request.Headers.Authorization = "Bearer token";

            _unitOfWork = serviceProvider.GetService<IUnitOfWork>();

            _usersController = new UsersController(serviceProvider.GetService<ILocationReaderService>(), 
                                                   tokenReaderServiceMock.Object, 
                                                   serviceProvider.GetService<IUserService>(), 
                                                   contextAccessor);
        }

        [Theory]
        [InlineData(1, (int)HttpStatusCode.OK)]
        [InlineData(-1, (int)HttpStatusCode.NotFound)]
        public async Task GetLocation(int id, int statusCode)
        {
            ObjectResult result = (ObjectResult)await _usersController.GetRecyclingPoint(id);

            Assert.Equal(statusCode, result.StatusCode);
        }

        [Theory]
        [InlineData(1, (int)HttpStatusCode.OK)]
        [InlineData(-1, (int)HttpStatusCode.NotFound)]
        public async Task GetContainer(int id, int statusCode)
        {
            ObjectResult result = (ObjectResult)await _usersController.GetContainer(id);

            Assert.Equal(statusCode, result.StatusCode);
        }

        [Theory]
        [InlineData("Recycling Point Test", "Paper", 1, (int)HttpStatusCode.Created)]
        [InlineData("Recycling Point inexistent", "Paper", 1, (int)HttpStatusCode.BadRequest)]
        [InlineData("Recycling Point Test", "Waste type inexistent", 1, (int)HttpStatusCode.BadRequest)]
        [InlineData("Recycling Point Test", "Paper", 999999, (int)HttpStatusCode.BadRequest)]
        public async Task MakeOperation(string recyclingPoint, string wasteType, decimal wasteAmout, int expectedStatusCode)
        {
            var operation = new UserOperationViewModel
            {
                RecyclingPointName = recyclingPoint,
                ContainerWasteType = wasteType,
                WasteAmount = wasteAmout
            };

            decimal containerFillAmount = 0;
            var containerToClean = await _unitOfWork.ContainerRepository
                .GetContainerAsync(operation.RecyclingPointName, operation.ContainerWasteType);
            if (containerToClean != null)
            {
                containerFillAmount = containerToClean.Occupied;
            }

            ObjectResult result = (ObjectResult)await _usersController.MakeAnOperation(operation);

            // clean
            if (containerToClean != null)
            {
                containerToClean.Occupied = containerFillAmount;
            }

            var userWithOperation = await _unitOfWork.UsersRepository.GetUserAsync(USER_ID);
            var userOperation = userWithOperation?.Operations.LastOrDefault();
            if(userOperation != null)
            {
                userWithOperation?.Operations.Remove(userOperation);
                await _unitOfWork.CommitAsync();
            }

            Assert.Equal(expectedStatusCode, result.StatusCode);
        }
    }
}
