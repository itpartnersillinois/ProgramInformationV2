namespace ProgramInformationV2.Search.Models {
    public class SectionFaculty {
        public string Name { get; set; } = "";

        public string NetId { get; set; } = "";

        public string Url { get; set; } = "";

        public override string ToString() => string.IsNullOrWhiteSpace(Url) ? Name : $"<a href='{Url}'>{Name}</a>";
    }
}
