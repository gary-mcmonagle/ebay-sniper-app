using Newtonsoft.Json;

namespace sniper_domain;

public class UserEntity : BaseEntity
{
    [JsonProperty("bids")]
    public List<EbayBidEntity> Bids { get; set; } = new List<EbayBidEntity>();
}