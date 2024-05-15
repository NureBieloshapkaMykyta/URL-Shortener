namespace URL_Shortener.Helpers;

public class BaseAlhoritmConstants
{
    public const string Description = "Input URL: The algorithm takes a long URL as input. This long URL is typically lengthy and may contain various characters, making it cumbersome to share or remember.\r\n\r\nShortening Process: The algorithm generates a shorter version of the input URL. This shortened URL typically consists of fewer characters, making it easier to share, especially in contexts like social media posts or text messages where character limits may apply.\r\n\r\nRandom Generation: To ensure uniqueness and prevent predictable short URLs, the algorithm often incorporates a random generation component. It generates a random string of characters.\r\n\r\nRedirection: When a user accesses the shortened URL, the algorithm looks up the original URL associated with it in the mapping/database. It then redirects the user's browser to the corresponding original URL, effectively transparently navigating the user to the intended destination.";

    public const string Name = "S-Url";
}
