using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTestProyect
{
    public abstract class BaseTest<INegocio>
        where INegocio : class
    {
        protected static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class, new()
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>() { CallBase = true };
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            dbSet.As<System.Data.Entity.IDbSet<T>>().Setup(m => m.Create()).Returns(new T());
            dbSet.As<System.Data.Entity.IDbSet<T>>().Setup(m => m.Add(It.IsAny<T>())).Returns<T>(i => { sourceList.Add(i); return i; });
            dbSet.Setup(m => m.AddRange(It.IsAny<IEnumerable<T>>()));
            dbSet.As<System.Data.Entity.IDbSet<T>>().Setup(m => m.Remove(It.IsAny<T>())).Returns<T>(i => { sourceList.Remove(i); return i; });
            dbSet.Setup(m => m.RemoveRange(It.IsAny<IEnumerable<T>>()));
            dbSet.Setup(x => x.Add(It.IsAny<T>())).Returns<T>(i => { sourceList.Add(i); return null; });
            dbSet.Setup(x => x.Remove(It.IsAny<T>())).Returns<T>(i => { sourceList.Remove(i); return null; });

            return dbSet.Object;
        }

        protected virtual void AddAdaptadorMock() { }
    }
}
