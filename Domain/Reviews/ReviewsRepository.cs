using System;
using System.Linq;
using JsonApiDotNetCore.Data;
using JsonApiDotNetCore.Internal.Query;
using JsonApiDotNetCore.Services;
using LibraryApi.Models;
using Microsoft.Extensions.Logging;

namespace LibraryApi.Domain.Reviews
{
    public class ReviewsRepository : DefaultEntityRepository<Review>
    {
        public ReviewsRepository(
            ILoggerFactory loggerFactory, 
            IJsonApiContext jsonApiContext, 
            IDbContextResolver contextResolver
        ) : base(loggerFactory, jsonApiContext, contextResolver)
        { }

        public override IQueryable<Review> Filter(IQueryable<Review> reviews, FilterQuery filterQuery) 
        {
            if (filterQuery.Attribute.Equals("query")) {
                return reviews.Where(b => 
                    b.User.Contains(filterQuery.Value, StringComparison.OrdinalIgnoreCase));
            }

            return base.Filter(reviews, filterQuery);
        }
    }
}