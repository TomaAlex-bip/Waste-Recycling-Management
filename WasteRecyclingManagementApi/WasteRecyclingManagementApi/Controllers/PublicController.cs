using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WasteRecyclingManagementApi.Core.Dtos;
using WasteRecyclingManagementApi.Core.Services;
using WasteRecyclingManagementApi.ViewModels;

namespace WasteRecyclingManagementApi.Controllers
{
    [Route("api/public")]
    [Authorize(Policy = "PublicApi")]
    [ApiController]
    public class PublicController : ControllerBase
    {
        private IRegistrationService _registrationService;
        private ILocationReaderService _locationReaderService;

        public PublicController(IRegistrationService registrationService, 
                                ILocationReaderService locationReaderService)
        {
            _registrationService = registrationService;
            _locationReaderService = locationReaderService;
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> RegisterUser(UserViewModel userViewModel)
        {
            var userRegistrationDto = await _registrationService
                .RegisterUserAsync(userViewModel.Username, userViewModel.Password);

            var statusCode = (int)HttpStatusCode.Created;
            if(userRegistrationDto.ErrorMessage != null)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
            }

            return StatusCode(statusCode, userRegistrationDto);
        }

        [HttpGet]
        [Route("locations")]
        public async Task<IEnumerable<RecyclingPointDto>> GetRecyclingPoints()
        {
            return await _locationReaderService.GetPublicRecyclingPointsAsync();
        }
    }
}
