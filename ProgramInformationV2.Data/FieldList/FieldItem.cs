using ProgramInformationV2.Data.DataModels;

namespace ProgramInformationV2.Data.FieldList {
    public class FieldItem {
        public string Description { get; set; } = "";
        public CategoryType CategoryType { get; set; }
        public FieldType FieldType { get; set; }
        public FieldInputType FieldInputType { get; set; }
        public bool IsDefault => ShowItem && Description == InitialDescription;
        public bool IsRequired { get; set; }
        public string InitialDescription { get; set; } = "";
        public bool ShowItem { get; set; } = true;
        public string Title { get; set; } = "";

        public string TitleLowerCase => Title.Replace(" ", "").ToLowerInvariant();

        public FieldSource Translate(int sourceId) => new() {
            CategoryType = CategoryType,
            Description = Description,
            IsActive = true,
            ShowItem = ShowItem,
            Title = Title,
            SourceId = sourceId
        };

    }
}
