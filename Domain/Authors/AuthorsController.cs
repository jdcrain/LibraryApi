using JsonApiDotNetCore.Services;
using LibraryApi.Controllers;
using LibraryApi.Models;
using Microsoft.Extensions.Logging;

namespace LibraryApi.Domain.Authors
{
    // extend controller from the JsonApiController base class
    public class AuthorsController : BelongsToUserController<Author>
    {
        // controller constructor to pass everything into the base class
        public AuthorsController(
            IJsonApiContext jsonApiContext, // used by JsonApiDotNetCore to know all of the data types, attributes, and relationships inside of our AppDbContext
            IResourceService<Author> resourceService, // used to get the Author data frm our database
            ILoggerFactory loggerFactory // Lets us log any messages to the console
        ) : base(jsonApiContext, resourceService, loggerFactory)
        {}
    }
}