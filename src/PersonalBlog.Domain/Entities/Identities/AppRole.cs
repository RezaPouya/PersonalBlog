using Microsoft.AspNetCore.Identity;

namespace PersonalBlog.Domain.Entities.Identities;

public class AppRole : IdentityRole<long>
{
    public AppRole() { }
    public AppRole(string roleName) : base(roleName) { }
}
