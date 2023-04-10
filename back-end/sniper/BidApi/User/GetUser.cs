using sniper_domain.Responses;

namespace BidApi.User;

public static class GetUser
{
    [FunctionName("GetUser")]
    [OpenApiOperation(operationId: "Get User")]
    [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The ID of the user to get")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/json", bodyType: typeof(GetUserResponse), Description = "Returns a 200 response with text")]
    public static async Task<ActionResult<GetUserResponse>> Run(
    [HttpTrigger(AuthorizationLevel.Function, "get", Route = "user/{id}")] HttpRequest req,
        [CosmosDB(Constants.UserDatabaseName, Constants.UserCollectionName,
                Connection = "CosmosDBConnection",
                SqlQuery = "select * from Users u where u.id = {id}")]
                IEnumerable<UserEntity> users,
    ILogger log,
    string id)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        if (string.IsNullOrEmpty(id))
        {
            return new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }

        var user = users?.FirstOrDefault();

        if (user == null)
        {
            return new NotFoundObjectResult($"User with id {id} not found");
        }


        return new OkObjectResult(new GetUserResponse { Id = user.Id, Bids = user.Bids });
    }
}