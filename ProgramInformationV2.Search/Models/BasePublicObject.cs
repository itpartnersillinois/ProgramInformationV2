namespace ProgramInformationV2.Search.Models {
    public abstract class BasePublicObject : BaseObject {
        public IEnumerable<string> DepartmentList { get; set; } = default!;

        public string Description { get; set; } = "";

        public bool IsRemovedFromSitemap { get; set; }

        public List<string> FixedFields { get; set; } = default!;

        public bool IsDeletable { get; set; } = false;

        public IEnumerable<string> SkillList { get; set; } = default!;

        public dynamic Suggest => new { input = Title, contexts = new { source = Source } };

        public IEnumerable<string> TagList { get; set; } = default!;

        public string Url { get; set; } = "";

        public static string ConvertVideoToEmbed(string href) {
            if (string.IsNullOrWhiteSpace(href)) {
                return string.Empty;
            } else if (href.Contains("youtube", StringComparison.InvariantCultureIgnoreCase) || href.Contains("youtu.be", StringComparison.InvariantCultureIgnoreCase)) {
                href = href.Trim('/').Replace("https://www.youtube.com/watch?v=", string.Empty).Replace("http://www.youtube.com/watch?v=", string.Empty).Replace("https://youtu.be/", string.Empty).Replace("https://www.youtube.com/embed/", string.Empty).Replace("http://www.youtube.com/embed/", string.Empty);
                return $"https://www.youtube.com/embed/{href}";
            } else if (href.Contains("mediaspace", StringComparison.InvariantCultureIgnoreCase) && !href.Contains("embed", StringComparison.InvariantCultureIgnoreCase)) {
                href = href.Trim('/').Split('/').Last();
                return $"https://mediaspace.illinois.edu/embed/secure/iframe/entryId/{href}/uiConfId/26883701";
            }
            return href;
        }

        internal void ProcessLists() {
            TagList = TagList == null ? [] : TagList.Select(ProcessTagName).ToList();
            DepartmentList = DepartmentList == null ? [] : DepartmentList.Select(ProcessTagName).ToList();
            SkillList = SkillList == null ? [] : SkillList.Select(ProcessTagName).ToList();
        }

        public static string ProcessTagName(string tag) => tag.Replace("\"", "");
    }
}
