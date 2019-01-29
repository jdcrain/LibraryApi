using System.Threading.Tasks;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LibraryApi.Domain.Users
{
    [NoHttpDelete, NoHttpPatch]
    public class UsersController : JsonApiController<User>
    {
        public UsersController(
            IJsonApiContext jsonApiContext, 
            IResourceService<User> resourceService, 
            ILoggerFactory loggerFactory
        ) : base(jsonApiContext, resourceService, loggerFactory)
        { }

        [HttpGet]
        public override async Task<IActionResult> GetAsync() => NotFound();

        [HttpGet("{id}")]
        public override async Task<IActionResult> GetAsync(int id) => NotFound();

        [HttpPost]
        public override async Task<IActionResult> PostAsync(User user) 
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password, 10);

            return await base.PostAsync(user);
        }
    }
}