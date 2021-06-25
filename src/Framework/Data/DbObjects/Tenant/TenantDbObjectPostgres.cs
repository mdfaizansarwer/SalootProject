﻿using Data.Entities.Identity;
using Date;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Data.DbObjects
{
    public class TenantDbObjectPostgres : ITenantDbObject
    {
        #region Fields

        private readonly ApplicationDbContext _applicationDbContext;

        protected readonly DbSet<Tenant> _dbSet;

        #endregion Fields

        #region Ctor

        public TenantDbObjectPostgres(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dbSet = _applicationDbContext.Set<Tenant>();
        }

        #endregion Ctor

        #region Methods

        public async Task<Tenant> GetTenantByUserAsync(int userId, CancellationToken cancellationToken)
        {
            var result = await _dbSet
                               .FromSqlRaw($"SELECT id , name , created_on FROM get_tenant_by_user({userId})")
                               .ToListAsync(cancellationToken);
            return result.FirstOrDefault();
        }

        #endregion
    }
}