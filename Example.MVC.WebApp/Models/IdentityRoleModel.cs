using Microsoft.AspNetCore.Identity;

namespace Example.MVC.WebApp.Models
{
	public class IdentityRoleModel
	{
		public IEnumerable<IdentityRole>? roles { get; set; }
		public string? role { get; set; }
	}
}
