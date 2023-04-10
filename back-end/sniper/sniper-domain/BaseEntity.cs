using Newtonsoft.Json;

namespace sniper_domain;

public class BaseEntity
{
    public BaseEntity()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTimeOffset.UtcNow;
    }

    [JsonProperty("id")]
    public Guid Id { get; private set; }

    [JsonProperty("createdAt")]
    public DateTimeOffset CreatedAt { get; private set; }

    [JsonProperty("modifiedAt")]
    public DateTimeOffset? ModifiedAt { get; set; }
}