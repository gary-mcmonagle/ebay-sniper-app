using Microsoft.Azure.Cosmos;
using sniper_domain.Requests;

namespace BidApi.User.Bid;

public static class CreateBid
{
    private static readonly string CosmosDBConnection = Environment.GetEnvironmentVariable("CosmosDBConnection");
    private static CosmosClient cosmosclient = new CosmosClient(CosmosDBConnection);


    [FunctionName("CreateBid")]
    [OpenApiOperation(operationId: "Create Bid new")]
    [OpenApiParameter(name: "userId", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The ID of the user to get")]
    [OpenApiRequestBody(contentType: "text/json", bodyType: typeof(CreateBidRequest), Required = true, Description = "The bid to create")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/json", bodyType: typeof(void), Description = "Returns a 200 response with text")]

    public static async Task<ActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "{userId}/bid")] HttpRequest req,
        ILogger log,
        string userId)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");
        var content = await new StreamReader(req.Body).ReadToEndAsync();
        var request = JsonConvert.DeserializeObject<CreateBidRequest>(content);
        var container = cosmosclient.GetContainer(Constants.UserDatabaseName, Constants.UserCollectionName);

        try
        {
            var user = await container.ReadItemAsync<UserEntity>(userId, new PartitionKey(userId));

            user.Resource.Bids.Add(new EbayBidEntity(request));
            await container.ReplaceItemAsync<UserEntity>(user, user.Resource.Id.ToString(), new PartitionKey(user.Resource.Id.ToString()));

            return new OkResult();
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return new NotFoundObjectResult($"User with id {userId} not found");
        }
    }
}
