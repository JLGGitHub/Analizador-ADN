using DataAccess.ContextDb;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BusinessRules
{
    public class BusinessRulesBase<T, TImplementacion> : IRepositoryBase<T> where T : class, new() where TImplementacion : IRepositoryBase<T>
    {
        protected readonly TImplementacion DaoBusiness;
        public MainContext _context => DaoBusiness._context;

        public BusinessRulesBase(TImplementacion daoBusiness)
        {
            this.DaoBusiness = daoBusiness; 
        }
        public T Search(Expression<Func<T, bool>> expression)
        {
            return DaoBusiness.Get(expression);
        }

        public T Search(Expression<Func<T, bool>> expression, Expression<Func<T, object>> include)
        {
            return DaoBusiness.Search(expression, include);
        }

        public int? Create(T obj)
        {
            return DaoBusiness.Create(obj);
        }

        public T Get(Expression<Func<T, bool>> expresion)
        {
            return DaoBusiness.Get(expresion);
        }
    }
}
