using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using MarketSimulator.Repository.IRepo;

namespace MarketSimulator.Repository.Repo
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class 
	{
		private readonly IMarketSimulatorContext context;
		private readonly DbSet<T> dbSet;

		public BaseRepository(IMarketSimulatorContext context)
		{
			this.context = context;
			this.dbSet = (context as DbContext).Set<T>();
		}

		public virtual T GetById(object id)
		{
			return this.dbSet.Find(id);
		}

		public virtual IEnumerable<T> Get()
		{
			return this.dbSet.ToList();
		}

		public virtual void Insert(T entity)
		{
			this.dbSet.Add(entity);
		}

		public virtual void Update(T entity)
		{
			this.dbSet.Attach(entity);
			this.context.Entry(entity).State = EntityState.Modified;
		}

		public virtual void Delete(object id)
		{
			T entity = this.dbSet.Find(id);
			Delete(entity);
		}

		public virtual void Delete(T entity)
		{
			if (context.Entry(entity).State == EntityState.Detached)
			{
				this.dbSet.Attach(entity);
			}

			this.dbSet.Remove(entity);
		}
	}
}