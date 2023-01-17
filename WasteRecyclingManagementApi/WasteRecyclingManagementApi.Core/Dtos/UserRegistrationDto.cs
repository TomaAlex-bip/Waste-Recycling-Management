namespace WasteRecyclingManagementApi.Core.Dtos
{
    public class UserRegistrationDto
    {
        public string Username { get; set; } = String.Empty;
        public ErrorMessageResponse? ErrorMessage { get; set; }
    }
}
