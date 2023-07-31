namespace Torc.Assesment.Api.Model
{
    public class LoggedUser
    {
        public string Token { get; set; } = string.Empty;
        public int Id { get; set; } = 0;
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
