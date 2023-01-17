using WasteRecyclingManagementApi.Core;
using WasteRecyclingManagementApi.Core.Dtos;
using WasteRecyclingManagementApi.Core.Entities;
using WasteRecyclingManagementApi.Core.Services;
using WasteRecyclingManagementApi.Services.ErrorMessages;

namespace WasteRecyclingManagementApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EmployeeOperationDto> CleanContainerAsync(int employeeId, string recyclingPointName, string containerWasteType)
        {
            var employeeOperationDto = new EmployeeOperationDto
            {
                EmployeeName = "",
                RecyclingPointName = recyclingPointName,
                ContainerWasteType = containerWasteType
            };
            var user = await _unitOfWork.UsersRepository.GetUserAsync(employeeId);
            if (user == null)
            {
                string errorMessage = ErrorMessageHelper.GetUserNotFoundError(employeeId);
                employeeOperationDto.ErrorMessage = new ErrorMessageResponse
                {
                    ErrorMessage = errorMessage
                };
                return employeeOperationDto;
            }

            var container = await _unitOfWork.ContainerRepository
                .GetContainerAsync(recyclingPointName, containerWasteType);
            if (container == null)
            {
                string errorMessage = ErrorMessageHelper.GetContainerNotFoundError(containerWasteType, recyclingPointName);
                employeeOperationDto.ErrorMessage = new ErrorMessageResponse
                {
                    ErrorMessage = errorMessage
                };
                return employeeOperationDto;
            }

            employeeOperationDto.CleanAmount = container.Occupied;
            employeeOperationDto.EmployeeName = user.Username;
            var operation = new Operation
            {
                Type = "clean",
                WasteAmount = container.Occupied,
                Status = ""
            };

            user.Operations.Add(operation);
            container.Operations.Add(operation);
            container.Occupied -= operation.WasteAmount;
            _unitOfWork.ContainerRepository.Update(container);

            await _unitOfWork.CommitAsync();

            return employeeOperationDto;
        }
    }
}
