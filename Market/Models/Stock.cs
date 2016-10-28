using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Market.Models
{
	public class Stock
	{
		public int StockId { get; set; }
		public string Name { get; set; }
		public string Code { get; set; }
		public string Unit { get; set; }
		public string Price { get; set; }
	}
}