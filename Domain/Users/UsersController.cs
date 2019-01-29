using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Internal;
using JsonApiDotNetCore.Services;
using LibraryApi.Models;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace LibraryApi.Domain.Users
{
    [NoHttpDelete, NoHttpPatch]
    public class UsersController : JsonApiController<User>
    {
        private readonly CurrentUserService _currentUser;

        public UsersController(
            IJsonApiContext jsonApiContext, 
            IResourceService<User> resourceService, 
            ILoggerFactory loggerFactory,
            CurrentUserService currentUser
        ) : base(jsonApiContext, resourceService, loggerFactory)
        { 
            _currentUser = currentUser;
        }

        [HttpGet("me"), CurrentUserFilter] // Add filter that checks the JWT and gets the current user
        public async Task<IActionResult> GetMe() 
        {
            return Ok(await _currentUser.GetUserAsync());
        }

        [HttpPost]
        public override async Task<IActionResult> PostAsync(User user) 
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password, 10);

            return await base.PostAsync(user);
        }

        [HttpGet]
        public override async Task<IActionResult> GetAsync() => NotFound();

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id) => NotFound();
    }
}