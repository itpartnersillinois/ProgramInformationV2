using OpenSearch.Net;
using ProgramInformationV2.Search.Models;

namespace ProgramInformationV2.Search.Helpers {

    public class BulkEditor(OpenSearchLowLevelClient? openSearchLowLevelClient) {
        private readonly OpenSearchLowLevelClient _openSearchLowLevelClient = openSearchLowLevelClient ?? default!;

        public async Task<string> DeleteAllItems(string source) {
            var responsePrograms = await _openSearchLowLevelClient.DeleteByQueryAsync<StringResponse>(UrlTypes.Programs.ConvertToUrlString(), GenerateDeleteJson(source));
            var responseCourses = await _openSearchLowLevelClient.DeleteByQueryAsync<StringResponse>(UrlTypes.Courses.ConvertToUrlString(), GenerateDeleteJson(source));
            var responseRequirementSets = await _openSearchLowLevelClient.DeleteByQueryAsync<StringResponse>(UrlTypes.RequirementSets.ConvertToUrlString(), GenerateDeleteJson(source));
            return responsePrograms.Success && responseCourses.Success && responseRequirementSets.Success ? source + " deleted" : source + " not deleted";
        }

        public async Task<string> DeleteTags(string source, string listName, string oldTag) => await UpdateTags(source, listName, oldTag, "");

        public async Task<bool> UpdateRequirementSets(string courseId, string courseTitle, string courseUrl) {
            var responseRequirementSets = await _openSearchLowLevelClient.UpdateByQueryAsync<StringResponse>(UrlTypes.RequirementSets.ConvertToUrlString(), GenerateUpdateJsonForCourseRequirements(courseId, courseTitle, courseUrl));
            return responseRequirementSets.Success;
        }

        public async Task<string> UpdateTags(string source, string listName, string oldTag, string newTag) {
            var responsePrograms = await _openSearchLowLevelClient.UpdateByQueryAsync<StringResponse>(UrlTypes.Programs.ConvertToUrlString(), GenerateUpdateJson(source, listName, oldTag, newTag));
            var responseCredentials = await _openSearchLowLevelClient.UpdateByQueryAsync<StringResponse>(UrlTypes.Programs.ConvertToUrlString(), GenerateUpdateJsonForCredentials(source, listName, oldTag, newTag));
            var responseCourses = await _openSearchLowLevelClient.UpdateByQueryAsync<StringResponse>(UrlTypes.Courses.ConvertToUrlString(), GenerateUpdateJson(source, listName, oldTag, newTag));
            return responsePrograms.Success && responseCourses.Success && responseCredentials.Success ? $"{source} updated: tag {oldTag} to {newTag}" : source + " not updated";
        }

        private string GenerateDeleteJson(string sourceCode) => "{\"query\": { \"bool\": { \"must\": { \"match_all\": { } }, \"filter\": [ { \"bool\": { \"must\": [ { \"term\": { \"source\":  \"" + sourceCode + "\" } } ] } } ] } } }";

        private string GenerateUpdateJson(string sourceCode, string listName, string oldText, string newText) => "{ \"script\": { \"source\": \"int i = 0; for (elem in ctx._source." + listName + ") { if (elem == '" + Sanitize(oldText) + "') { ctx._source." + listName + "[i] = '" + Sanitize(newText) + "' } i++; }\", \"lang\": \"painless\" }, \"query\": { \"bool\": { \"filter\": [ { \"term\": { \"source\": \"" + sourceCode + "\" } }, { \"term\": { \"" + listName + "\": \"" + Sanitize(oldText) + "\" } } ] } } }";

        private string GenerateUpdateJsonForCourseRequirements(string id, string title, string url) => "{ \"script\": { \"source\": \"int i = 0; for (elem in ctx._source.courseRequirements) { if (elem.courseId == '" + id + "') { ctx._source.courseRequirements[i].title = '" + Sanitize(title) + "'; ctx._source.courseRequirements[i].internalTitle = '" + Sanitize(title) + "'; ctx._source.courseRequirements[i].url = '" + Sanitize(url) + "';  } i++; }\", \"lang\": \"painless\" }, \"query\": { \"bool\": { \"filter\": [ { \"term\": { \"courseRequirements.courseId\": \"" + id + "\" } } ] } } }";

        private string GenerateUpdateJsonForCredentials(string sourceCode, string listName, string oldText, string newText) => "{ \"script\": { \"source\": \"int i = 0; for (cred in ctx._source.credentials) { int j = 0; for (elem in cred." + listName + ") { if (elem == '" + Sanitize(oldText) + "') { ctx._source.credentials[i]." + listName + "[j] = '" + Sanitize(newText) + "' } j++; } i++; }\", \"lang\": \"painless\" }, \"query\": { \"bool\": { \"filter\": [ { \"term\": { \"source\": \"" + sourceCode + "\" } }, { \"term\": { \"credentials." + listName + "\": \"" + Sanitize(oldText) + "\" } } ] } } }";

        private string Sanitize(string value) => value.Replace("'", "\\\\`");
    }
}