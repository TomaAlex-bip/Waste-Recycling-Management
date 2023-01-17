namespace WasteRecyclingManagementApi.Core.Entities
{
    public class Container
    {
        public int Id { get; set; }
        public int RecyclingPointId { get; set; }
        public RecyclingPoint RecyclingPoint { get; set; }
        public string Type { get; set; }
        public string MeasureUnit { get; set; }
        public decimal TotalCapacity { get; set; }
        public decimal Occupied { get; set; }
        public List<Operation> Operations { get; set; }
    }
}
