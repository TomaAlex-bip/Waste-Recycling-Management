using JetBrains.Annotations;
using WasteRecyclingManagementApi.Core.Dtos;

namespace WasteRecyclingManagementApi.Core.Services
{
    /// <summary>
    /// Service for employee operations.
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Clean the specified container, and register an cleaning operation for the specified employee.
        /// </summary>
        /// <param name="employeeId">The id of the employee which cleaned the container</param>
        /// <param name="recyclingPointName">The Recycling Point name where the container is</param>
        /// <param name="containerWasteType">The type of the container</param>
        /// <returns>An EmployeeOperationDto which has a field of type ErrorMessageDto which can be null 
        /// if everything went well, or contains the encountered error message.</returns>
        [ItemNotNull]
        Task<EmployeeOperationDto> CleanContainerAsync([NotNull] int employeeId, 
                                                       [NotNull] string recyclingPointName, 
                                                       [NotNull] string containerWasteType);
    }
}
