namespace WasteRecyclingManagementApi.Core.Entities
{
    public class RecyclingPoint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<Container> Containers { get; set; }
    }
}
