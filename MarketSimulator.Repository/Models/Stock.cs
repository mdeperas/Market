using System.ComponentModel.DataAnnotations;

namespace MarketSimulator.Repository.Models
{
	public class Stock : EntityModelBase
	{
		[Display(Name = "Name")]
		public string Name { get; set; }

		[Display(Name = "Code")]
		public string Code { get; set; }

		[Display(Name = "Unit")]
		public string Unit { get; set; }

		[Display(Name = "Price")]
		public string Price { get; set; }
	}
}