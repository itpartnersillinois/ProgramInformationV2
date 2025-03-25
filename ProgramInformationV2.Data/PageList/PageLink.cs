namespace ProgramInformationV2.Data.PageList {

    public class PageLink(string text, string url) {
        public string Text { get; set; } = text;
        public string Url { get; set; } = url;

        public bool IsCurrent = false;
    }
}