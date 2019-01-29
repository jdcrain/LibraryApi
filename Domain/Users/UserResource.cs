using System.Collections.Generic;
using JsonApiDotNetCore.Models;
using LibraryApi.Models;

namespace LibraryApi.Domain.Users
{
    public class UserResource : ResourceDefinition<User>
    {
        // Create resource to override output attributes so that we don't include passwords in API responses (like in a POST response)
        protected override List<AttrAttribute> OutputAttrs() => Remove(u => new { u.Password, u.PasswordConfirmation });
    }
}