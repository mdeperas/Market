using Market;
using MarketSimulator.Repository.Models;
using MarketSimulator.Repository.Repo;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MarketSimulator.Repository.IRepo
{
	public interface IUnitOfWork
	{
		bool IsLazyLoadingEnabled { get; set; }
		AuthRepository AuthRepository { get; }
		BaseRepository<UserWallet> UserWalletRepository { get; }
		BaseRepository<Stock> StockRepository { get; }
		void Dispose();
		void Save();
	}
}
