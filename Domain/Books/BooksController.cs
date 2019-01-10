using JsonApiDotNetCore.Controllers;
using JsonApiDotNetCore.Services;
using LibraryApi.Models;
using Microsoft.Extensions.Logging;

namespace LibraryApi.Domain.Books
{
    // extend controller from the JsonApiController base class
    public class BooksController : JsonApiController<Book>
    {
        // controller constructor to pass everything into the base class
        public BooksController(
            IJsonApiContext jsonApiContext, // used by JsonApiDotNetCore to know all of the data types, attributes, and relationships inside of our AppDbContext
            IResourceService<Book> resourceService, // used to get the Book data frm our database
            ILoggerFactory loggerFactory // Lets us log any messages to the console
        ) : base(jsonApiContext, resourceService, loggerFactory)
        {}
    }
}