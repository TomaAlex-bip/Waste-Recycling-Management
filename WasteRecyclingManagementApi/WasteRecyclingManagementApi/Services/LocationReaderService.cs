using WasteRecyclingManagementApi.Core;
using WasteRecyclingManagementApi.Core.Dtos;
using WasteRecyclingManagementApi.Core.Services;
using WasteRecyclingManagementApi.Services.ErrorMessages;
using WasteRecyclingManagementApi.Services.MapHelper;

namespace WasteRecyclingManagementApi.Services
{
    public class LocationReaderService : ILocationReaderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LocationReaderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<RecyclingPointDto>> GetPublicRecyclingPointsAsync()
        {
            var recyclingPoints = await _unitOfWork.RecyclingPointsRepository.GetAsync();
            var recyclingPointDtos = new List<RecyclingPointDto>();
            foreach (var recyclingPoint in recyclingPoints)
            {
                var recyclingPointDto = RecyclingPointMapper.MapRecyclingPointToDto(recyclingPoint);

                recyclingPointDtos.Add(recyclingPointDto);
            }

            return recyclingPointDtos;
        }

        public async Task<RecyclingPointDto> GetRecyclingPointAsync(int id)
        {
            var recyclingPoint = await _unitOfWork.RecyclingPointsRepository.GetRecyclingPointAsync(id);
            if(recyclingPoint == null)
            {
                string errorMessage = ErrorMessageHelper.GetRecyclingPointNotFoundError(id);
                return new RecyclingPointDto
                {
                    Id = id,
                    ErrorMessage = new ErrorMessageResponse
                    {
                        ErrorMessage = errorMessage
                    }
                };
            }

            var recyclingPointDto = RecyclingPointMapper.MapRecyclingPointToDto(recyclingPoint);

            var containerDtos = new List<ContainerDto>();
            foreach(var container in recyclingPoint.Containers)
            {
                containerDtos.Add(ContainerMapper.MapContainerToDto(container));
            }
            recyclingPointDto.Containers = containerDtos;

            return recyclingPointDto;
        }

        public async Task<IEnumerable<RecyclingPointDto>> GetRecyclingPointsAsync()
        {
            var recyclingPoints = await _unitOfWork.RecyclingPointsRepository.GetRecyclingPointsAsync();
            var recyclingPointDtos = new List<RecyclingPointDto>();
            foreach (var recyclingPoint in recyclingPoints)
            {
                var recyclingPointDto = RecyclingPointMapper.MapRecyclingPointToDto(recyclingPoint);

                var containerDtos = new List<ContainerDto>();
                foreach (var container in recyclingPoint.Containers)
                {
                    var containerDto = ContainerMapper.MapContainerToDto(container);
                    containerDtos.Add(containerDto);
                }

                recyclingPointDto.Containers = containerDtos;

                recyclingPointDtos.Add(recyclingPointDto);
            }

            return recyclingPointDtos;
        }

        public async Task<IEnumerable<ContainerDto>> GetContainersAsync()
        {
            var containers = await _unitOfWork.ContainerRepository.GetContainersAsync();
            var containersDto = new List<ContainerDto>();
            foreach(var container in containers)
            {
                containersDto.Add(ContainerMapper.MapContainerToDto(container));
            }

            return containersDto;
        }

        public async Task<ContainerWithErrorDto> GetContainerAsync(int id)
        {
            var container = await _unitOfWork.ContainerRepository.GetContainerAsync(id);
            if (container == null)
            {
                string errorMessage = ErrorMessageHelper.GetContainerNotFoundError(id);
                return new ContainerWithErrorDto
                {
                    Id = id,
                    ErrorMessage = new ErrorMessageResponse
                    {
                        ErrorMessage = errorMessage
                    }
                };
            }

            var containerDto = ContainerMapper.MapContainerWithErrorToDto(container);

            return containerDto;
        }

    }
}
