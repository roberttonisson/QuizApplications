

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Controller for QuizInvitations
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class QuizInvitationsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public QuizInvitationsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/QuizInvitations
        /// <summary>
        /// Get the list of all QuizInvitations .
        /// </summary>
        /// <returns>List of QuizInvitations</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizInvitation>>> GetQuizInvitations()
        {
            var quizInvitations = (await _bll.QuizInvitations.GetAllAsync(null));
            
            return Ok(quizInvitations);
        }

        // GET: api/QuizInvitations/5
        /// <summary>
        /// Get single QuizInvitation by given id
        /// </summary>
        /// <param name="id">Id of the QuizInvitation that we are returning</param>
        /// <returns>QuizInvitation</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizInvitation>> GetQuizInvitation(Guid id)
        {
            var quizInvitation = await _bll.QuizInvitations.FirstOrDefaultAsync(id);

            if (quizInvitation == null)
            {
                return NotFound();
            }

            return Ok(quizInvitation);
        }

        // PUT: api/QuizInvitations/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing QuizInvitation by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the QuizInvitation from DB</param>
        /// <param name="quizInvitation">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuizInvitation(Guid id, QuizInvitation quizInvitation)
        {
            if (id != quizInvitation.Id)
            {
                return BadRequest();
            }

            await _bll.QuizInvitations.UpdateAsync(quizInvitation);
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/QuizInvitations
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new QuizInvitation to the DB.
        /// </summary>
        /// <param name="quizInvitation">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        public async Task<ActionResult<QuizInvitation>> PostQuizInvitation(QuizInvitation quizInvitation)
        {
            _bll.QuizInvitations.Add(quizInvitation);
            await _bll.SaveChangesAsync();

            return Ok(quizInvitation);
        }

        // DELETE: api/QuizInvitations/5
        /// <summary>
        /// Deletes a QuizInvitation record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuizInvitation>> DeleteQuizInvitation(Guid id)
        {
            var quizInvitation = await _bll.QuizInvitations.FirstOrDefaultAsync(id);
            if (quizInvitation == null)
            {
                return NotFound();
            }

            await _bll.QuizInvitations.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(quizInvitation);
        }
        
    }
}
