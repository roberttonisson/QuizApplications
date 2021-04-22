using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Controller for TopicQuestions
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TopicQuestionsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public TopicQuestionsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/TopicQuestions
        /// <summary>
        /// Get the list of all TopicQuestions .
        /// </summary>
        /// <returns>List of TopicQuestions</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicQuestion>>> GetTopicQuestions()
        {
            var topicQuestions = (await _bll.TopicQuestions.GetAllAsync(null));
            
            return Ok(topicQuestions);
        }

        // GET: api/TopicQuestions/5
        /// <summary>
        /// Get single TopicQuestion by given id
        /// </summary>
        /// <param name="id">Id of the TopicQuestion that we are returning</param>
        /// <returns>TopicQuestion</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TopicQuestion>> GetTopicQuestion(Guid id)
        {
            var topicQuestion = await _bll.TopicQuestions.FirstOrDefaultAsync(id);

            if (topicQuestion == null)
            {
                return NotFound();
            }

            return Ok(topicQuestion);
        }

        // PUT: api/TopicQuestions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing TopicQuestion by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the TopicQuestion from DB</param>
        /// <param name="topicQuestion">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<TopicQuestion>> PutTopicQuestion(Guid id, TopicQuestion topicQuestion)
        {
            if (id != topicQuestion.Id)
            {
                return BadRequest();
            }

            await _bll.TopicQuestions.UpdateAsync(topicQuestion);
            await _bll.SaveChangesAsync();

            return Ok(topicQuestion);

        }

        // POST: api/TopicQuestions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new TopicQuestion to the DB.
        /// </summary>
        /// <param name="topicQuestion">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        public async Task<ActionResult<TopicQuestion>> PostTopicQuestion(TopicQuestion topicQuestion)
        {
            _bll.TopicQuestions.Add(topicQuestion);
            await _bll.SaveChangesAsync();

            return Ok(topicQuestion);
        }
        
        // DELETE: api/TopicQuestions/5
        /// <summary>
        /// Deletes a TopicQuestion record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<TopicQuestion>> DeleteTopicQuestion(Guid id)
        {
            var topicQuestion = await _bll.TopicQuestions.FirstOrDefaultAsync(id);
            if (topicQuestion == null)
            {
                return NotFound();
            }

            await _bll.TopicQuestions.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(topicQuestion);
        }
        
    }
}
