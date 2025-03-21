using ProgramInformationV2.Data.DataModels;

namespace ProgramInformationV2.Data.FieldList {
    public class ProgramGroup : BaseGroup {
        public ProgramGroup() {
            CategoryType = CategoryType.Program;
            FieldItems = [
                new() { Title = "Title", CategoryType = CategoryType.Program, FieldInputType = FieldInputType.ShortText, FieldType = FieldType.General, IsRequired = true },
                new() { Title = "Summary Text", CategoryType = CategoryType.Program, FieldInputType = FieldInputType.LongText, FieldType = FieldType.General },
                new() { Title = "Link URL", CategoryType = CategoryType.Program, FieldInputType = FieldInputType.Url, FieldType = FieldType.Link },
                new() { Title = "Alternate Link URL", CategoryType = CategoryType.Program, FieldInputType = FieldInputType.Url, FieldType = FieldType.Link },
                new() { Title = "Alternate Link Text", CategoryType = CategoryType.Program, FieldInputType = FieldInputType.ShortText, FieldType = FieldType.Link },
                new() { Title = "Program Image", CategoryType = CategoryType.Program, FieldInputType = FieldInputType.Image, FieldType = FieldType.Link },
                new() { Title = "Detail Image", CategoryType = CategoryType.Program, FieldInputType = FieldInputType.Image, FieldType = FieldType.Link },
                new() { Title = "Detail Image Alternative Text", CategoryType = CategoryType.Program, FieldInputType = FieldInputType.ShortText, FieldType = FieldType.Link },
                new() { Title = "Video URL", CategoryType = CategoryType.Program, FieldInputType = FieldInputType.Url, FieldType = FieldType.Link },
                new() { Title = "Description", CategoryType = CategoryType.Program, FieldInputType = FieldInputType.RichText, FieldType = FieldType.Overview },
                new() { Title = "Who Should Apply", CategoryType = CategoryType.Program, FieldInputType = FieldInputType.RichText, FieldType = FieldType.Overview }
            ];
        }
    }
}
