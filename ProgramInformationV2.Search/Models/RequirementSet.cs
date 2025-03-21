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

        public string Description { get; set; } = "";

        public string DisplayedTitle { get; set; } = "";

        public bool IsReused { get; set; }

        public int MaximumCreditHours { get; set; }

        public int MinimumCreditHours { get; set; }

        public override void CleanHtmlFields() => Description = CleanHtml(Description);

    }
}
