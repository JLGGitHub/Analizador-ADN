using DataAccess.ContextDb;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IRepositoryBase<T> where T : class, new()
    {
        MainContext _context { get; }
        T Get(Expression<Func<T, bool>> expresion);
        int? Create(T obj);
        T Search(Expression<Func<T, bool>> expression);
        T Search(Expression<Func<T, bool>> expression, Expression<Func<T, object>> include);
    }
}
