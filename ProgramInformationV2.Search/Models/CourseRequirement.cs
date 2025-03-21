namespace ProgramInformationV2.Search.Models {
    public class CourseRequirement : BaseObject {
        public CourseRequirement() {
            ConcurrentRegistration = null;
            Course = new CourseIdentifier();
            Prerequisites = null;
            Requirements = null;
            CreatedOn = DateTime.Now;
            LastUpdated = DateTime.Now;
        }

        public CourseRequirement(string id, Course course) {
            ConcurrentRegistration = null;
            Prerequisites = null;
            Requirements = null;
            Course = new CourseIdentifier {
                CourseNumber = course.CourseNumber,
                Rubric = course.Rubric,
                Source = course.Source,
            };
            MinimumCreditHours = course.MinimumCreditHours;
            MaximumCreditHours = course.MaximumCreditHours;
            Title = course.Title;
            Source = course.Source;
            ParentId = id;
            Url = course.Url;
            Fragment = course.Fragment;
            CreatedOn = DateTime.Now;
            LastUpdated = DateTime.Now;
            SetId();
        }

        public IEnumerable<CourseIdentifier>? ConcurrentRegistration { get; set; }

        public CourseIdentifier Course { get; set; } = default!;

        public string CourseNumber => Course.CourseNumber;

        public string CourseSource => Course.Source;

        public string CreditHours { get; set; } = "";

        public string Description { get; set; } = "";

        public override bool IsIdValid => true;

        public int MaximumCreditHours { get; set; }

        public int MinimumCreditHours { get; set; }

        public string ParentId { get; set; } = "";

        public IEnumerable<CourseIdentifier>? Prerequisites { get; set; }

        public IEnumerable<CourseIdentifier>? Requirements { get; set; }

        public string Rubric => Course.Rubric;

        public string Url { get; set; } = "";

        internal override string CreateId => Id = !string.IsNullOrWhiteSpace(Rubric) && !string.IsNullOrWhiteSpace(CourseNumber) ?
            ParentId + "-" + Rubric + "-" + CourseNumber + "-" + CourseSource :
            ParentId + "-" + Guid.NewGuid().ToString();
    }
}

