using System;
using System.Linq;
using JsonApiDotNetCore.Data;
using JsonApiDotNetCore.Internal.Query;
using JsonApiDotNetCore.Services;
using LibraryApi.Models;
using LibraryApi.Repositories;
using LibraryApi.Services;
using Microsoft.Extensions.Logging;

namespace LibraryApi.Domain.Reviews
{
    public class ReviewsRepository : BelongsToUserRepository<Review>
    {
        public ReviewsRepository(
            ILoggerFactory loggerFactory, 
            IJsonApiContext jsonApiContext, 
            IDbContextResolver contextResolver,
            CurrentUserService currentUser
        ) : base(loggerFactory, jsonApiContext, contextResolver, currentUser)
        { }

        public override IQueryable<Review> Filter(IQueryable<Review> reviews, FilterQuery filterQuery) 
        {
            if (filterQuery.Attribute.Equals("query")) {
                return reviews.Where(b => 
                    b.User.Username.Contains(filterQuery.Value, StringComparison.OrdinalIgnoreCase) ||
                    b.User.Email.Contains(filterQuery.Value, StringComparison.OrdinalIgnoreCase));
            }

            return base.Filter(reviews, filterQuery);
        }
    }
}