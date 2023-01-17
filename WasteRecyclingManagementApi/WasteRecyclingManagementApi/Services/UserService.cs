using WasteRecyclingManagementApi.Core;
using WasteRecyclingManagementApi.Core.Dtos;
using WasteRecyclingManagementApi.Core.Entities;
using WasteRecyclingManagementApi.Core.Services;
using WasteRecyclingManagementApi.Services.ErrorMessages;

namespace WasteRecyclingManagementApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OperationDto>?> GetUserOperationsAsync(int userId)
        {
            var user = await _unitOfWork.UsersRepository.GetUserAsync(userId);
            if (user == null)
                return null;

            var operationDtos = new List<OperationDto>();
            foreach (var operation in user.Operations)
            {
                var operationDto = MapHelper.OperationMapper.MapToOperationDto(operation.Container, user, operation);
                operationDtos.Add(operationDto);
            }

            return operationDtos;
        }

        public async Task<UserOperationDto> MakeAnOperationAsync(int userId, string recyclingPointName, 
                                                                 string containerWasteType, decimal wasteAmount)
        {
            var userOperationDto = new UserOperationDto
            {
                Username = "",
                WasteAmount = wasteAmount,
                RecyclingPointName = recyclingPointName,
                ContainerWasteType = containerWasteType
            };
            var user = await _unitOfWork.UsersRepository.GetUserAsync(userId);
            if (user == null)
            {
                string errorMessage = ErrorMessageHelper.GetUserNotFoundError(userId);
                userOperationDto.ErrorMessage = new ErrorMessageResponse
                {
                    ErrorMessage = errorMessage
                };
                return userOperationDto;
            }

            userOperationDto.Username = user.Username;

            var container = await _unitOfWork.ContainerRepository
                .GetContainerAsync(recyclingPointName, containerWasteType);
            if (container == null)
            {
                string errorMessage = ErrorMessageHelper.GetContainerNotFoundError(containerWasteType, recyclingPointName);
                userOperationDto.ErrorMessage = new ErrorMessageResponse
                {
                    ErrorMessage = errorMessage
                };
                return userOperationDto;
            }

            if(userOperationDto.WasteAmount > (container.TotalCapacity - container.Occupied))
            {
                string errorMessage = ErrorMessageHelper.GetContainerCapacityError();
                userOperationDto.ErrorMessage = new ErrorMessageResponse
                {
                    ErrorMessage = errorMessage
                };
                return userOperationDto;
            }

            var operation = new Operation
            {
                Type = "deposit",
                WasteAmount = wasteAmount,
                Status = ""
            };

            user.Operations.Add(operation);
            container.Operations.Add(operation);
            container.Occupied += operation.WasteAmount;
            _unitOfWork.ContainerRepository.Update(container);

            await _unitOfWork.CommitAsync();
            return userOperationDto;
        }
    }
}
