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
        public IMainContext _context => DaoBusiness._context;

        public BusinessRulesBase(TImplementacion daoBusiness)
        {
            this.DaoBusiness = daoBusiness; 
        }

        /// <summary>
        /// Busca por medio de una expresion algun registro en BD
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public T Search(Expression<Func<T, bool>> expression)
        {
            return DaoBusiness.Get(expression);
        }

        /// <summary>
        /// Crea un registro generico
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int? Create(T obj)
        {
            return DaoBusiness.Create(obj);
        }

        /// <summary>
        /// Consulta un registro por medio de una expresion 
        /// </summary>
        /// <param name="expresion"></param>
        /// <returns></returns>
        public T Get(Expression<Func<T, bool>> expresion)
        {
            return DaoBusiness.Get(expresion);
        }
    }
}
