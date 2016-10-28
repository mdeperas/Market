using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MarketSimulator.Repository.Models;

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