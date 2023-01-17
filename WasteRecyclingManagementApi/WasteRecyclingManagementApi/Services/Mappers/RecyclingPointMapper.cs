using WasteRecyclingManagementApi.Core.Dtos;
using WasteRecyclingManagementApi.Core.Entities;

namespace WasteRecyclingManagementApi.Services.MapHelper
{
    public static class RecyclingPointMapper
    {
        public static RecyclingPointDto MapRecyclingPointToDto(RecyclingPoint recyclingPoint)
        {
            return new RecyclingPointDto
            {
                Id = recyclingPoint.Id,
                Name = recyclingPoint.Name,
                Latitude = recyclingPoint.Latitude,
                Longitude = recyclingPoint.Longitude,
                Containers = new List<ContainerDto>()
            };
        }

    }
}
