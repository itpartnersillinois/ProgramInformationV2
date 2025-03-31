using System.Xml.Linq;

namespace ProgramInformationV2.Data.CourseImport {

    public class ScheduleCourse {
        public string CourseNumber { get; set; } = "";
        public string CreditHours { get; set; } = "";
        public string Description { get; set; } = "";
        public string Rubric { get; set; } = "";
        public string SectionDateRange { get; set; } = "";
        public List<ScheduleSection> Sections { get; set; } = default!;
        public string Title { get; set; } = "";

        public bool AddXml(XDocument xml, string rubric, string courseNumber, string term) {
            Rubric = rubric;
            CourseNumber = courseNumber;
            if (string.IsNullOrEmpty(Title)) {
                Title = xml.Descendants("label").FirstOrDefault()?.Value?.Trim() ?? "";
            }
            if (string.IsNullOrEmpty(Description)) {
                Description = xml.Descendants("description").FirstOrDefault()?.Value?.Trim() ?? "";
            }
            if (string.IsNullOrEmpty(CreditHours)) {
                CreditHours = xml.Descendants("creditHours").FirstOrDefault()?.Value?.Trim() ?? "";
            }
            if (string.IsNullOrEmpty(SectionDateRange)) {
                SectionDateRange = xml.Descendants("sectionDateRange").FirstOrDefault()?.Value?.Trim() ?? "";
            }
            if (Sections == null) {
                Sections = xml.Descendants("detailedSections").FirstOrDefault()?.Elements().Select(x => new ScheduleSection(x, term)).ToList() ?? [];
            } else {
                Sections.AddRange(xml.Descendants("detailedSections").FirstOrDefault()?.Elements().Select(x => new ScheduleSection(x, term)) ?? []);
            }
            return true;
        }
    }
}