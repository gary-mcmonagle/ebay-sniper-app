using sniper_domain.Responses;

namespace BidApi.User;

public static class CreateUser
{
    [FunctionName("CreateUser")]
    [OpenApiOperation(operationId: "Create User")]
    // [OpenApiRequestBody(contentType: "text/json", bodyType: typeof(CreateBidRequest), Required = true, Description = "The bid to create")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/json", bodyType: typeof(GetUserResponse), Description = "Returns a 200 response with text")]

    public static async Task<ActionResult<EbayBidEntity>> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "user")] HttpRequest req,
      [CosmosDB(
                Constants.UserDatabaseName,
                Constants.UserCollectionName,
                Connection = "CosmosDBConnection")] IAsyncCollector<UserEntity> users,
        ILogger log)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");
        var content = await new StreamReader(req.Body).ReadToEndAsync();
        //var request = JsonConvert.DeserializeObject<CreateBidRequest>(content);

        var user = new UserEntity();
        await users.AddAsync(user);

        return new OkObjectResult(new GetUserResponse { Id = user.Id });
    }
}

