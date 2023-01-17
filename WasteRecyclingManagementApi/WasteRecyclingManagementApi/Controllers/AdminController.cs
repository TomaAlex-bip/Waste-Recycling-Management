using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WasteRecyclingManagementApi.Core.Dtos;
using WasteRecyclingManagementApi.Core.Services;
using WasteRecyclingManagementApi.Services.MapHelper;
using WasteRecyclingManagementApi.ViewModels;

namespace WasteRecyclingManagementApi.Controllers
{
    [Route("api/admin")]
    [Authorize(Policy = "AdminApi")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly ILocationWriteService _locationWriteService;

        public AdminController(IAdminService adminService, 
                               ILocationWriteService locationWriteService)
        {
            _adminService = adminService;
            _locationWriteService = locationWriteService;
        }

        [HttpGet]
        [Route("operations")]
        public async Task<IEnumerable<OperationDto>> GetAllOperations()
        {
            return await _adminService.GetAllOperationsAsync();
        }

        [HttpPost]
        [Route("locations")]
        public async Task<IActionResult> AddRecyclingPoint(
            [FromBody]RecyclingPointViewModel recyclingPointViewModel)
        {
            var recyclingPointDto = await _locationWriteService
                .AddRecyclingPointAsync(recyclingPointViewModel.Name,
                                    recyclingPointViewModel.Latitude,
                                    recyclingPointViewModel.Longitude);

            var statusCode = (int)HttpStatusCode.Created;
            if(recyclingPointDto.ErrorMessage != null)
                statusCode = (int)HttpStatusCode.BadRequest;

            return StatusCode(statusCode, recyclingPointDto);
        }

        [HttpPost]
        [Route("locations/{id}")]
        public async Task<IActionResult> AddContainersAtRecyclingPoint(
            [FromRoute]int id, 
            [FromBody]IEnumerable<ContainerViewModel> containerViewModels)
        {
            var containerDtos = new List<ContainerDto>();
            foreach(var containerViewModel in containerViewModels)
            {
                containerDtos.Add(ContainerMapper.MapContainerViewModelToDto(containerViewModel));
            }

            var errorMessage = await _locationWriteService.AddContainersAsync(id, containerDtos);
            if (errorMessage != null)
                return StatusCode((int)HttpStatusCode.BadRequest, errorMessage);

            return StatusCode((int)HttpStatusCode.Created, containerDtos);
        }

        [HttpPut]
        [Route("employees")]
        public async Task<IActionResult> ChangeUserRole(
            [FromBody] EmployeeViewModel employeeViewModel)
        {
            var errorMessage = await _adminService
                .ChangeUserRoleAsync(employeeViewModel.Username, employeeViewModel.Role);

            if (errorMessage != null)
                return StatusCode((int)HttpStatusCode.BadRequest, errorMessage);
            
            return StatusCode((int)HttpStatusCode.OK, employeeViewModel);
        }

        [HttpDelete]
        [Route("locations/{id}")]
        public async Task<IActionResult> DeleteRecyclingPoint(
            [FromRoute] int id)
        {
            var result = await _adminService.RemoveRecyclingPoint(id);
            if(result == null)
                return StatusCode((int)HttpStatusCode.OK);

            return StatusCode((int)HttpStatusCode.BadRequest, result);
        }

    }
}
