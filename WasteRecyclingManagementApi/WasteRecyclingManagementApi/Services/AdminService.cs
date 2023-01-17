using JetBrains.Annotations;
using WasteRecyclingManagementApi.Core;
using WasteRecyclingManagementApi.Core.Dtos;
using WasteRecyclingManagementApi.Core.Entities;
using WasteRecyclingManagementApi.Core.Services;
using WasteRecyclingManagementApi.Services.ErrorMessages;
using WasteRecyclingManagementApi.Services.MapHelper;

namespace WasteRecyclingManagementApi.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorMessageResponse?> ChangeUserRoleAsync(string username, int role)
        {
            var user = await _unitOfWork.UsersRepository.GetUserAsync(username);
            if (user == null)
            {
                string errorMessage = ErrorMessageHelper.GetUserNotFoundError(username);
                return new ErrorMessageResponse
                {
                    ErrorMessage = errorMessage
                };
            }

            user.Role = role;
            _unitOfWork.UsersRepository.Update(user);
            await _unitOfWork.CommitAsync();

            return null;
        }

        public async Task<IEnumerable<OperationDto>> GetAllOperationsAsync()
        {
            var containers = await _unitOfWork.ContainerRepository.GetContainersAsync();
            var operationDtos = new List<OperationDto>();
            foreach (var container in containers)
            {
                foreach (var operation in container.Operations)
                {
                    var user = await _unitOfWork.UsersRepository.GetUserAsync(operation.UserId);
                    if (user == null)
                        continue;

                    var operationDto = OperationMapper.MapToOperationDto(container, user, operation);

                    operationDtos.Add(operationDto);
                }
            }

            return operationDtos;
        }

        public async Task<ErrorMessageResponse?> RemoveRecyclingPoint([NotNull] int id)
        {
            RecyclingPoint? recyclingPointToBeRemoved = await _unitOfWork.RecyclingPointsRepository
                .GetRecyclingPointAsync(id);
            if(recyclingPointToBeRemoved == null)
            {
                string errorMessage = ErrorMessageHelper.GetRecyclingPointNotFoundError(id);
                return new ErrorMessageResponse
                {
                    ErrorMessage = errorMessage
                };
            }

            _unitOfWork.RecyclingPointsRepository.Remove(recyclingPointToBeRemoved);
            await _unitOfWork.CommitAsync();

            return null;
        }
    }
}
