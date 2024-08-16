using Microsoft.AspNetCore.Mvc;
using UserService.Model;
using UserService.Repository;

namespace UserService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {
        /// <summary>
        /// Declare User interface
        /// </summary>
        private readonly Iuser _iuser;

        /// <summary>
        /// User class constructor
        /// </summary>
        /// <param name="iuser"></param>
        public Users(Iuser iuser)
        {
            _iuser = iuser;
        }

        /// <summary>
        /// Get all user details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserDetails")]
        public Task<List<UserDetails>> GetUserDetails()
        {
            return _iuser.GetAll();
        }

        /// <summary>
        /// Get user details by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserById/{id}")]
        public Task<UserDetails> GetUserById(int id)
        {
            return _iuser.GetUserById(id);
        }

        /// <summary>
        /// Add new user details
        /// </summary>
        /// <param name="userDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUser")]
        public async Task<ActionResult> AddUser([FromBody] UserDetails userDetails)
        {

            if (ModelState.IsValid)
            {
                return Ok(await _iuser.AddUser(userDetails));
            }
            else
            {
                return BadRequest(ModelState);

            }
        }

        /// <summary>
        /// Delete user details by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            return Ok(await _iuser.DeleteUser(id));
        }
    }
}
