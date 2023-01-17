namespace WasteRecyclingManagementApi.Core.Dtos
{
    public class ContainerWithErrorDto
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string MeasureUnit { get; set; } = string.Empty;
        public decimal TotalCapacity { get; set; }
        public decimal Occupied { get; set; }
        public ErrorMessageResponse? ErrorMessage { get; set; }
    }
}
