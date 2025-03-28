using OpenSearch.Client;
using ProgramInformationV2.Search.Models;

namespace ProgramInformationV2.Search.Setters {

    public class RequirementSetSetter(OpenSearchClient? openSearchClient) {
        private readonly OpenSearchClient _openSearchClient = openSearchClient ?? default!;

        public async Task<string> DeleteRequirementSet(string id) {
            var response = await _openSearchClient.DeleteAsync<RequirementSet>(id, d => d.Index(UrlTypes.RequirementSets.ConvertToUrlString()));
            return response.IsValid ? $"Requirement Set {id} deleted" : "error";
        }

        public async Task<string> SetRequirementSet(RequirementSet requirementSet) {
            requirementSet.Prepare();
            var response = await _openSearchClient.IndexAsync(requirementSet, i => i.Index(UrlTypes.RequirementSets.ConvertToUrlString()));
            return response.IsValid ? requirementSet.Id : "";
        }
    }
}