using Data.Entities.Identity;
using Data.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Data.DataProviders
{
    public interface ITenantDataProvider : IDataProvider<Tenant>
    {
        #region Asynchronous

        /// <summary>
        /// Asynchronously find the tenant from user.
        /// </summary>
        /// <param name="userId">Id of user for finding Tenant entity that user belongs to.</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result is the Tenant entity that user belongs to that mapped to TDto.
        /// </returns>
        Task<Tenant> GetTenantByUserAsync(int userId, CancellationToken cancellationToken);

        /// <summary>
        /// Asynchronously find the tenant from user that mapped to TDto.
        /// </summary>
        /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
        /// <param name="userId">Id of user for finding Tenant entity that user belongs to.</param>
        /// <param name="cancellationToken">A System.Threading.CancellationToken to observe while waiting for the task to complete.</param>
        /// <returns>A task that represents the asynchronous operation.
        /// The task result is the Tenant entity that user belongs to that mapped to TDto.
        /// </returns>
        Task<TDto> GetTenantByUserAsync<TDto>(int userId, CancellationToken cancellationToken) where TDto : class, IViewModel;

        #endregion Asynchronous

        #region Synchronous

        /// <summary>
        /// Find the tenant from user.
        /// </summary>
        /// <param name="userId">Id of user for finding Tenant entity that user belongs to.</param>
        /// <returns>Return the Tenant entity that user belongs to that mapped to TDto.
        /// </returns>
        Tenant GetTenantByUser(int userId);

        /// <summary>
        /// Find the tenant from user that mapped to TDto.
        /// </summary>
        /// <typeparam name="TDto">TDto is a destination type of mapping process that its source type is TEntity.</typeparam>
        /// <param name="userId">Id of user for finding Tenant entity that user belongs to.</param>
        /// <returns>Return the Tenant entity that user belongs to that mapped to TDto.
        /// </returns>
        TDto GetTenantByUser<TDto>(int userId) where TDto : class, IViewModel;

        #endregion Synchronous
    }
}
