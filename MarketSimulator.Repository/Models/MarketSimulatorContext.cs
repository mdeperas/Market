using System.Collections.Generic;
using System.Data.Entity;
using MarketSimulator.Repository.IRepo;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MarketSimulator.Repository.Models
{
	public class   MarketSimulatorContext : IdentityDbContext<IdentityUser>, IMarketSimulatorContext
	{
		public MarketSimulatorContext()
			: base("AuthContext")
		{
			Database.SetInitializer<MarketSimulatorContext>(new MarketSimulatorInitializer());
		}

		public static MarketSimulatorContext Create()
		{
			return new MarketSimulatorContext();
		}

		public IDbSet<Stock> Stocks { get; set; }
		public IDbSet<UserData> UserData { get; set; }
		public IDbSet<UserWallet> UserWallets { get; set; }
	}

	public class MarketSimulatorInitializer : DropCreateDatabaseIfModelChanges<MarketSimulatorContext>
	{
		public List<Stock> StocksList { get; set; } = new List<Stock>();

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
				new Stock { Name = "Future Processing", Code = "FP", Unit = "1" },
				new Stock { Name = "FP Lab", Code = "FPL", Unit = "100" },
				new Stock { Name = "Progress Bar", Code = "PGB", Unit = "1" },
				new Stock { Name = "FP Coin", Code = "FPC", Unit = "50" },
				new Stock { Name = "FP Adventure", Code = "FPA", Unit = "50" },
				new Stock { Name = "Deadline 24", Code = "DL24", Unit = "100" }
			};

			return StockList;
		}
	}
}