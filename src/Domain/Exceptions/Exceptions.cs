namespace PersonalBlog.Domain.Exceptions;

public class BussinessException : Exception
{
    public BussinessException(string message) : base(message)
    {
    }

    public BussinessException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class AuthorizationException : Exception
{
    public AuthorizationException(string message) : base(message)
    {
    }

    public AuthorizationException(string message, Exception innerException) : base(message, innerException)
    {
    }
}