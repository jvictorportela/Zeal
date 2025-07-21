namespace Zeal.Communication.Requests.Event;

public class RequestFilterEventJson
{
    public string? Name { get; set; }
    public IList<DateTime> StartDate { get; set; } = [];
    public IList<decimal> Distance { get; set; } = [];
}
