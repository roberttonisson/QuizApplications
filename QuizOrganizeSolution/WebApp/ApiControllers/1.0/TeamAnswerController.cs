using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Controller for TeamAnswers
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TeamAnswersController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public TeamAnswersController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/TeamAnswers
        /// <summary>
        /// Get the list of all TeamAnswers .
        /// </summary>
        /// <returns>List of TeamAnswers</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamAnswer>>> GetTeamAnswers()
        {
            var teamAnswers = (await _bll.TeamAnswers.GetAllAsync(null));
            
            return Ok(teamAnswers);
        }

        // GET: api/TeamAnswers/5
        /// <summary>
        /// Get single TeamAnswer by given id
        /// </summary>
        /// <param name="id">Id of the TeamAnswer that we are returning</param>
        /// <returns>TeamAnswer</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TeamAnswer>> GetTeamAnswer(Guid id)
        {
            var teamAnswer = await _bll.TeamAnswers.FirstOrDefaultAsync(id);

            if (teamAnswer == null)
            {
                return NotFound();
            }

            return Ok(teamAnswer);
        }

        // PUT: api/TeamAnswers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing TeamAnswer by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the TeamAnswer from DB</param>
        /// <param name="teamAnswer">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeamAnswer(Guid id, TeamAnswer teamAnswer)
        {
            if (id != teamAnswer.Id)
            {
                return BadRequest();
            }

            await _bll.TeamAnswers.UpdateAsync(teamAnswer);
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/TeamAnswers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new TeamAnswer to the DB.
        /// </summary>
        /// <param name="teamAnswer">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        public async Task<ActionResult<TeamAnswer>> PostTeamAnswer(TeamAnswer teamAnswer)
        {
            _bll.TeamAnswers.Add(teamAnswer);
            await _bll.SaveChangesAsync();

            return Ok(teamAnswer);
        }

        // DELETE: api/TeamAnswers/5
        /// <summary>
        /// Deletes a TeamAnswer record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<TeamAnswer>> DeleteTeamAnswer(Guid id)
        {
            var teamAnswer = await _bll.TeamAnswers.FirstOrDefaultAsync(id);
            if (teamAnswer == null)
            {
                return NotFound();
            }

            await _bll.TeamAnswers.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(teamAnswer);
        }
        
    }
}
