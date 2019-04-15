using System.Linq;
using System.Threading.Tasks;
using JsonApiDotNetCore.Data;
using JsonApiDotNetCore.Models;
using JsonApiDotNetCore.Services;
using LibraryApi.Models;
using LibraryApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LibraryApi.Repositories
{
    public class BelongsToUserRepository<T> : DefaultEntityRepository<T> where T : BelongsToUser
    {
        private readonly CurrentUserService _currentUser;
        private readonly AppDbContext _db;

        public BelongsToUserRepository(
            ILoggerFactory loggerFactory, 
            IJsonApiContext jsonApiContext, 
            IDbContextResolver contextResolver,
            CurrentUserService currentUser
        ) : base(loggerFactory, jsonApiContext, contextResolver)
        {
            _currentUser = currentUser;
            _db = (AppDbContext) contextResolver.GetContext();
        }

        public override IQueryable<T> Get() 
        {
            return base.Get().Include("User");
        }

        public override async Task<T> CreateAsync(T entity) 
        {
            entity.UserId = _currentUser.GetUserId();
            
            var savedEntity = await base.CreateAsync(entity);

            await _db.Entry(savedEntity).Reference(e => e.User).LoadAsync();

            return savedEntity;
        }
    }
}