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

        public async Task<string> RequestAccess(string sourceCode, string email) {
            var source = await _programRepository.ReadAsync(c => c.Sources.FirstOrDefault(s => s.Code == sourceCode));
            if (source == null) {
                return "Source Code not found";
            }

            var existingItem = await _programRepository.ReadAsync(c => c.SecurityEntries.FirstOrDefault(s => s.SourceId == source.Id && s.Email == email));
            if (existingItem != null) {
                if (existingItem.IsActive) {
                    return "You already have access";
                } else if (existingItem.IsRequested) {
                    return "You entry is pending";
                } else {
                    return "You entry has been rejected -- please contact the owner for more information";
                }
            }

            var value = await _programRepository.CreateAsync(new SecurityEntry(email, source.Id, true));
            return $"Requested access to code {sourceCode}";
        }
    }
}
