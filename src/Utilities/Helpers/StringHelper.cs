namespace PersonalBlog.Utilities.Helpers;

public static class StringHelper
{
    public static string CreateOtpCode(int numberOfCharacters = 6)
    {
        StringBuilder str = new StringBuilder();

        Random rnd = new Random(DateTime.Now.Millisecond);

        for (int i = 1; i <= numberOfCharacters; i++)
        {
            if (i == 1)
            {
                str.Append(rnd.Next(1, 9));
                continue;
            }

            str.Append(rnd.Next(0, 9));
        }

        return str.ToString();
    }
}