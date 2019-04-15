using System.Threading.Tasks;
using JsonApiDotNetCore.Data;
using JsonApiDotNetCore.Models;
using JsonApiDotNetCore.Services;
using LibraryApi.Models;
using LibraryApi.Services;
using Microsoft.Extensions.Logging;

namespace LibraryApi.Repositories
{
    public class BelongsToUserRepository<T> : DefaultEntityRepository<T> where T : BelongsToUser
    {
        private readonly CurrentUserService _currentUser;

        public BelongsToUserRepository(
            ILoggerFactory loggerFactory, 
            IJsonApiContext jsonApiContext, 
            IDbContextResolver contextResolver,
            CurrentUserService currentUser
        ) : base(loggerFactory, jsonApiContext, contextResolver)
        {
            _currentUser = currentUser;
        }

        public override async Task<T> CreateAsync(T entity) 
        {
            entity.UserId = _currentUser.GetUserId();
            
            return await base.CreateAsync(entity);
        }
    }
}