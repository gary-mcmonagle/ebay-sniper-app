using System.Collections.ObjectModel;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using sniper_domain;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System.Net;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace BidApi;

public static class BidApi
{
    [FunctionName("Get")]
    [OpenApiOperation(operationId: "Get Bid")]
    [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "The ID of the bid to get")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/json", bodyType: typeof(EbayBidEntity), Description = "Returns a 200 response with text")]

    public static async Task<ActionResult<EbayBidEntity>> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "bid/{id}")] HttpRequest req,
            [CosmosDB("EbayBids", "Bids",
                Connection = "CosmosDBConnection",
                SqlQuery = "select * from Bids r where r.EbayItemId = {id}")]
                IEnumerable<EbayBidEntity> bids,
        ILogger log,
        string id)
    {
        log.LogInformation("C# HTTP trigger function processed a request.");

        if (string.IsNullOrEmpty(id))
        {
            return new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }

        var bid = bids?.FirstOrDefault();

        if (bid == null)
        {
            return new NotFoundObjectResult($"Bid with id {id} not found");
        }

        return new OkObjectResult(bid);
    }
}

