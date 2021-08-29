using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.ContextDb
{
    public interface IMainContext
    {
        /// <summary>
        /// Metodo representarivo de DbContext para realizar los Mock Test.
        /// </summary>
        /// <typeparam name="TEntity">Entidad de Negocio</typeparam>
        /// <returns></returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        /// Metodo representarivo de DbContext para realizar los Mock Test.
        /// </summary>
        /// <typeparam name="TEntity">Entidad de Negocio</typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        int SaveChanges(bool acceptAllChangesOnSuccess);

        /// <summary>
        /// Metodo representarivo de DbContext para realizar los Mock Test.
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// Metodo representarivo de DbContext para realizar los Mock Test.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// Metodo representarivo de DbContext para realizar los Mock Test.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
