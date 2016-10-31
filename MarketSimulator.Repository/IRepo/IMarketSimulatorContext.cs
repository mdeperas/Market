using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MarketSimulator.Repository.Models;

namespace MarketSimulator.Repository.IRepo
{
	public interface IMarketSimulatorContext : IDisposable
	{
		IDbSet<Stock> Stocks { get; set; }
		IDbSet<UserData> UserData { get; set; }
		IDbSet<UserWallet> UserWallets { get; set; }

		int SaveChanges();
		Database Database { get; }
		DbEntityEntry Entry(object entity);
	}
}
