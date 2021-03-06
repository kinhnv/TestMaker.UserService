using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestMaker.Common.Extensions;
using TestMaker.Common.Models;
using TestMaker.UserService.Domain.Models.User;
using TestMaker.UserService.Domain.Services;

namespace TestMaker.UserService.Api.Controllers
{
    [Authorize]
    [Route("api/Profile")]
    [ApiController]
    public class ProfileController : Controller
    {
        private readonly IUsersService _usersService;

        public ProfileController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var userId = User.GetUserId();
            var result = await _usersService.GetUserAsync(userId ?? Guid.Empty);
            return Ok(new ApiResult<UserForDetails>(result));
        }
    }
}
