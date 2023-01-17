namespace WasteRecyclingManagementApi.Core.Entities
{
    public class Operation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ContainerId { get; set; }
        public Container Container { get; set; }
        public string Type { get; set; }
        public decimal WasteAmount { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
