﻿using AutoMapper;
using Data.DbObjects;
using Data.Entities.Identity;
using Data.Interfaces;
using Date;
using System.Threading;
using System.Threading.Tasks;

namespace Data.DataProviders
{
    public class TenantDataProvider : DataProvider<Tenant>, ITenantDataProvider
    {
        #region Fields

        private readonly ITenantDbObject _tenantDbObject;

        #endregion

        #region Ctor

        public TenantDataProvider(ApplicationDbContext dbContext, IMapper mapper, ITenantDbObject tenantDbObject) : base(dbContext, mapper)
        {
            _tenantDbObject = tenantDbObject;
        }

        #endregion

        #region Methods

        public async Task<Tenant> GetTenantByUserAsync(int userId, CancellationToken cancellationToken)
        {
            return await _tenantDbObject.GetTenantByUserAsync(userId, cancellationToken);
        }

        public async Task<TDto> GetTenantByUserAsync<TDto>(int userId, CancellationToken cancellationToken) where TDto : class, IViewModel
        {
            var entity = await _tenantDbObject.GetTenantByUserAsync(userId, cancellationToken);
            return _mapper.Map<TDto>(entity);
        }

        #endregion
    }
}