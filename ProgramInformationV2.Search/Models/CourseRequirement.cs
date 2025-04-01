using OpenSearch.Client;

namespace ProgramInformationV2.Search.Models {

    public class CourseRequirement : BaseObject {

        public CourseRequirement() {
            CreatedOn = DateTime.Now;
            LastUpdated = DateTime.Now;
        }

        [Keyword]
        public string CourseId { get; set; } = "";

        public string Description { get; set; } = "";

        [Keyword]
        public override string EditLink => "";

        public override bool IsIdValid => true;

        [Keyword]
        public string ParentId { get; set; } = "";

        public string Url { get; set; } = "";

        internal override string CreateId => Id = ParentId + "-" + Guid.NewGuid().ToString();
    }
}