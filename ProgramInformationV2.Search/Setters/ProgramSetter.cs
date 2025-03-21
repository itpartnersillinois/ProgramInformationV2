using OpenSearch.Client;
using ProgramInformationV2.Search.Models;

namespace ProgramInformationV2.Search.Setters {
    public class ProgramSetter(OpenSearchClient? openSearchClient) {

        private readonly OpenSearchClient _openSearchClient = openSearchClient ?? default!;

        public async Task<string> SetProgram(Program program) {
            program.Prepare();
            var response = await _openSearchClient.IndexAsync(program, i => i.Index(UrlTypes.Programs.ConvertToUrlString()));
            return response.IsValid ? program.Id : "";
        }
    }
}
