using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MarketSimulator.Repository.Models
{
	public class UserData : IdentityUser
	{
		[Required]
		[Display(Name = "Amount of money")]
		[Range(1, int.MaxValue)]
		public int AmountOfMoney { get; set; }
	}
}
