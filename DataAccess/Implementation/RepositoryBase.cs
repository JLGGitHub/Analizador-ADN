using DataAccess.ContextDb;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Implementation
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class, new()
    {
        /// <summary>
        /// 
        /// </summary>
        public MainContext _context { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public RepositoryBase(MainContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expresion"></param>
        /// <returns></returns>
        public T Get(Expression<Func<T, bool>> expresion)
        {
            return _context.Set<T>().Find(expresion);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int? Create(T obj)
        {
            _context.Set<T>().Add(obj);
            return _context.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public T Search(Expression<Func<T, bool>> expression)
        {
            return this._context.Set<T>().Where(expression).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="include"></param>
        /// <returns></returns>
        public T Search(Expression<Func<T, bool>> expression, Expression<Func<T, object>> include)
        {
            return this._context.Set<T>().Where(expression).Include(include).FirstOrDefault();
        }
    }
}
