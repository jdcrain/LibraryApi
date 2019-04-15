using System.ComponentModel.DataAnnotations.Schema;
using JsonApiDotNetCore.Models;

namespace LibraryApi.Models
{
    public abstract class BelongsToUser : Identifiable
    {
        public int UserId { get; set; }
        public User User { get; set; }

        [Attr("username"), NotMapped] public string Username { get => User?.Username; }
    }
}