namespace ProgramInformationV2.Search.Models {
    public class CourseIdentifier {
        public CourseIdentifier() {
        }

        public CourseIdentifier(string rubric, string courseNumber) {
            Rubric = rubric;
            CourseNumber = courseNumber;
        }

        public string CourseNumber { get; set; } = "";

        public string Fragment { get; set; } = "";

        public string Id { get; set; } = "";

        public string Notes { get; set; } = "";

        public string Rubric { get; set; } = "";

        public string Source { get; set; } = "";
    }
}
