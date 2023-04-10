using Newtonsoft.Json;
using sniper_domain.Requests;

namespace sniper_domain;

public class EbayBidEntity : BaseEntity
{
    [JsonConstructor]
    public EbayBidEntity(string ebayItemId, double bidAmount) : base()
    {
        EbayItemId = ebayItemId;
        BidAmount = bidAmount;
    }

    public EbayBidEntity(CreateBidRequest request) : base()
    {
        EbayItemId = request.EbayItemId;
        BidAmount = request.BidAmount;
    }

    [JsonProperty("ebayItemId")]
    public string EbayItemId { get; set; }

    [JsonProperty("bidAmount")]
    public double BidAmount { get; set; }
}

