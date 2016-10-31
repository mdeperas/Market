using System.ComponentModel.DataAnnotations;
using MarketSimulator.Repository.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MarketSimulator.Repository.ViewModel
{
	public class UserModel
	{
		[Required]
		[Display(Name = "User name")]
		public string UserName { get; set; }

		[Required]
		[Display(Name="Password")]
		[DataType((DataType.Password))]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		public string Password { get; set; }

		[Display(Name="Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

		public UserWallet[] UserWallets { get; set; }
		public UserData UserData { get; set; }
	}
}