using Zeal.Communication.Requests.Address;

namespace Zeal.Communication.Requests.Event;

// Vai ser a mesma para o Create e Update de eventos
public class RequestEventJson
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } // Data de início do evento
    public decimal Distance { get; set; }
    public int? MaxParticipants { get; set; }
    public int? MinimumAge { get; set; }
    public decimal RegistrationPrice { get; set; }
    public DateTime RegistrationStartDate { get; set; } // Data de início das inscrições
    public DateTime? RegistrationEndDate { get; set; } // Data de término das inscrições

    public AddressJson? Address { get; set; } // Endereço do evento
}
