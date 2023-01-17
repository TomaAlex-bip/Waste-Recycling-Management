using WasteRecyclingManagementApi.Core.Dtos;
using WasteRecyclingManagementApi.Core.Entities;
using WasteRecyclingManagementApi.ViewModels;

namespace WasteRecyclingManagementApi.Services.MapHelper
{
    public static class ContainerMapper
    {
        public static ContainerDto MapContainerToDto(Container container)
        {
            return new ContainerDto
            {
                Id = container.Id,
                Type = container.Type,
                MeasureUnit = container.MeasureUnit,
                TotalCapacity = container.TotalCapacity,
                Occupied = container.Occupied
            };
        }

        public static ContainerWithErrorDto MapContainerWithErrorToDto(Container container)
        {
            return new ContainerWithErrorDto
            {
                Id = container.Id,
                Type = container.Type,
                MeasureUnit = container.MeasureUnit,
                TotalCapacity = container.TotalCapacity,
                Occupied = container.Occupied
            };
        }

        public static ContainerDto MapContainerViewModelToDto(ContainerViewModel containerViewModel)
        {
            return new ContainerDto
            {
                Type = containerViewModel.WasteType,
                MeasureUnit = containerViewModel.MeasureUnit,
                TotalCapacity = containerViewModel.TotalCapacity,
                Id = 0,
                Occupied = 0,
            };
        }
    }
}
