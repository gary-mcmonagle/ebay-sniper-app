namespace sniper_domain;

public class EbayBidEntity
{
    public EbayBidEntity(string ebayItemId, double bidAmount)
    {
		EbayItemId = ebayItemId;
		BidAmount = bidAmount;
    }

    public string EbayItemId { get; set; }
	public double BidAmount { get; set; }
}

