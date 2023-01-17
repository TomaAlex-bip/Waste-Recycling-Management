using WasteRecyclingManagementApi.Core;
using WasteRecyclingManagementApi.Core.Dtos;
using WasteRecyclingManagementApi.Core.Services;
using WasteRecyclingManagementApi.Services.ErrorMessages;

namespace WasteRecyclingManagementApi.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegistrationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserRegistrationDto> RegisterUserAsync(string username, string password)
        {
            var userRegistrationDto = new UserRegistrationDto
            {
                Username = username
            };

            var userDuplicate = await _unitOfWork.UsersRepository.GetUserAsync(username);
            if(userDuplicate != null)
            {
                string errorMessage = ErrorMessageHelper.GetUserDuplicateError(username);
                userRegistrationDto.ErrorMessage = new ErrorMessageResponse
                {
                    ErrorMessage = errorMessage
                };
                return userRegistrationDto;
            }

            await _unitOfWork.UsersRepository.RegisterUserAsync(username, password);
            await _unitOfWork.CommitAsync();
            
            return userRegistrationDto;
        }
    }
}
