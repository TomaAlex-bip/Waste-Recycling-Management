namespace WasteRecyclingManagementApi.Core.Dtos
{
    public class RecyclingPointDto
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public IEnumerable<ContainerDto>? Containers { get; set; } = null;
        public ErrorMessageResponse? ErrorMessage { get; set; }
    }
}
