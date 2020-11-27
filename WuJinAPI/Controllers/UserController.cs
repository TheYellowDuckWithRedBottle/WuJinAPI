using Microsoft.AspNetCore.Mvc;
using WuJinAPI.Models;
using WuJinAPI.Services;

namespace WuJinAPI.Controllers
{
    public class UserController:ControllerBase
    {
        private readonly UserService _UserService;
        public UserController(UserService userService)
        {
            _UserService = userService;
        }
        [HttpGet]
        public ActionResult<User> GetAllUser()
        {
          var users = _UserService.GetUsers();
            if (users.Count == 0 || users == null)
            {
                return NotFound();
            }
            else return Ok(users);
           
        }
        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public bool ValidateUser([FromForm] string userName, [FromForm] string password)
        {
            var user = _UserService.GetUser(userName);
            if (user == null)
            {
                return false;
            }
            else if (user != null && user.password == password)
                return true;
            else
            {
                return false;
            }

        }
       
    }
}
