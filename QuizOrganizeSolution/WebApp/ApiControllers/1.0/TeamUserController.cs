using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Controller for TeamUsers
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TeamUsersController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public TeamUsersController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/TeamUsers
        /// <summary>
        /// Get the list of all TeamUsers .
        /// </summary>
        /// <returns>List of TeamUsers</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamUser>>> GetTeamUsers()
        {
            var teamUsers = (await _bll.TeamUsers.GetAllAsync(null));
            
            return Ok(teamUsers);
        }

        // GET: api/TeamUsers/5
        /// <summary>
        /// Get single TeamUser by given id
        /// </summary>
        /// <param name="id">Id of the TeamUser that we are returning</param>
        /// <returns>TeamUser</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamUser>> GetTeamUser(Guid id)
        {
            var teamUser = await _bll.TeamUsers.FirstOrDefaultAsync(id);

            if (teamUser == null)
            {
                return NotFound();
            }

            return Ok(teamUser);
        }

        // PUT: api/TeamUsers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing TeamUser by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the TeamUser from DB</param>
        /// <param name="teamUser">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeamUser(Guid id, TeamUser teamUser)
        {
            if (id != teamUser.Id)
            {
                return BadRequest();
            }

            await _bll.TeamUsers.UpdateAsync(teamUser);
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/TeamUsers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new TeamUser to the DB.
        /// </summary>
        /// <param name="teamUser">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        public async Task<ActionResult<TeamUser>> PostTeamUser(TeamUser teamUser)
        {
            _bll.TeamUsers.Add(teamUser);
            await _bll.SaveChangesAsync();

            return Ok(teamUser);
        }

        // DELETE: api/TeamUsers/5
        /// <summary>
        /// Deletes a TeamUser record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<TeamUser>> DeleteTeamUser(Guid id)
        {
            var teamUser = await _bll.TeamUsers.FirstOrDefaultAsync(id);
            if (teamUser == null)
            {
                return NotFound();
            }

            await _bll.TeamUsers.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(teamUser);
        }
        
    }
}
