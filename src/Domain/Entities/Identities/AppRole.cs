namespace PersonalBlog.Domain.Entities.Identities;

public class AppRole : IdentityRole<int>
{
    public AppRole() { }
    public AppRole(string roleName) : base(roleName) { }
}
