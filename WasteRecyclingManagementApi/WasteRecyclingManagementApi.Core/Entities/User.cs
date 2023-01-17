namespace WasteRecyclingManagementApi.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public List<Operation> Operations { get; set; }
    }
}
