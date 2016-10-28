using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection;
using MarketSimulator.Repository.IRepo;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MarketSimulator.Repository.Models
{
	public class MarketSimulatorContext : IdentityDbContext<IdentityUser>, IMarketSimulatorContext
	{
		public MarketSimulatorContext()
			: base("DefaultConnection")
		{
			Database.SetInitializer<MarketSimulatorContext>(new MarketSimulatorInitializer());
		}

		public static MarketSimulatorContext Create()
		{
			return new MarketSimulatorContext();
		}

		public IDbSet<Stock> Stocks { get; set; }
		public IDbSet<UserModel> UserModels { get; set; }
		public IDbSet<UserWallet> UserWallets { get; set; }
	}

	public class MarketSimulatorInitializer : DropCreateDatabaseAlways<MarketSimulatorContext>
	{
		public List<Stock> StocksList { get; set; }

		public MarketSimulatorInitializer()
		{
			this.StocksList.AddRange(this.CreateStocks());
		}

		protected override void Seed(MarketSimulatorContext context)
		{
			base.Seed(context);

			SeedStocks(context);
		}

		private void SeedStocks(MarketSimulatorContext context)
		{
			foreach (var stock in StocksList)
			{
				context.Set<Stock>().Add(stock);
			}

			context.SaveChanges();
		}
		private List<Stock> CreateStocks()
		{
			List<Stock> StockList = new List<Stock>
			{
				new Stock { Name = "Future Processing", Code = "FP", Unit = "1", Price = "5.1414" },
				new Stock { Name = "FP Lab", Code = "FPL", Unit = "100", Price = "3.5361" },
				new Stock { Name = "Progress Bar", Code = "PGB", Unit = "1", Price = "4.3509" },
				new Stock { Name = "FP Coin", Code = "FPC", Unit = "50", Price = "17.5274" },
				new Stock { Name = "FP Adventure", Code = "FPA", Unit = "50", Price = "11.4476" },
				new Stock { Name = "Deadline 24", Code = "DL24", Unit = "100", Price = "5.2782" }
			};

			return StockList;
		}
	}
}