namespace Domain.Models;

public abstract class ChangesTrackingEntity
{
    public DateTime ModifiedDate { get; set; }
}
