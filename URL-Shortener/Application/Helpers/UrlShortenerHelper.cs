using System.Text;

namespace Application.Helpers;

public class UrlShortenerHelper
{
    public string GenerateSurl(int length = 6) 
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";

        StringBuilder result = new StringBuilder();

        Random random = new Random();

        var charsLength = chars.Length;
        for (int i = 0; i < length; i++)
        {
            int index = random.Next(charsLength);
            result.Append(chars[index]);
        }

        return result.ToString();
    }
}
