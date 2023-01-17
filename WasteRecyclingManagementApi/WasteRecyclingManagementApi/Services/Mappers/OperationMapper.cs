using WasteRecyclingManagementApi.Core.Dtos;
using WasteRecyclingManagementApi.Core.Entities;

namespace WasteRecyclingManagementApi.Services.MapHelper
{
    public static class OperationMapper
    {
        public static OperationDto MapToOperationDto(Container container, User user, Operation operation)
        {
            return new OperationDto
            {
                RecyclingPointName = container.RecyclingPoint.Name,
                OperationType = operation.Type,
                UserName = user.Username,
                Date = operation.Date,
                WasteType = container.Type,
                WasteAmount = operation.WasteAmount
            };
        }
    }
}
