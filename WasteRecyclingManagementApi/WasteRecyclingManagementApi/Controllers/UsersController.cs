using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WasteRecyclingManagementApi.Core.Dtos;
using WasteRecyclingManagementApi.Core.Services;
using WasteRecyclingManagementApi.Services.ErrorMessages;
using WasteRecyclingManagementApi.ViewModels;

namespace WasteRecyclingManagementApi.Controllers
{
    [Route("api/users")]
    [Authorize(Policy = "UsersApi")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILocationReaderService _locationReaderService;
        private readonly IUserService _userService;
        private readonly ITokenReaderService _tokenReaderService;
        private readonly IHttpContextAccessor _contextAccessor;

        public UsersController(ILocationReaderService locationReaderService,
                               ITokenReaderService tokenReaderService,
                               IUserService userService,
                               IHttpContextAccessor contextAccessor)
        {
            _locationReaderService = locationReaderService;
            _userService = userService;
            _tokenReaderService = tokenReaderService;
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        [Route("locations")]
        public async Task<IEnumerable<RecyclingPointDto>> GetRecyclingPoints()
        {
            return await _locationReaderService.GetRecyclingPointsAsync();
        }

        [HttpGet]
        [Route("locations/{id}")]
        public async Task<IActionResult> GetRecyclingPoint([FromRoute] int id)
        {
            var statusCode = (int)HttpStatusCode.OK;
            var recyclingPoint = await _locationReaderService.GetRecyclingPointAsync(id);
            if (recyclingPoint.ErrorMessage != null)
            {
                statusCode = (int)HttpStatusCode.NotFound;
            }

            return StatusCode(statusCode, recyclingPoint);
        }

        [HttpGet]
        [Route("containers")]
        public async Task<IEnumerable<ContainerDto>> GetContainers()
        {
            return await _locationReaderService.GetContainersAsync();
        }

        [HttpGet]
        [Route("containers/{id}")]
        public async Task<IActionResult> GetContainer([FromRoute] int id)
        {
            var statusCode = (int)HttpStatusCode.OK;
            var containerDto = await _locationReaderService.GetContainerAsync(id);
            if (containerDto.ErrorMessage != null)
            {
                statusCode = (int)HttpStatusCode.NotFound;
            }

            return StatusCode(statusCode, containerDto);
        }

        [HttpGet]
        [Route("operations")]
        public async Task<IActionResult> GetUserOperations()
        {
            var context = _contextAccessor.HttpContext;
            if (context == null)
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

            var userOperations = await _userService.GetUserOperationsAsync(userId);

            return StatusCode((int)HttpStatusCode.OK, userOperations);
        }

        [HttpPost]
        [Route("operation")]
        public async Task<IActionResult> MakeAnOperation(UserOperationViewModel operationViewModel)
        {
            var context = _contextAccessor.HttpContext;
            if (context == null)
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

            var userOperationDto = await _userService.MakeAnOperationAsync(
                                                    userId,
                                                    operationViewModel.RecyclingPointName,
                                                    operationViewModel.ContainerWasteType,
                                                    operationViewModel.WasteAmount);

            var statusCode = (int)HttpStatusCode.Created;
            if(userOperationDto.ErrorMessage != null)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
            }
           
            return StatusCode(statusCode, userOperationDto);
        }
    }
}
