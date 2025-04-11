using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProgramInformationV2.Function.Helper;
using ProgramInformationV2.Search.Getters;
using ProgramInformationV2.Search.Models;

namespace ProgramInformationV2.Function {

    public class GetSections(CourseGetter courseGetter, ILogger<GetSections> logger) {
        private readonly CourseGetter _courseGetter = courseGetter;
        private readonly ILogger<GetSections> _logger = logger;

        [Function("Section")]
        [OpenApiOperation(operationId: "Section", tags: "Get Section Information", Description = "Get a section of a course.")]
        [OpenApiParameter(name: "id", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The id of the section (this includes the source).")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(Course), Description = "The section")]
        public async Task<HttpResponseData> Id([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req) {
            _logger.LogInformation("Called Section.");
            var requestHelper = RequestHelperFactory.Create();
            requestHelper.Initialize(req);
            var id = requestHelper.GetRequest(req, "id");
            requestHelper.Validate();
            var returnItem = await _courseGetter.GetSection(id);
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(returnItem);
            return response;
        }
    }
}