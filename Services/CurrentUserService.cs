using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LibraryApi.Models;
using Microsoft.AspNetCore.Http;

namespace LibraryApi.Services
{
    public class CurrentUserService
    {
        private readonly HttpContext _httpContext;
        private readonly AppDbContext _db;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, AppDbContext db)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _db = db;
        }

        public int GetUserId()
        {
            var idString = _httpContext.User.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value;

            return int.Parse(idString);
        }

        public async Task<User> GetUserAsync() 
        {
            return await _db.Users.FindAsync(GetUserId());
        }
    }
}