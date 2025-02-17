using ProgramInformationV2.Data.DataContext;
using ProgramInformationV2.Data.DataModels;

namespace ProgramInformationV2.Data.DataHelpers {
    public class SourceHelper(ProgramRepository programRepository) {
        private readonly ProgramRepository _programRepository = programRepository;

        public async Task<string> CreateSource(string newSourceCode, string newTitle, string email) {
            var source = await _programRepository.ReadAsync(c => c.Sources.FirstOrDefault(s => s.Code == newSourceCode.ToLowerInvariant() || s.Title == newTitle));
            if (source != null) {
                return "Source code or name is in use";
            }
            _ = await _programRepository.CreateAsync(new Source { Code = newSourceCode.ToLowerInvariant(), CreatedByEmail = email, IsActive = true, IsTest = false, Title = newTitle });
            var newSource = await _programRepository.ReadAsync(pr => pr.Sources.FirstOrDefault(s => s.Code == newSourceCode.ToLowerInvariant()));
            if (newSource != null) {
                _ = await _programRepository.CreateAsync(new SecurityEntry { SourceId = newSource.Id, IsActive = true, IsFullAdmin = true, IsOwner = true, IsPublic = true, IsRequested = false, Email = email });
            }
            return $"Added source {newTitle} with code {newSourceCode}";
        }
    }
}
