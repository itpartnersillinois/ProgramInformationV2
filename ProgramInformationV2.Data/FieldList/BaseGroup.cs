using ProgramInformationV2.Data.DataModels;

namespace ProgramInformationV2.Data.FieldList {

    public abstract class BaseGroup {
        public CategoryType CategoryType { get; set; }
        public List<FieldItem> FieldItems { get; set; } = default!;

        public List<IGrouping<FieldType, FieldItem>> FieldsItemsGrouped =>
            [.. FieldItems.GroupBy(f => f.FieldType)];

        public Dictionary<FieldType, string> FieldTypeInstructions { get; set; } = default!;

        public string Instructions { get; set; } = "";
    }
}