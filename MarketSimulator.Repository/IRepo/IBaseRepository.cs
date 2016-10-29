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
		IEnumerable<T> Get();
		T GetById(object id);
		void Insert(T entity);
		void Update(T entity);
	}
}
