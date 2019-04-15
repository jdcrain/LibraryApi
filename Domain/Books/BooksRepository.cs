using System;
using System.Linq;
using JsonApiDotNetCore.Data;
using JsonApiDotNetCore.Internal.Query;
using JsonApiDotNetCore.Services;
using LibraryApi.Models;
using LibraryApi.Repositories;
using LibraryApi.Services;
using Microsoft.Extensions.Logging;

namespace LibraryApi.Domain.Books
{
    public class BooksRepository : BelongsToUserRepository<Book>
    {
        public BooksRepository(
            ILoggerFactory loggerFactory, 
            IJsonApiContext jsonApiContext, 
            IDbContextResolver contextResolver,
            CurrentUserService currentUser
        ) : base(loggerFactory, jsonApiContext, contextResolver, currentUser)
        { }

        public override IQueryable<Book> Filter(IQueryable<Book> books, FilterQuery filterQuery) 
        {
            if (filterQuery.Attribute.Equals("query")) {
                return books.Where(b => 
                    b.Title.Contains(filterQuery.Value, StringComparison.OrdinalIgnoreCase));
            }

            return base.Filter(books, filterQuery);
        }
    }
}