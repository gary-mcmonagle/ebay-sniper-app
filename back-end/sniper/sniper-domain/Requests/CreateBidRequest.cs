using Newtonsoft.Json;

namespace sniper_domain.Requests;
public class CreateBidRequest
{
    public CreateBidRequest(string ebayItemId, double bidAmount)
    {
        EbayItemId = ebayItemId;
        BidAmount = bidAmount;
    }

    [JsonProperty("ebayItemId")]
    public string EbayItemId { get; set; }

    [JsonProperty("bidAmount")]
    public double BidAmount { get; set; }

}