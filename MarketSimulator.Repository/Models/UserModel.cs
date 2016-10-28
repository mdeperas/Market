using System.ComponentModel.DataAnnotations;

namespace MarketSimulator.Repository.Models
{
	public class UserModel : EntityModelBase
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

		[Required]
		[Display(Name = "Amount of money")]
		[Range(1, int.MaxValue)]
		public int AmountOfMoney { get; set; }
	}
}