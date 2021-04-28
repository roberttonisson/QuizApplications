using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.DTO.Identity;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Controller for Quizzes
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class QuizController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly UserManager<Models.Identity.AppUser> _userManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public QuizController(IAppBLL bll, UserManager<Models.Identity.AppUser>_userManager)
        {
            _bll = bll;
        }
        
        // GET: api/Quiz
        /// <summary>
        /// Get the list of all Quizzes .
        /// </summary>
        /// <returns>List of Quizzes</returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzes()
        {
            var quizzes = (await _bll.Quizzes.GetAllAsync(null));
            
            return Ok(quizzes);
        }
        
        // GET: api/Quiz
        /// <summary>
        /// Get the list of all Quizzes .
        /// </summary>
        /// <returns>User with quizzes</returns>
        [HttpGet("userQuizzes")]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUserWithQuizzes()
        {
            var user = (await _bll.AppUsers.GetUserWithQuizCollectionsCustomUser(User.UserGuidId()));
            
            return Ok(user);
        }
        
        // GET: api/Quiz
        /// <summary>
        /// Get the list of all frinds quizzes
        /// </summary>
        /// <returns>List of Quizzes</returns>
        [HttpGet("friendQuizzes")]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetFriendQuizzes()
        {
            var quizzes = await _bll.Quizzes.GetFriendQuizzes(User.UserGuidId());
            
            return Ok(quizzes);
        }

        // GET: api/Quiz/5
        /// <summary>
        /// Get single Quiz by given id
        /// </summary>
        /// <param name="id">Id of the Quiz that we are returning</param>
        /// <returns>Quiz</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz>> GetQuiz(Guid id)
        {
            var quiz = await _bll.Quizzes.GetSingleWithCollections(id);

            if (quiz == null)
            {
                return NotFound();
            }

            return Ok(quiz);
        }

        // PUT: api/Quiz/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing Quiz by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the Quiz from DB</param>
        /// <param name="quiz">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz(Guid id, Quiz quiz)
        {
            if (id != quiz.Id)
            {
                return BadRequest();
            }

            await _bll.Quizzes.UpdateAsync(quiz);
            await _bll.SaveChangesAsync();

            return Ok(quiz);

        }

        // POST: api/Quiz
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new Quiz to the DB.
        /// </summary>
        /// <param name="quiz">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        public async Task<ActionResult<Quiz>> PostQuiz(Quiz quiz)
        {
            _bll.Quizzes.Add(quiz);
            await _bll.SaveChangesAsync();

            return Ok(quiz);
        }

        // DELETE: api/Quiz/5
        /// <summary>
        /// Deletes a Quiz record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Quiz>> DeleteQuiz(Guid id)
        {
            var quiz = await _bll.Quizzes.FirstOrDefaultAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }

            await _bll.Quizzes.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(quiz);
        }
        
    }
}
