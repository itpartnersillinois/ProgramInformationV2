using OpenSearch.Client;
using ProgramInformationV2.Search.Models;

namespace ProgramInformationV2.Search.Getters {
    public class ProgramGetter(OpenSearchClient? openSearchClient) {

        private readonly OpenSearchClient _openSearchClient = openSearchClient ?? default!;

        public async Task<Program> GetProgram(string id) {
            var response = await _openSearchClient.GetAsync<Program>(id);
            return response.IsValid ? response.Source : new Program();
        }

        public async Task<List<GenericItem>> GetAllProgramsBySource(string source) {
            var response = await _openSearchClient.SearchAsync<Program>(s => s.Index(UrlTypes.Programs.ConvertToUrlString()).Query(q => q.Match(m => m.Field(fld => fld.Source).Query(source))));
            return response.IsValid ? response.Documents.Select(r => r.GetGenericItem()).ToList() : new List<GenericItem>();
        }
    }
}
