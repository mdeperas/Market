using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using MarketSimulator.Repository.IRepo;
using MarketSimulator.Repository.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MarketSimulator.Repository.Repo
{
	public class BaseRepository<T> : IBaseRepository<T> where T : EntityModelBase
	{
		private readonly IMarketSimulatorContext context;
		private readonly DbSet<T> dbSet;

		public BaseRepository(IMarketSimulatorContext context)
		{
			this.context = context;
			this.dbSet = context.Set<T>();
		}

		public virtual T GetById(object id)
		{
			return this.dbSet.Find(id);
		}

		public virtual int GetItemsCount()
		{
			return this.dbSet.Count();
		}

		public virtual IEnumerable<T> Get(
			Expression<Func<T, bool>> filter = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			string includeProperties = "",
			int pageNumber = 1,
			int itemsPerPage = 2147483647)
		{

			if(dbSet == null)
			{
				return null;
			}

			IQueryable<T> query = dbSet;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(includeProperty);
			}

			return (orderBy != null ? orderBy(query.ToList().AsQueryable()) : query.OrderBy(q => q.Id)).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage);
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