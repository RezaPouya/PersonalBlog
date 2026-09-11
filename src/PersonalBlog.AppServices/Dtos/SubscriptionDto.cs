namespace PersonalBlog.AppServices.Dtos;

public class SubscriptionDto
{
    public long Id { get; set; }
    public string Email { get; set; } = default!;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateSubscriptionInputDto
{
    public string Email { get; set; } = default!;
}
