using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mile3image.DTO_s.RequestDTO;
using mile3image.DTO_s.ResponceDTO;
using mile3image.Entities;
using mile3image.IService;
using System.Threading.Tasks;

namespace mile3image.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromForm] UserRequestDTO userRequestDTO)
        {
            if (userRequestDTO == null || userRequestDTO.ProfileImage == null)
            {
                return BadRequest("User details and profile image are required.");
            }

            var createdUser = await _userService.CreateUser(userRequestDTO);

            if (createdUser == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating user");
            }

            return Ok(createdUser);
        }
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var data=await _userService.GetAllUsers();
            return Ok(data);
        }
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(int userid)
        {
            var data=await _userService.GetUserById(userid);
            return Ok(data);
        }
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(int userId, [FromForm] UserRequestDTO userRequestDTO)
        {
            
                var data = await _userService.UpdateUser(userId, userRequestDTO);
                return Ok(data);
           
        }
        [HttpDelete("DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                await _userService.DeleteUser(userId);
                return Ok("User deleted successfully.");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
