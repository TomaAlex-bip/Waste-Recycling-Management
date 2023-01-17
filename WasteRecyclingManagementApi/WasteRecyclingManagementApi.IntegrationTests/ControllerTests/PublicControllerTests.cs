using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using WasteRecyclingManagementApi.Controllers;
using WasteRecyclingManagementApi.Core;
using WasteRecyclingManagementApi.Core.Dtos;
using WasteRecyclingManagementApi.Core.Services;
using WasteRecyclingManagementApi.Data;
using WasteRecyclingManagementApi.Services;
using WasteRecyclingManagementApi.ViewModels;
using Xunit;

namespace WasteRecyclingManagementApi.Tests.ControllerTests
{
    public class PublicControllerTests
    {
        private readonly PublicController _publicController;
        private readonly IUnitOfWork _unitOfWork;

        public PublicControllerTests()
        {
            var locationReaderServiceMock = new Mock<ILocationReaderService>();
            locationReaderServiceMock.Setup(p => p.GetPublicRecyclingPointsAsync()).ReturnsAsync(new List<RecyclingPointDto>());

            var connString = Helper.Instance.GetConnectionString();

            var services = new ServiceCollection();
            services.AddDbContext<RecyclingDbContext>(options => options.UseSqlServer(connString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IRegistrationService, RegistrationService>();
            
            var serviceProvider = services.BuildServiceProvider();

            _unitOfWork = serviceProvider.GetService<IUnitOfWork>();

            _publicController = new PublicController(serviceProvider.GetService<IRegistrationService>(), 
                                                     locationReaderServiceMock.Object);
        }

        [Theory]
        [InlineData("test1234", "test1234password", (int)HttpStatusCode.Created)]
        public async Task RegisterNewUser(string username, string password, int expectedStatusCode)
        {
            UserViewModel user = new UserViewModel
            {
                Username = username,
                Password = password
            };

            ObjectResult result = (ObjectResult)await _publicController.RegisterUser(user);
            var userToRemove = await _unitOfWork.UsersRepository.GetUserAsync(user.Username);
            if (userToRemove != null)
            {
                _unitOfWork.UsersRepository.Remove(userToRemove);
                await _unitOfWork.CommitAsync();
            }

            Assert.Equal(expectedStatusCode, result.StatusCode);
        }

        [Theory]
        [InlineData("test1234", "test1234password", (int)HttpStatusCode.BadRequest)]
        public async Task NotRegisterNewUserDuplicate(string username, string password, int expectedStatusCode)
        {
            UserViewModel user = new UserViewModel
            {
                Username = username,
                Password = password
            };

            await _publicController.RegisterUser(user);
            ObjectResult result = (ObjectResult)await _publicController.RegisterUser(user);
            var userToRemove = await _unitOfWork.UsersRepository.GetUserAsync(user.Username);
            if (userToRemove != null)
            {
                _unitOfWork.UsersRepository.Remove(userToRemove);
                await _unitOfWork.CommitAsync();
            }

            Assert.Equal(expectedStatusCode, result.StatusCode);
        }
    }
}
