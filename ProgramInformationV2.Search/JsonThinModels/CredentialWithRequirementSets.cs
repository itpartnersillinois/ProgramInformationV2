using ProgramInformationV2.Search.Models;

namespace ProgramInformationV2.Search.JsonThinModels {

    public class CredentialWithRequirementSets {
        public Credential Credential { get; set; } = new();
        public List<CredentialOption> OtherCredentials { get; set; } = [];
        public Link Program { get; set; } = new();
        public List<RequirementSet> RequirementSets { get; set; } = [];
    }
}