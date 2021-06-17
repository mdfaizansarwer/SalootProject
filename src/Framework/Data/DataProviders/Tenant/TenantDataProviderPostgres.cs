using AutoMapper;
using Data.Entities.Identity;
using Data.Interfaces;
using Date;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Data.DataProviders
{
    public class TenantDataProviderPostgres : DataProvider<Tenant>, ITenantDataProvider
    {
        #region Ctor

        public TenantDataProviderPostgres(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        #endregion

        #region Asynchronous Methods

        public async Task<Tenant> GetTenantByUserAsync(int userId, CancellationToken cancellationToken)
        {
            var result = await _dbSet
                               .FromSqlRaw($"SELECT id , name , created_on FROM get_tenant_by_user({userId})")
                               .ToListAsync(cancellationToken);
            return result.FirstOrDefault();
        }

        public async Task<TDto> GetTenantByUserAsync<TDto>(int userId, CancellationToken cancellationToken) where TDto : class, IViewModel
        {
            var result = await _dbSet
                               .FromSqlRaw($"SELECT id , name , created_on FROM get_tenant_by_user({userId})")
                               .ToListAsync(cancellationToken);
            return _mapper.Map<TDto>(result.FirstOrDefault());
        }

        #endregion

        #region Synchronous Methods

        public Tenant GetTenantByUser(int userId)
        {
            return _dbSet
                   .FromSqlRaw($"SELECT id , name , created_on FROM get_tenant_by_user({userId})")
                   .ToList()
                   .FirstOrDefault();
        }

        public TDto GetTenantByUser<TDto>(int userId) where TDto : class, IViewModel
        {
            var result = _dbSet
                         .FromSqlRaw($"SELECT id , name , created_on FROM get_tenant_by_user({userId})")
                         .ToList()
                         .FirstOrDefault();
            return _mapper.Map<TDto>(result);
        }

        #endregion
    }
}
