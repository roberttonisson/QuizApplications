using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Controller for QuestionAnswers
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class QuestionAnswersController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public QuestionAnswersController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/QuestionAnswers
        /// <summary>
        /// Get the list of all QuestionAnswers .
        /// </summary>
        /// <returns>List of QuestionAnswers</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionAnswer>>> GetQuestionAnswers()
        {
            var questionAnswers = (await _bll.QuestionAnswers.GetAllAsync(null));
            
            return Ok(questionAnswers);
        }

        // GET: api/QuestionAnswers/5
        /// <summary>
        /// Get single QuestionAnswer by given id
        /// </summary>
        /// <param name="id">Id of the QuestionAnswer that we are returning</param>
        /// <returns>QuestionAnswer</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionAnswer>> GetQuestionAnswer(Guid id)
        {
            var questionAnswer = await _bll.QuestionAnswers.FirstOrDefaultAsync(id);

            if (questionAnswer == null)
            {
                return NotFound();
            }

            return Ok(questionAnswer);
        }

        // PUT: api/QuestionAnswers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing QuestionAnswer by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the QuestionAnswer from DB</param>
        /// <param name="questionAnswer">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionAnswer(Guid id, QuestionAnswer questionAnswer)
        {
            if (id != questionAnswer.Id)
            {
                return BadRequest();
            }

            await _bll.QuestionAnswers.UpdateAsync(questionAnswer);
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/QuestionAnswers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new QuestionAnswer to the DB.
        /// </summary>
        /// <param name="questionAnswer">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        public async Task<ActionResult<QuestionAnswer>> PostQuestionAnswer(QuestionAnswer questionAnswer)
        {
            _bll.QuestionAnswers.Add(questionAnswer);
            await _bll.SaveChangesAsync();

            return Ok(questionAnswer);
        }
        
        // POST: api/QuestionAnswers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new QuestionAnswer list to the DB.
        /// </summary>
        /// <param name="questionAnswers">DTO list with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost("addList")]
        public async Task<ActionResult<QuestionAnswer[]>> PostQuestionAnswerList(QuestionAnswer[] questionAnswers)
        {
            await _bll.QuestionAnswers.AddList(questionAnswers);

            return Ok(questionAnswers);
        }

        // DELETE: api/QuestionAnswers/5
        /// <summary>
        /// Deletes a QuestionAnswer record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuestionAnswer>> DeleteQuestionAnswer(Guid id)
        {
            var questionAnswer = await _bll.QuestionAnswers.FirstOrDefaultAsync(id);
            if (questionAnswer == null)
            {
                return NotFound();
            }

            await _bll.QuestionAnswers.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(questionAnswer);
        }
        
    }
}
