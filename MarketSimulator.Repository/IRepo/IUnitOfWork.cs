using MarketSimulator.Repository.Models;
using MarketSimulator.Repository.Repo;

namespace MarketSimulator.Repository.IRepo
{
	public interface IUnitOfWork
	{
		AuthRepository AuthRepository { get; }
		BaseRepository<UserWallet> UserWalletRepository { get; }
		BaseRepository<Stock> StockRepository { get; }
		BaseRepository<UserData> UserDataRepository { get; }
		void Save();
		void Dispose();
	}
}
