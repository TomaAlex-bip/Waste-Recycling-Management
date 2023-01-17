namespace WasteRecyclingManagementApi.Core.Dtos
{
    public class OperationDto
    {
        public string UserName { get; set; } = string.Empty;
        public string RecyclingPointName { get; set; } = string.Empty;
        public string OperationType { get; set; } = string.Empty;
        public string WasteType { get; set; } = string.Empty;
        public decimal WasteAmount { get; set; }
        public DateTime Date { get; set; }
    }
}
