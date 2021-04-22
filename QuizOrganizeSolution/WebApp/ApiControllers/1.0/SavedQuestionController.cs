using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Controller for SavedQuestions
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SavedQuestionsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public SavedQuestionsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/SavedQuestions
        /// <summary>
        /// Get the list of all SavedQuestions .
        /// </summary>
        /// <returns>List of SavedQuestions</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SavedQuestion>>> GetSavedQuestions()
        {
            var savedQuestions = (await _bll.SavedQuestions.GetAllAsync(null));
            
            return Ok(savedQuestions);
        }

        // GET: api/SavedQuestions/5
        /// <summary>
        /// Get single SavedQuestion by given id
        /// </summary>
        /// <param name="id">Id of the SavedQuestion that we are returning</param>
        /// <returns>SavedQuestion</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<SavedQuestion>> GetSavedQuestion(Guid id)
        {
            var savedQuestion = await _bll.SavedQuestions.FirstOrDefaultAsync(id);

            if (savedQuestion == null)
            {
                return NotFound();
            }

            return Ok(savedQuestion);
        }

        // PUT: api/SavedQuestions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing SavedQuestion by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the SavedQuestion from DB</param>
        /// <param name="savedQuestion">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSavedQuestion(Guid id, SavedQuestion savedQuestion)
        {
            if (id != savedQuestion.Id)
            {
                return BadRequest();
            }

            await _bll.SavedQuestions.UpdateAsync(savedQuestion);
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/SavedQuestions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new SavedQuestion to the DB.
        /// </summary>
        /// <param name="savedQuestion">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        public async Task<ActionResult<SavedQuestion>> PostSavedQuestion(SavedQuestion savedQuestion)
        {
            _bll.SavedQuestions.Add(savedQuestion);
            await _bll.SaveChangesAsync();

            return Ok(savedQuestion);
        }

        // DELETE: api/SavedQuestions/5
        /// <summary>
        /// Deletes a SavedQuestion record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<SavedQuestion>> DeleteSavedQuestion(Guid id)
        {
            var savedQuestion = await _bll.SavedQuestions.FirstOrDefaultAsync(id);
            if (savedQuestion == null)
            {
                return NotFound();
            }

            await _bll.SavedQuestions.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(savedQuestion);
        }
        
    }
}
