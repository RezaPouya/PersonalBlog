namespace Iau.Bazaar.Domain.Entities.Identities.Users.Services;

public interface ICurrentAppUser
{
    int UserId { get; }
    string UserName { get; }
    string FirstName { get; }
}