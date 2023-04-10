using Newtonsoft.Json;

namespace sniper_domain.Responses;
public class GetUserResponse
{
    [JsonProperty("id")]
    public Guid Id { get; set; }

    [JsonProperty("bids")]
    public IEnumerable<EbayBidEntity> Bids { get; set; } = new List<EbayBidEntity>();
}