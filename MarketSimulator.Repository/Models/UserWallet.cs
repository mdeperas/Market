using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MarketSimulator.Repository.ViewModel;
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
		[ForeignKey("UserModelId")]
		public virtual UserModel UserId { get; set; }

		[JsonIgnore]
		[ForeignKey("StockId")]
		public virtual Stock Stock { get; set; }
	}
}
