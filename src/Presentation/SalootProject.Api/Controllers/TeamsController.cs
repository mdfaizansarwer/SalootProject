using Data.DataProviders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalootProject.Api.Midllewares;
using Services.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SalootProject.Api.Controllers
{
    public class TeamsController : BaseController
    {
        #region Fields

        private readonly TeamService _teamService;

        #endregion Fields

        #region Ctor

        public TeamsController(TeamService teamService, ITeamDataProvider teamDataProvider, ITenantDataProvider tenantDataProvider)
        {
            _teamService = teamService;
        }

        #endregion Ctor

        #region Actions

        [HttpGet, Authorize(Roles = "Admin,SystemUser")]
        public async Task<ApiResponse<List<TeamListViewModel>>> GetAllTeams(CancellationToken cancellationToken)
        {
            var teamDtos = await _teamService.GetAllTeamsAsync(cancellationToken);
            return Ok(teamDtos);
        }

        [HttpGet("{id:int:min(1)}"), Authorize(Roles = "Admin,SystemUser")]
        public async Task<ApiResponse<TeamViewModel>> GetTeamsById(int id, CancellationToken cancellationToken)
        {
            var teamDto = await _teamService.GetTeamsByIdAsync(id, cancellationToken);
            return Ok(teamDto);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ApiResponse> CreateTeam(TeamCreateUpdateViewModel teamCreateOrUpdateViewModel, CancellationToken cancellationToken)
        {
            await _teamService.CreateAsync(teamCreateOrUpdateViewModel, cancellationToken);
            return Ok();
        }

        [HttpPut("{id:int:min(1)}"), Authorize(Roles = "Admin")]
        public async Task<ApiResponse> UpdateTeam(int id, TeamCreateUpdateViewModel teamCreateOrUpdateViewModel, CancellationToken cancellationToken)
        {
            await _teamService.UpdateAsync(id, teamCreateOrUpdateViewModel, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id:int:min(1)}"), Authorize(Roles = "Admin")]
        public async Task<ApiResponse> DeleteTeam(int id, CancellationToken cancellationToken)
        {
            await _teamService.DeleteAsync(id, cancellationToken);
            return Ok();
        }

        #endregion Actions
    }
}