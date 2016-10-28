using Market;
using MarketSimulator.Repository.Models;
using MarketSimulator.Repository.Repo;

namespace MarketSimulator.Repository.IRepo
{
	public interface IUnitOfWork
	{
		AuthRepository AuthRepository { get; }
		BaseRepository<UserWallet> UserWalletRepository { get; }
		BaseRepository<Stock> StockRepository { get; }
		BaseRepository<UserModel> UserModelRepository { get; }
		void Save();
		void Dispose();
	}
}
