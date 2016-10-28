using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MarketSimulator.Repository.IRepo
{
	public interface IBaseRepository<T>
	{
		void Delete(object id);
		void Delete(T entity);
		IEnumerable<T> Get(
			Expression<Func<T, bool>> filter = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			string includeProperties = "",
			int pageNumber = 1,
			int itemsPerPage = 2147483647);
		T GetById(object id);
		void Insert(T entity);
		void Update(T entity);
	}
}
