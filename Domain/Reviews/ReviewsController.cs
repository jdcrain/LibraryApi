using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using LibraryApi.Controllers;
using LibraryApi.Models;
using Microsoft.Extensions.Logging;

namespace LibraryApi.Domain.Reviews
{
    public class ReviewsController : BelongsToUserController<Review>
    {
        // controller constructor to pass everything into the base class
        public ReviewsController(
            IJsonApiContext jsonApiContext, // used by JsonApiDotNetCore to know all of the data types, attributes, and relationships inside of our AppDbContext
            IResourceService<Review> resourceService, // used to get the Review data from our database
            ILoggerFactory loggerFactory // Lets us log any messages to the console
        ) : base(jsonApiContext, resourceService, loggerFactory)
        {}
    }
}