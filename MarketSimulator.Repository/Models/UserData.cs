using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MarketSimulator.Repository.ViewModel;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;

namespace MarketSimulator.Repository.Models
{
	public class UserData : EntityModelBase
	{
		public string UserId { get; set; }

		[Required]
		[Display(Name = "Amount of money")]
		[Range(1, int.MaxValue)]
		public int AmountOfMoney { get; set; }

		[ForeignKey("UserId")]
		public virtual IdentityUser User { get; set; }
	}
}
