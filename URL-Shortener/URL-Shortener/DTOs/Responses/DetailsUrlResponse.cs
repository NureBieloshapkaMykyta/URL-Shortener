namespace URL_Shortener.DTOs.Responses;

public class DetailsUrlResponse
{
    public Guid Id { get; set; }

    public string BaseUrl { get; set; }

    public string ShorteredUrl { get; set; }

    public DateTime ModifiedDate { get; set; }

    public DisplayUserResponse Creator { get; set; }
}
