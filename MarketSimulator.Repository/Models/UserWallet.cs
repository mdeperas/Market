using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace MarketSimulator.Repository.Models
{
	public class UserWallet : EntityModelBase
	{
		public string UserDataId { get; set; }
		public string StockId { get; set; }
		[Display(Name = "Amount")]
		public string Amount { get; set; }

		[JsonIgnore]
		[ForeignKey("UserDataId")]
		public virtual UserData UserData { get; set; }

		[JsonIgnore]
		[ForeignKey("StockId")]
		public virtual Stock Stock { get; set; }
	}
}
