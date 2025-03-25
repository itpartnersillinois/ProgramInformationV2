using OpenSearch.Client;
using ProgramInformationV2.Search.Models;

namespace ProgramInformationV2.Search.Getters {

    public class ProgramGetter : BaseGetter<Program> {

        public ProgramGetter(OpenSearchClient? openSearchClient) : base(openSearchClient) {
        }

        public async Task<List<GenericItem>> GetAllCredentialsBySource(string source, string search) {
            var response = string.IsNullOrWhiteSpace(search) ?
                await _openSearchClient.SearchAsync<Program>(s => s.Index(UrlTypes.Programs.ConvertToUrlString()).Query(q => q.Match(m => m.Field(fld => fld.Source).Query(source)))) :
                await _openSearchClient.SearchAsync<Program>(s => s.Index(UrlTypes.Programs.ConvertToUrlString())
                    .Query(m => m.Match(m => m.Field(fld => fld.Source).Query(source))
                        && (m.Match(m => m.Field(fld => fld.Title).Query(search)) || m.Match(m => m.Field(fld => fld.Credentials.Select(ft => ft.Title)).Query(search)))));
            LogDebug(response);
            return response.IsValid ? response.Documents.SelectMany(c => c.Credentials).Select(r => r.GetGenericItem()).OrderBy(g => g.Title).ToList() : new List<GenericItem>();
        }

        public async Task<List<GenericItem>> GetAllProgramsBySource(string source, string search) {
            var response = string.IsNullOrWhiteSpace(search) ?
                await _openSearchClient.SearchAsync<Program>(s => s.Index(UrlTypes.Programs.ConvertToUrlString()).Query(q => q.Match(m => m.Field(fld => fld.Source).Query(source)))) :
                await _openSearchClient.SearchAsync<Program>(s => s.Index(UrlTypes.Programs.ConvertToUrlString())
                    .Query(m => m.Match(m => m.Field(fld => fld.Source).Query(source)) && m.Match(m => m.Field(fld => fld.Title).Query(search))));
            LogDebug(response);
            return response.IsValid ? response.Documents.Select(r => r.GetGenericItem()).OrderBy(g => g.Title).ToList() : new List<GenericItem>();
        }

        public async Task<Credential> GetCredential(string credentialId) {
            var program = await GetProgramByCredential(credentialId);
            return program.Credentials?.SingleOrDefault(c => c.Id == credentialId) ?? new Credential();
        }

        public async Task<Program> GetProgram(string id) {
            var response = await _openSearchClient.GetAsync<Program>(id);
            LogDebug(response);
            return response.IsValid ? response.Source : new Program();
        }

        public async Task<Program> GetProgramByCredential(string credentialId) {
            var response = await _openSearchClient.SearchAsync<Program>(s => s.Index(UrlTypes.Programs.ConvertToUrlString()).Query(q => q.Match(m => m.Field(fld => fld.CredentialIdList).Query(credentialId))));
            LogDebug(response);
            return response.IsValid ? response.Documents.FirstOrDefault() ?? new Program() : new Program();
        }
    }
}