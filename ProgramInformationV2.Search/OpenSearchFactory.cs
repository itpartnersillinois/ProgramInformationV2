using Amazon;
using Amazon.Runtime;
using OpenSearch.Client;
using OpenSearch.Net.Auth.AwsSigV4;
using ProgramInformationV2.Search.Models;

namespace ProgramInformationV2.Search {

    public static class OpenSearchFactory {

        public static OpenSearchClient CreateClient(string? baseUrl, string? accessKey, string? secretKey, bool debug) {
            var nodeAddress = new Uri(baseUrl ?? "");
            var connection = string.IsNullOrWhiteSpace(accessKey) || string.IsNullOrWhiteSpace(secretKey) ? null : new AwsSigV4HttpConnection(new BasicAWSCredentials(accessKey, secretKey), RegionEndpoint.USEast2);
            var config = new ConnectionSettings(nodeAddress, connection);
            if (debug) {
                config.DisableDirectStreaming(true);
            }
            var client = new OpenSearchClient(config);
            client.ConnectionSettings.DefaultIndices.Add(typeof(Program), UrlTypes.Programs.ConvertToUrlString());
            client.ConnectionSettings.DefaultIndices.Add(typeof(Course), UrlTypes.Courses.ConvertToUrlString());
            client.ConnectionSettings.DefaultIndices.Add(typeof(RequirementSet), UrlTypes.RequirementSets.ConvertToUrlString());
            return client;
        }

        public static string MapIndex(OpenSearchClient openSearchClient) {
            var returnValue = "";
            var index1 = openSearchClient.Indices.Create(UrlTypes.Programs.ConvertToUrlString(), c => c.Map(m => m.AutoMap<Program>()));
            returnValue += index1.IsValid ? $"Program created; " : $"Program failed - {index1.DebugInformation}; ";
            var index2 = openSearchClient.Indices.Create(UrlTypes.Courses.ConvertToUrlString(), c => c.Map(m => m.AutoMap<Course>()));
            returnValue += index2.IsValid ? $"Course created; " : $"Course failed - {index2.DebugInformation}; ";
            var index3 = openSearchClient.Indices.Create(UrlTypes.RequirementSets.ConvertToUrlString(), c => c.Map(m => m.AutoMap<RequirementSet>()));
            returnValue += index3.IsValid ? $"Req Set created; " : $"Req Set failed - {index3.DebugInformation}; ";
            return returnValue;
        }
    }
}