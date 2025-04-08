using OpenSearch.Client;

namespace ProgramInformationV2.Search.Models {

    public class SectionFaculty : IEquatable<SectionFaculty> {
        public string Name { get; set; } = "";

        [Keyword]
        public string NetId { get; set; } = "";

        public bool ShowInProfile { get; set; }
        public string Url { get; set; } = "";

        public bool Equals(SectionFaculty? other) => Name == other?.Name && NetId == other?.NetId;

        public override bool Equals(object? obj) => obj == null ? false : ((SectionFaculty) obj).Name == Name && ((SectionFaculty) obj).NetId == NetId;

        public override int GetHashCode() => (Name, NetId).GetHashCode();

        public override string ToString() => string.IsNullOrWhiteSpace(Url) ? Name : $"<a href='{Url}'>{Name}</a>";
    }
}