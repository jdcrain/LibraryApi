using JsonApiDotNetCore.Models;

namespace LibraryApi.Models
{
    public abstract class BelongsToUser : Identifiable
    {
        public int UserId { get; set; }
        public User User { get; set; }
    }
}