using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MarketSimulator.Repository.Models
{
	public class UserWallet : EntityModelBase
	{
		public string UserModelId { get; set; }
		public string StockId { get; set; }
		[Display(Name = "Amount")]
		public string Amount { get; set; }

		[JsonIgnore]
		[ForeignKey("UserModelId")]
		public virtual UserModel UserModel { get; set; }

		[JsonIgnore]
		[ForeignKey("StockId")]
		public virtual Stock Stock { get; set; }
	}
}
