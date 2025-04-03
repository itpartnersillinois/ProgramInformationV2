using OpenSearch.Client;
using ProgramInformationV2.Search.Models;

namespace ProgramInformationV2.Search.Getters {

    public class RequirementSetGetter(OpenSearchClient? openSearchClient) : BaseGetter<RequirementSet>(openSearchClient) {

        public async Task<List<GenericItem>> GetAllRequirementSetsBySource(string source, string search) {
            var response = string.IsNullOrWhiteSpace(search) ?
                await _openSearchClient.SearchAsync<RequirementSet>(s => s.Index(UrlTypes.RequirementSets.ConvertToUrlString()).Query(q => q.Match(m => m.Field(fld => fld.Source).Query(source)) && q.Term(m => m.IsReused, true))) :
                await _openSearchClient.SearchAsync<RequirementSet>(s => s.Index(UrlTypes.RequirementSets.ConvertToUrlString())
                    .Query(m => m.Match(m => m.Field(fld => fld.Source).Query(source)) && m.Match(m => m.Field(fld => fld.InternalTitle).Query(search)) && m.Term(m => m.IsReused, true)));
            LogDebug(response);
            return response.IsValid ? response.Documents.Select(r => r.GetGenericItem()).OrderBy(g => g.Title).ToList() : [];
        }

        public async Task<List<GenericItem>> GetAllRequirementSetsBySourceIncludingPrivate(string source, string search, string credentialId) {
            var response = string.IsNullOrWhiteSpace(search) ?
                await _openSearchClient.SearchAsync<RequirementSet>(s => s.Index(UrlTypes.RequirementSets.ConvertToUrlString())
                    .Query(q => q.Match(m => m.Field(fld => fld.Source).Query(source)) && (q.Term(m => m.IsReused, true) || q.Term(m => m.CredentialId, credentialId))))
                :
                await _openSearchClient.SearchAsync<RequirementSet>(s => s.Index(UrlTypes.RequirementSets.ConvertToUrlString())
                    .Query(m => m.Match(m => m.Field(fld => fld.Source).Query(source)) && m.Match(m => m.Field(fld => fld.InternalTitle).Query(search)) && m.Term(m => m.IsReused, true)));
            LogDebug(response);
            return response.IsValid ? response.Documents.Select(r => r.GetGenericItem()).OrderBy(g => g.Title).ToList() : [];
        }

        public async Task<RequirementSet> GetRequirementSet(string id) {
            if (string.IsNullOrWhiteSpace(id)) {
                return new();
            }
            var response = await _openSearchClient.GetAsync<RequirementSet>(id);
            LogDebug(response);
            return response.IsValid ? response.Source : new RequirementSet();
        }

        public async Task<List<RequirementSet>> GetRequirementSets(IEnumerable<string> ids) {
            if (ids == null || !ids.Any()) {
                return [];
            }
            var response = await _openSearchClient.SearchAsync<RequirementSet>(s => s.Index(UrlTypes.RequirementSets.ConvertToUrlString())
                .Query(q => q
                .Bool(b => b
                .Filter(f => f.Terms(m => m.Field(fld => fld.Id).Terms(ids))))));
            LogDebug(response);
            return response.IsValid ? [.. response.Documents] : [];
        }

        public async Task<List<GenericItem>> GetRequirementSetsChosen(IEnumerable<string> ids) => [.. (await GetRequirementSets(ids)).Select(r => r.GetGenericItem())];
    }
}