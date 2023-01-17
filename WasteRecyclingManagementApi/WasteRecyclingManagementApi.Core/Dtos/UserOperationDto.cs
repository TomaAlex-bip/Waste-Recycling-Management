namespace WasteRecyclingManagementApi.Core.Dtos
{
    public class UserOperationDto
    {
        public string Username { get; set; }
        public string RecyclingPointName { get; set; }
        public string ContainerWasteType { get; set; }
        public decimal WasteAmount { get; set; }
        public ErrorMessageResponse? ErrorMessage { get; set; }
    }
}
