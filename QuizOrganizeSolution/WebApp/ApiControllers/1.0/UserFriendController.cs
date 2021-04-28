using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.App.DTO;
using BLL.App.DTO.CustomDTO;
using BLL.App.DTO.Identity;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Controller for UserFriends
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UserFriendsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public UserFriendsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/UserFriends
        /// <summary>
        /// Get the list of all UserFriends .
        /// </summary>
        /// <returns>List of UserFriends</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserFriend>>> GetUserFriends()
        {
            var userFriends = (await _bll.UserFriends.GetAllAsync(null));
            
            return Ok(userFriends);
        }
        
        

        // GET: api/UserFriends/5
        /// <summary>
        /// Get single UserFriend by given id
        /// </summary>
        /// <param name="id">Id of the UserFriend that we are returning</param>
        /// <returns>UserFriend</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserFriend>> GetUserFriend(Guid id)
        {
            var userFriend = await _bll.UserFriends.FirstOrDefaultAsync(id);

            if (userFriend == null)
            {
                return NotFound();
            }

            return Ok(userFriend);
        }
        // GET: api/UserFriends/5
        /// <summary>
        /// Get single user with friends collections by given id
        /// </summary>
        /// <returns>User</returns>
        [HttpGet("myFriends")]
        public async Task<ActionResult<AppUserCustomDTO>> GetUserWithFriends()
        {
            var userFriend = await _bll.AppUsers.GetUserWithFriendsCollectionsCustomUser(User.UserGuidId());

            if (userFriend == null)
            {
                return NotFound();
            }

            return Ok(userFriend);
        }
        

        // PUT: api/UserFriends/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Change existing UserFriend by given ID
        /// </summary>
        /// <param name="id">Given ID that we use to find the UserFriend from DB</param>
        /// <param name="userFriend">DTO with new values tha we need to change</param>
        /// <returns>Nothing</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserFriend(Guid id, UserFriend userFriend)
        {
            if (id != userFriend.Id)
            {
                return BadRequest();
            }

            await _bll.UserFriends.UpdateAsync(userFriend);
            await _bll.SaveChangesAsync();

            return NoContent();

        }

        // POST: api/UserFriends
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Add a new UserFriend to the DB.
        /// </summary>
        /// <param name="userFriend">DTO with the values for the record tha will be inserted into DB.</param>
        /// <returns>DTO with the values from the record that was added to the DB.</returns>
        [HttpPost]
        public async Task<ActionResult<UserFriend>> PostUserFriend(UserFriend userFriend)
        {
            var uf = await _bll.UserFriends.SendFriendRequest(userFriend);
            return Ok(uf);
        }
        
        // POST: api/UserFriends
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        /// <summary>
        /// Search for users.
        /// </summary>
        /// <param name="search">String with search parameters.</param>
        /// <returns>All Users from DB by criteria.</returns>
        [HttpPost("search")]
        public async Task<ActionResult<IEnumerable<AppUser>>> SearchUsers(SearchDTO search)
        {
            return Ok(await _bll.UserFriends.SearchUsers(search.Search));
        }
        

        // DELETE: api/UserFriends/5
        /// <summary>
        /// Deletes a UserFriend record from the DB by id.
        /// </summary>
        /// <param name="id">Id for the record that will be removed from the DB.</param>
        /// <returns>:)</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserFriend>> DeleteUserFriend(Guid id)
        {
            var userFriend = await _bll.UserFriends.FirstOrDefaultAsync(id);
            if (userFriend == null)
            {
                return NotFound();
            }

            await _bll.UserFriends.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(userFriend);
        }
        
    }
}
