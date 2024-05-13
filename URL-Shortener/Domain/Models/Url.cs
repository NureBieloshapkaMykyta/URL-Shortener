namespace Domain.Models;

public class Url
{
    public Guid Id { get; set; }

    public string BaseUrl { get; set; }

    public string ShorteredUrl { get; set; }

    public DateTime ModifiedDate { get; set; }

    public Guid CreatorId { get; set; }

    public virtual AppUser Creator { get; set; }
}
