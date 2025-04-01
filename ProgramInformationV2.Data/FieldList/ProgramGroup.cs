using ProgramInformationV2.Data.DataModels;

namespace ProgramInformationV2.Data.FieldList {

    public class ProgramGroup : BaseGroup {

        public ProgramGroup() {
            CategoryType = CategoryType.Program;
            FieldItems = [
                new() { Title = "Title", CategoryType = CategoryType.Program, FieldType = FieldType.General, IsRequired = true },
                new() { Title = "Summary Text", CategoryType = CategoryType.Program, FieldType = FieldType.General },
                new() { Title = "Link URL", CategoryType = CategoryType.Program, FieldType = FieldType.Link },
                new() { Title = "Alternate Link URL", CategoryType = CategoryType.Program, FieldType = FieldType.Link },
                new() { Title = "Alternate Link Text", CategoryType = CategoryType.Program, FieldType = FieldType.Link },
                new() { Title = "Program Image", CategoryType = CategoryType.Program, FieldType = FieldType.Link },
                new() { Title = "Detail Image", CategoryType = CategoryType.Program, FieldType = FieldType.Link },
                new() { Title = "Detail Image Alternative Text", CategoryType = CategoryType.Program, FieldType = FieldType.Link },
                new() { Title = "Video URL", CategoryType = CategoryType.Program, FieldType = FieldType.Link },
                new() { Title = "Description", CategoryType = CategoryType.Program, FieldType = FieldType.Overview },
                new() { Title = "Who Should Apply", CategoryType = CategoryType.Program,FieldType = FieldType.Overview },
                new() { Title = "URL Fragment", CategoryType = CategoryType.Program, InitialDescription = "Note that the URL fragment is used to make searching for this item easier and to meet SEO standards. This needs to be unique and consist of lower-case letters, numbers, and dashes. Do not use this if you cannot meet these requirements and rely on the ID to be a unique identifier.", FieldType = FieldType.Technical },
                new() { Title = "Id", CategoryType = CategoryType.Program, FieldType = FieldType.Technical },
                new() { Title = "Edit Link", CategoryType = CategoryType.Program, FieldType = FieldType.Technical }
            ];
        }
    }
}