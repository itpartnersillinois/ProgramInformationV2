using OpenSearch.Net;
using ProgramInformationV2.Search.Models;

namespace ProgramInformationV2.Search.Helpers {

    public class BulkEditor(OpenSearchLowLevelClient? openSearchLowLevelClient) {
        private readonly OpenSearchLowLevelClient _openSearchLowLevelClient = openSearchLowLevelClient ?? default!;

        public async Task<string> ChangeTags(string source, string oldTag, string newTag) {
            var responsePrograms = await _openSearchLowLevelClient.UpdateByQueryAsync<StringResponse>(UrlTypes.Programs.ConvertToUrlString(), GenerateUpdateJson(source));
            var responseCredentials = await _openSearchLowLevelClient.UpdateByQueryAsync<StringResponse>(UrlTypes.Programs.ConvertToUrlString(), GenerateUpdateJsonForCredentials(source));
            var responseCourses = await _openSearchLowLevelClient.UpdateByQueryAsync<StringResponse>(UrlTypes.Courses.ConvertToUrlString(), GenerateUpdateJson(source));
            return responsePrograms.Success && responseCourses.Success && responseCredentials.Success ? $"{source} updated: tag {oldTag} to {newTag}" : source + " not updated";
        }

        public async Task<string> DeleteAllItems(string source) {
            var responsePrograms = await _openSearchLowLevelClient.DeleteByQueryAsync<StringResponse>(UrlTypes.Programs.ConvertToUrlString(), GenerateDeleteJson(source));
            var responseCourses = await _openSearchLowLevelClient.DeleteByQueryAsync<StringResponse>(UrlTypes.Courses.ConvertToUrlString(), GenerateDeleteJson(source));
            var responseRequirementSets = await _openSearchLowLevelClient.DeleteByQueryAsync<StringResponse>(UrlTypes.RequirementSets.ConvertToUrlString(), GenerateDeleteJson(source));
            return responsePrograms.Success && responseCourses.Success && responseRequirementSets.Success ? source + " deleted" : source + " not deleted";
        }

        public async Task<string> DeleteTags(string source, string oldTag) {
            var responsePrograms = await _openSearchLowLevelClient.UpdateByQueryAsync<StringResponse>(UrlTypes.Programs.ConvertToUrlString(), GenerateDeleteJsonForTag(source));
            var responseCredentials = await _openSearchLowLevelClient.UpdateByQueryAsync<StringResponse>(UrlTypes.Programs.ConvertToUrlString(), GenerateDeleteJsonForCredentialTag(source));
            var responseCourses = await _openSearchLowLevelClient.UpdateByQueryAsync<StringResponse>(UrlTypes.Courses.ConvertToUrlString(), GenerateDeleteJsonForTag(source));
            return responsePrograms.Success && responseCourses.Success && responseCredentials.Success ? $"{source} deleted tag {oldTag}" : source + " not updated";
        }

        private string GenerateDeleteJson(string sourceCode) => "{\"query\": { \"bool\": { \"must\": { \"match_all\": { } }, \"filter\": [ { \"bool\": { \"must\": [ { \"term\": { \"source\":  \"" + sourceCode + "\" } } ] } } ] } } }";

        private string GenerateDeleteJsonForCredentialTag(string sourceCode) => "";

        private string GenerateDeleteJsonForTag(string sourceCode) => "";

        private string GenerateUpdateJson(string sourceCode) => "";

        private string GenerateUpdateJsonForCredentials(string sourceCode) => "";
    }
}