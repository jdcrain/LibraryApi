using System.Threading.Tasks;
using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using LibraryApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LibraryApi.Controllers
{
    public abstract class BelongsToUserController<T> : JsonApiController<T> where T : BelongsToUser
    {
        public BelongsToUserController(
            IJsonApiContext jsonApiContext, 
            IResourceService<T> resourceService, 
            ILoggerFactory loggerFactory
        ) : base(jsonApiContext, resourceService, loggerFactory)
        {}

        [HttpPost, CurrentUserFilter]
        public override async Task<IActionResult> PostAsync([FromBody] T model) => await base.PostAsync(model);
    }
}