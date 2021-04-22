using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Controller for Feedbacks
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class FeedbacksController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public FeedbacksController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/Feedbacks
        /// <summary>
        /// Get the list of all Feedbacks .
        /// </summary>
        /// <returns>List of Feedbacks</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacks()
        {
            var feedback = await _bll.Feedback.GetAllAsync(null);
            
            return Ok(feedback);
        }

        // GET: api/Feedbacks/5
        /// <summary>
        /// Get single Feedback by given id
        /// </summary>
        /// <param name="id">Id of the Feedback that we are returning</param>
        /// <returns>Feedback</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Feedback>> GetFeedback(Guid id)
        {
            var feedback = await _bll.Feedback.FirstOrDefaultAsync(id);

            if (feedback == null)
            {
                return NotFound();
            }

            return Ok(feedback);
        }

        // PUT: api/Feedbacks/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing Feedback by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the Feedback from DB</param>
        /// <param name="feedback"></param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedback(Guid id, Feedback feedback)
        {
            if (id != feedback.Id)
            {
                return BadRequest();
            }

            await _bll.Feedback.UpdateAsync(feedback);
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/Feedbacks
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new Feedback to the DB.
        /// </summary>
        /// <param name="feedback"></param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        public async Task<ActionResult<Feedback>> PostFeedback(Feedback feedback)
        {
            var bllEntity = feedback;
            _bll.Feedback.Add(bllEntity);
            await _bll.SaveChangesAsync();

            feedback.Id = bllEntity.Id;

            return Ok(feedback);
        }

        // DELETE: api/Feedbacks/5
        /// <summary>
        /// Deletes a Feedback record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Feedback>> DeleteFeedback(Guid id)
        {
            var feedback = await _bll.Feedback.FirstOrDefaultAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }

            await _bll.Feedback.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(feedback);
        }
        
    }
}
