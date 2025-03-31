namespace ProgramInformationV2.Data.CourseImport {

    public class CourseUrl {
        public string CourseNumber { get; set; } = "";
        public string Rubric { get; set; } = "";
        public string Semester { get; set; } = "";
        public string Url { get; set; } = "";
        public int Year { get; set; }

        public override string ToString() => $"{Rubric} {CourseNumber}";
    }
}