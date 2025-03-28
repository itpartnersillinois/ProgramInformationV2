namespace ProgramInformationV2.Search.Models {

    public class RequirementSet : BaseObject {

        public RequirementSet() {
            CreatedOn = DateTime.Now;
            LastUpdated = DateTime.Now;
            CourseRequirements = [];
            IsReused = false;
            SetId();
        }

        public IEnumerable<string> CourseProgramIdList => CourseRequirements.Where(c => c.IsActive).Select(c => c.Id);

        public IEnumerable<CourseRequirement> CourseRequirements { get; set; } = default!;

        public string CredentialId { get; set; } = "";

        public string Description { get; set; } = "";

        public override string InternalTitle => IsReused ? $"{Title} ({InternalTitleOverride})" : Title;

        public string InternalTitleOverride { get; set; } = "";

        public bool IsReused { get; set; }

        public int MaximumCreditHours { get; set; }

        public int MinimumCreditHours { get; set; }

        public override void CleanHtmlFields() {
            Description = CleanHtml(Description);
            if (CourseRequirements != null && CourseRequirements?.Count() > 0) {
                CourseRequirements = [.. CourseRequirements.OrderBy(s => s.Title)];
            }
        }
    }
}