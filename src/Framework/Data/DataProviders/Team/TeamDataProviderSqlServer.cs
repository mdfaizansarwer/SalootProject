using AutoMapper;
using Data.Entities;
using Data.Interfaces;
using Date;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Data.DataProviders
{
    public class TeamDataProviderSqlServer : DataProvider<Team>, ITeamDataProvider
    {
        #region Ctor

        public TeamDataProviderSqlServer(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        #endregion Ctor

        #region Asynchronous Methods

        public async Task<IList<Team>> GetChildTeamAsync(int rootTeamId, CancellationToken cancellationToken)
        {
            return await _dbSet
                         .FromSqlRaw($"EXEC GetChildTeams @RootTeamId = {rootTeamId}")
                         .AsNoTracking()
                         .ToListAsync(cancellationToken);
        }

        public async Task<IList<TDto>> GetChildTeamAsync<TDto>(int rootTeamId, CancellationToken cancellationToken) where TDto : class, IListViewModel
        {
            var entities = await _dbSet
                                 .FromSqlRaw($"EXEC GetChildTeams @RootTeamId = {rootTeamId}")
                                 .AsNoTracking()
                                 .ToListAsync(cancellationToken);
            return _mapper.Map<IList<TDto>>(entities);
        }

        public async Task<Team> GetRootTeamAsync(int childTeamId, CancellationToken cancellationToken)
        {
            var result = await _dbSet
                               .FromSqlRaw($"EXEC GetRootTeam @ChildTeamId = {childTeamId}")
                               .AsNoTracking()
                               .ToListAsync(cancellationToken);

            return result.FirstOrDefault();
        }

        public async Task<TDto> GetRootTeamAsync<TDto>(int childTeamId, CancellationToken cancellationToken) where TDto : class, IViewModel
        {
            var result = await _dbSet
                               .FromSqlRaw($"EXEC GetRootTeam @ChildTeamId = {childTeamId}")
                               .AsNoTracking()
                               .ToListAsync(cancellationToken);
            return _mapper.Map<TDto>(result.FirstOrDefault());
        }

        public async Task<Team> GetRootTeamByUserAsync(int userId, CancellationToken cancellationToken)
        {
            var result = await _dbSet
                               .FromSqlRaw($"EXEC GetRootTeamByUser @InputUserId = {userId}")
                               .AsNoTracking()
                               .ToListAsync(cancellationToken);
            return result.FirstOrDefault();
        }


        public async Task<TDto> GetRootTeamByUserAsync<TDto>(int userId, CancellationToken cancellationToken) where TDto : class, IViewModel
        {
            var result = await _dbSet
                               .FromSqlRaw($"EXEC GetRootTeamByUser @InputUserId = {userId}")
                               .AsNoTracking()
                               .ToListAsync(cancellationToken);
            return _mapper.Map<TDto>(result.FirstOrDefault());
        }

        #endregion Asynchronous Methods

        #region Synchronous Methods

        public IList<Team> GetChildTeam(int rootTeamId)
        {
            return _dbSet
                   .FromSqlRaw($"EXEC GetChildTeams @RootTeamId = {rootTeamId}")
                   .AsNoTracking()
                   .ToList();
        }

        public IList<TDto> GetChildTeam<TDto>(int rootTeamId) where TDto : class, IListViewModel
        {
            var entities = _dbSet
                            .FromSqlRaw($"EXEC GetChildTeams @RootTeamId = {rootTeamId}")
                            .AsNoTracking()
                            .ToList();
            return _mapper.Map<IList<TDto>>(entities);
        }

        public Team GetRootTeam(int childTeamId)
        {
            return _dbSet
                   .FromSqlRaw($"EXEC GetRootTeam @ChildTeamId = {childTeamId}")
                   .AsNoTracking()
                   .ToList()
                   .FirstOrDefault();
        }

        public TDto GetRootTeam<TDto>(int childTeamId) where TDto : class, IViewModel
        {
            var result = _dbSet
                         .FromSqlRaw($"EXEC GetRootTeam @ChildTeamId = {childTeamId}")
                         .ToList()
                         .FirstOrDefault();
            return _mapper.Map<TDto>(result);
        }

        public Team GetRootTeamByUser(int userId)
        {
            return _dbSet
                   .FromSqlRaw($"EXEC GetRootTeamByUser @InputUserId = {userId}")
                   .AsNoTracking()
                   .ToList()
                   .FirstOrDefault();
        }

        public TDto GetRootTeamByUser<TDto>(int userId) where TDto : class, IViewModel
        {
            var result = _dbSet
                         .FromSqlRaw($"EXEC GetRootTeamByUser @InputUserId = {userId}")
                         .AsNoTracking()
                         .ToList()
                         .FirstOrDefault();
            return _mapper.Map<TDto>(result);
        }

        #endregion Synchronous
    }
}