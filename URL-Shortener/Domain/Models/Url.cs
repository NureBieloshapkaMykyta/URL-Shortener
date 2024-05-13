namespace Domain.Models;

public class Url
{
    public Guid Id { get; set; }

    public string BaseUrl { get; set; }

    public string ShorteredUrl { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual Guid CreatorId { get; set; }

    public AppUser Creator { get; set; }
}
