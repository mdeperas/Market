using System;
using System.Data.Entity;
using Market;
using MarketSimulator.Repository.IRepo;
using MarketSimulator.Repository.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MarketSimulator.Repository.Repo
{
	public class UnitOfWork : IDisposable, IUnitOfWork
	{
		private IMarketSimulatorContext dbContext;
		private AuthRepository authRepository;
		private BaseRepository<UserWallet> userWalletRepository;
		private BaseRepository<Stock> stockRepository;

		public bool IsLazyLoadingEnabled
		{
			get
			{
				return (this.dbContext as DbContext).Configuration.LazyLoadingEnabled;
			}

			set
			{
				(this.dbContext as DbContext).Configuration.LazyLoadingEnabled = value;
			}
		}

		private bool disposed = false;

		public UnitOfWork(IMarketSimulatorContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public BaseRepository<Stock> StockRepository
		{
			get
			{
				if (this.stockRepository == null)
				{
					this.stockRepository = new BaseRepository<Stock>(this.dbContext);
				}

				return this.stockRepository;
			}
		}

		public BaseRepository<UserWallet> UserWalletRepository
		{
			get
			{
				if (this.userWalletRepository == null)
				{
					this.userWalletRepository = new BaseRepository<UserWallet>(this.dbContext);
				}

				return this.userWalletRepository;
			}
		}

		public AuthRepository AuthRepository
		{
			get
			{
				if (this.authRepository == null)
				{
					this.authRepository = new AuthRepository(this.dbContext);
				}

				return this.authRepository;
			}
		}

		public void Save()
		{
			try
			{
				this.dbContext.SaveChanges();
			}
			catch (Exception e)
			{
				////TODO: Log errors in the database.
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.dbContext.Dispose();
				}

				this.disposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}