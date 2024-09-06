namespace Example.MVC.WebApp.Models
{
	public class LoginRequestModel
	{
        public string? Username { get; set; }
        public string? Password { get; set; }

        public Exception? Exception { get; set; }
    }
}
