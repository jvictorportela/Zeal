namespace Zeal.Domain.Entities;

public class Event : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public decimal Distance { get; set; }
    public int? MaxParticipants { get; set; }
    public int? MinimumAge { get; set; }
    public decimal RegistrationPrice { get; set; }
    public DateTime RegistrationStartDate { get; set; }
    public DateTime? RegistrationEndDate { get; set; }
    public long UserId { get; set; }
    public long AddressId { get; set; }
    public Address? Address { get; set; }
}
