using OpenSearch.Client;
using ProgramInformationV2.Search.Getters;
using ProgramInformationV2.Search.Models;

namespace ProgramInformationV2.Search.Setters {

    public class ProgramSetter(OpenSearchClient? openSearchClient, ProgramGetter? programGetter) {
        private readonly OpenSearchClient _openSearchClient = openSearchClient ?? default!;

        private readonly ProgramGetter _programGetter = programGetter ?? default!;

        public async Task<string> SetCredential(Credential credential) {
            credential.Prepare();
            var program = string.IsNullOrWhiteSpace(credential.ProgramId) ? new Program() : await _programGetter.GetProgram(credential.ProgramId);

            if (string.IsNullOrWhiteSpace(credential.ProgramId)) {
                program.Source = credential.Source;
                program.Id = credential.Id + "-program";
                program.Title = credential.Title + " Program";
                program.IsActive = credential.IsActive;
                program.Credentials.Add(credential);
            } else {
                program.Credentials.RemoveAll(c => c.Id == credential.Id);
                program.Credentials.Add(credential);
            }
            program.Prepare();
            var response = await _openSearchClient.IndexAsync(program, i => i.Index(UrlTypes.Programs.ConvertToUrlString()));
            return response.IsValid ? credential.Id : "";
        }

        public async Task<string> SetProgram(Program program) {
            program.Prepare();
            var response = await _openSearchClient.IndexAsync(program, i => i.Index(UrlTypes.Programs.ConvertToUrlString()));
            return response.IsValid ? program.Id : "";
        }
    }
}