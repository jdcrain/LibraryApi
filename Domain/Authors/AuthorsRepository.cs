using System;
using System.Linq;
using JsonApiDotNetCore.Data;
using JsonApiDotNetCore.Internal.Query;
using JsonApiDotNetCore.Services;
using LibraryApi.Models;
using LibraryApi.Repositories;
using LibraryApi.Services;
using Microsoft.Extensions.Logging;

namespace LibraryApi.Domain.Authors
{
    public class AuthorsRepository : BelongsToUserRepository<Author>
    {
        public AuthorsRepository(
            ILoggerFactory loggerFactory, 
            IJsonApiContext jsonApiContext, 
            IDbContextResolver contextResolver,
            CurrentUserService currentUser
        ) : base(loggerFactory, jsonApiContext, contextResolver, currentUser)
        { }

        public override IQueryable<Author> Filter(IQueryable<Author> authors, FilterQuery filterQuery) 
        {
            if (filterQuery.Attribute.Equals("query")) {
                return authors.Where(a => 
                    a.First.Contains(filterQuery.Value, StringComparison.OrdinalIgnoreCase) ||
                    a.Last.Contains(filterQuery.Value, StringComparison.OrdinalIgnoreCase));
            }

            return base.Filter(authors, filterQuery);
        }
    }
}