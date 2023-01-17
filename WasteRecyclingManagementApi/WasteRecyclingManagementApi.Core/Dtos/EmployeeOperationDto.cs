namespace WasteRecyclingManagementApi.Core.Dtos
{
    public class EmployeeOperationDto
    {
        public string EmployeeName { get; set; } = string.Empty;
        public string RecyclingPointName { get; set; } = string.Empty;
        public string ContainerWasteType { get; set; } = string.Empty;
        public decimal CleanAmount { get; set; }
        public ErrorMessageResponse? ErrorMessage { get; set; }
    }
}
