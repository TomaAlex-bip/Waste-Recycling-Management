using WasteRecyclingManagementApi.Core;
using WasteRecyclingManagementApi.Core.Dtos;
using WasteRecyclingManagementApi.Core.Entities;
using WasteRecyclingManagementApi.Core.Services;
using WasteRecyclingManagementApi.Services.ErrorMessages;

namespace WasteRecyclingManagementApi.Services
{
    public class LocationWriteService : ILocationWriteService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LocationWriteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ErrorMessageResponse?> AddContainersAsync(int recyclingPointId, IEnumerable<ContainerDto> containerDtos)
        {
            var recyclingPoint = await _unitOfWork.RecyclingPointsRepository.GetRecyclingPointAsync(recyclingPointId);
            if (recyclingPoint == null)
            {
                string errorMessage = ErrorMessageHelper.GetRecyclingPointNotFoundError(recyclingPointId);
                return new ErrorMessageResponse
                {
                    ErrorMessage = errorMessage
                };
            }

            var containerTypes = recyclingPoint.Containers.Select(x => x.Type).ToArray();

            foreach (var item in containerDtos)
            {
                var container = new Container
                {
                    Type = item.Type,
                    MeasureUnit = item.MeasureUnit,
                    TotalCapacity = item.TotalCapacity,
                    Occupied = 0
                };

                if (containerTypes.Contains(item.Type))
                {
                    string errorMessage = ErrorMessageHelper.GetContainerDuplicateError(item.Type, recyclingPointId);
                    return new ErrorMessageResponse
                    {
                        ErrorMessage = errorMessage
                    };
                }

                recyclingPoint.Containers.Add(container);
            }
            await _unitOfWork.CommitAsync();
            return null;
        }

        public async Task<RecyclingPointDto> AddRecyclingPointAsync(string name, double latitude, double longitude)
        {
            var recyclingPoint = new RecyclingPoint
            {
                Name = name,
                Latitude = latitude,
                Longitude = longitude
            };

            var recyclingPointDto = new RecyclingPointDto
            {
                Name = name,
                Longitude = longitude,
                Latitude = latitude
            };

            var recyclingPointDuplicate = await _unitOfWork.RecyclingPointsRepository.GetRecyclingPointAsync(name);
            if(recyclingPointDuplicate != null)
            {
                string errorMessage = ErrorMessageHelper.GetRecyclingPointDuplicateError(name);
                recyclingPointDto.ErrorMessage = new ErrorMessageResponse
                {
                    ErrorMessage = errorMessage
                };

                return recyclingPointDto;
            }

            await _unitOfWork.RecyclingPointsRepository.AddAsync(recyclingPoint);
            await _unitOfWork.CommitAsync();

            return recyclingPointDto;
        }
    }
}
