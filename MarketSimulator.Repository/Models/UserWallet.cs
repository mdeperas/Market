using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MarketSimulator.Repository.ViewModel;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;

namespace MarketSimulator.Repository.Models
{
	public class UserWallet : EntityModelBase
	{
		public string UserId { get; set; }
		public string StockId { get; set; }
		[Display(Name = "Amount")]
		public string Amount { get; set; }

		[JsonIgnore]
		[ForeignKey("UserId")]
		public virtual IdentityUser User { get; set; }

		[JsonIgnore]
		[ForeignKey("StockId")]
		public virtual Stock Stock { get; set; }
	}
}
