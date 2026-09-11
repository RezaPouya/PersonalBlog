namespace PersonalBlog.AppServices.Dtos;

public class CreateContactMessageInputDto { public string FullName { get; set; } = default!; public string Email { get; set; } = default!; public string Subject { get; set; } = default!; public string Body { get; set; } = default!; public string RecaptchaToken { get; set; } = default!; }
