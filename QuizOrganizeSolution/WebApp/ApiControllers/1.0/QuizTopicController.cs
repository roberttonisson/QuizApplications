using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Controller for QuizTopics
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class QuizTopicsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public QuizTopicsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/QuizTopics
        /// <summary>
        /// Get the list of all QuizTopics .
        /// </summary>
        /// <returns>List of QuizTopics</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizTopic>>> GetQuizTopics()
        {
            var quizTopics = (await _bll.QuizTopics.GetAllAsync(null));
            
            return Ok(quizTopics);
        }

        // GET: api/QuizTopics/5
        /// <summary>
        /// Get single QuizTopic by given id
        /// </summary>
        /// <param name="id">Id of the QuizTopic that we are returning</param>
        /// <returns>QuizTopic</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizTopic>> GetQuizTopic(Guid id)
        {
            var quizTopic = await _bll.QuizTopics.FirstOrDefaultAsync(id);

            if (quizTopic == null)
            {
                return NotFound();
            }

            return Ok(quizTopic);
        }

        // PUT: api/QuizTopics/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing QuizTopic by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the QuizTopic from DB</param>
        /// <param name="quizTopic">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<QuizTopic>> PutQuizTopic(Guid id, QuizTopic quizTopic)
        {
            if (id != quizTopic.Id)
            {
                return BadRequest();
            }

            await _bll.QuizTopics.UpdateAsync(quizTopic);
            await _bll.SaveChangesAsync();

            return Ok(quizTopic);

        }

        // POST: api/QuizTopics
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new QuizTopic to the DB.
        /// </summary>
        /// <param name="quizTopic">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        public async Task<ActionResult<QuizTopic>> PostQuizTopic(QuizTopic quizTopic)
        {
            _bll.QuizTopics.Add(quizTopic);
            await _bll.SaveChangesAsync();

            return Ok(quizTopic);
        }

        // DELETE: api/QuizTopics/5
        /// <summary>
        /// Deletes a QuizTopic record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuizTopic>> DeleteQuizTopic(Guid id)
        {
            var quizTopic = await _bll.QuizTopics.FirstOrDefaultAsync(id);
            if (quizTopic == null)
            {
                return NotFound();
            }

            await _bll.QuizTopics.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(quizTopic);
        }
        
    }
}
