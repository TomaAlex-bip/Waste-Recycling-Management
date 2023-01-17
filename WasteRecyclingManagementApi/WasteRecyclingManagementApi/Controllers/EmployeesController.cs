using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WasteRecyclingManagementApi.Core.Dtos;
using WasteRecyclingManagementApi.Core.Services;
using WasteRecyclingManagementApi.Services.ErrorMessages;
using WasteRecyclingManagementApi.ViewModels;

namespace WasteRecyclingManagementApi.Controllers
{
    [Route("api/employees")]
    [Authorize(Policy = "EmployeesApi")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ITokenReaderService _tokenReaderService;

        public EmployeesController(IEmployeeService employeeService,
                                   ITokenReaderService tokenReaderService,
                                   IHttpContextAccessor contextAccessor)
        {
            _employeeService = employeeService;
            _contextAccessor = contextAccessor;
            _tokenReaderService = tokenReaderService;
        }

        [HttpPost]
        [Route("cleaning")]
        public async Task<IActionResult> CleanContainer(
            [FromBody] EmployeeOperationViewModel operationViewModel)
        {
            var context = _contextAccessor.HttpContext;
            if(context == null)
            {
                var errorMessage = ErrorMessageHelper.GetTokenToProvidedError();
                var errorMessageDto = new ErrorMessageResponse
                {
                    ErrorMessage = errorMessage
                };
                return StatusCode((int)HttpStatusCode.BadRequest, errorMessageDto);
            }

            string token = context.Request.Headers.Authorization;
            int userId = _tokenReaderService.GetUserId(token);

            var operationDto = await _employeeService
                .CleanContainerAsync(userId, 
                                     operationViewModel.RecyclingPointName, 
                                     operationViewModel.ContainerWasteType);

            var statusCode = (int)HttpStatusCode.OK;
            if(operationDto.ErrorMessage != null)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
            }

            return StatusCode(statusCode, operationDto);
            
        }
    }
}
