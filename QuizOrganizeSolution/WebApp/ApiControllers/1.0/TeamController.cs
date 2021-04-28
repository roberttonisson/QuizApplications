using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.DTO.CustomDTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Controller for Teams
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TeamsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public TeamsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/Teams
        /// <summary>
        /// Get the list of all Teams .
        /// </summary>
        /// <returns>List of Teams</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            var teams = (await _bll.Teams.GetAllAsync(null));
            
            return Ok(teams);
        }

        // GET: api/Teams/5
        /// <summary>
        /// Get single Team by given id
        /// </summary>
        /// <param name="id">Id of the Team that we are returning</param>
        /// <returns>Team</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(Guid id)
        {
            var team = await _bll.Teams.FirstOrDefaultAsync(id);

            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

        // PUT: api/Teams/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing Team by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the Team from DB</param>
        /// <param name="team">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(Guid id, Team team)
        {
            if (id != team.Id)
            {
                return BadRequest();
            }

            await _bll.Teams.UpdateAsync(team);
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/Teams
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new Team to the DB.
        /// </summary>
        /// <param name="team">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team team)
        {
            _bll.Teams.Add(team);
            await _bll.SaveChangesAsync();

            return Ok(team);
        }
        
        // POST: api/Teams
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new Team to and its members invitations to the DB.
        /// </summary>
        /// <param name="addTeamDto">DTO with the values for the records that will be inserted into DB.</param>
        /// <returns>Team that was added to the DB.</returns>
        [HttpPost("members")]
        public async Task<ActionResult<Team>> PostTeam(AddTeamDTO addTeamDto)
        {
            var team = await _bll.Teams.AddTeamWithMembers(addTeamDto);

            return Ok(team);
        }

        // DELETE: api/Teams/5
        /// <summary>
        /// Deletes a Team record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Team>> DeleteTeam(Guid id)
        {
            var team = await _bll.Teams.FirstOrDefaultAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            await _bll.Teams.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(team);
        }
        
    }
}
