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
using Xunit;

namespace WasteRecyclingManagementApi.Tests.ControllerTests
{
    public class AdminControllerTests
    {
        private readonly AdminController _adminController;

        private readonly IUnitOfWork _unitOfWork;

        public AdminControllerTests()
        {
            var connString = Helper.Instance.GetConnectionString();

            var services = new ServiceCollection();
            services.AddDbContext<RecyclingDbContext>(options => options.UseSqlServer(connString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ILocationWriteService, LocationWriteService>();

            var serviceProvider = services.BuildServiceProvider();

            _unitOfWork = serviceProvider.GetService<IUnitOfWork>();

            _adminController = new AdminController(serviceProvider.GetService<IAdminService>(),
                                                   serviceProvider.GetService<ILocationWriteService>());
        }

        [Fact]
        public async Task AddRecyclingPoint()
        {
            var recyclingPoint = new RecyclingPointViewModel
            {
                Name = "Recycling Point Test 2",
                Latitude = 40.3245345,
                Longitude = 23.5676543
            };

            ObjectResult result = (ObjectResult)await _adminController.AddRecyclingPoint(recyclingPoint);

            // delete the test recycling point from the database
            var recyclingPointToRemove = await _unitOfWork.RecyclingPointsRepository.GetRecyclingPointAsync(recyclingPoint.Name);
            if(recyclingPointToRemove != null)
            {
                _unitOfWork.RecyclingPointsRepository.Remove(recyclingPointToRemove);
                await _unitOfWork.CommitAsync();
            }

            Assert.Equal((int)HttpStatusCode.Created, result.StatusCode);
        }

        [Fact]
        public async Task NotAddRecyclingPointErrorDuplicate()
        {
            var recyclingPoint = new RecyclingPointViewModel
            {
                Name = "Recycling Point Test 2",
                Latitude = 40.3245345,
                Longitude = 23.5676543
            };

            ObjectResult result = (ObjectResult)await _adminController.AddRecyclingPoint(recyclingPoint);
            ObjectResult result2 = (ObjectResult)await _adminController.AddRecyclingPoint(recyclingPoint);

            // delete the test recycling point from the database
            var recyclingPointToRemove = await _unitOfWork.RecyclingPointsRepository.GetRecyclingPointAsync(recyclingPoint.Name);
            if (recyclingPointToRemove != null)
            {
                _unitOfWork.RecyclingPointsRepository.Remove(recyclingPointToRemove);
                await _unitOfWork.CommitAsync();
            }

            Assert.Equal((int)HttpStatusCode.BadRequest, result2.StatusCode);
        }

    }
}
