namespace ProgramInformationV2.Search.Models {

    public class CourseRequirement : BaseObject {

        public CourseRequirement() {
            ConcurrentRegistration = null;
            Prerequisites = null;
            Requirements = null;
            CreatedOn = DateTime.Now;
            LastUpdated = DateTime.Now;
        }

        public IEnumerable<CourseIdentifier>? ConcurrentRegistration { get; set; }

        public string CourseId { get; set; } = "";
        public string Description { get; set; } = "";

        public override bool IsIdValid => true;

        public string ParentId { get; set; } = "";

        public IEnumerable<CourseIdentifier>? Prerequisites { get; set; }

        public IEnumerable<CourseIdentifier>? Requirements { get; set; }

        public string Url { get; set; } = "";

        internal override string CreateId => Id = ParentId + "-" + Guid.NewGuid().ToString();
    }
}