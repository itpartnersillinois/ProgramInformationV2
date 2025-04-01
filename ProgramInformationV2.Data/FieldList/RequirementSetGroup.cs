using ProgramInformationV2.Data.DataModels;

namespace ProgramInformationV2.Data.FieldList {

    public class RequirementSetGroup : BaseGroup {

        public RequirementSetGroup() {
            CategoryType = CategoryType.RequirementSet;
            FieldItems = [
                new() { Title = "Title", CategoryType = CategoryType.RequirementSet, FieldType = FieldType.General, IsRequired = true },
                new() { Title = "Internal Title", CategoryType = CategoryType.RequirementSet, FieldType = FieldType.General },
                new() { Title = "Description", CategoryType = CategoryType.RequirementSet, FieldType = FieldType.General },
                new() { Title = "Minimum Credit Hours", CategoryType = CategoryType.RequirementSet, FieldType = FieldType.General },
                new() { Title = "Maximum Credit Hours", CategoryType = CategoryType.RequirementSet, FieldType = FieldType.General },
                new() { Title = "Is This Shared?", CategoryType = CategoryType.RequirementSet, FieldType = FieldType.Technical },
                new() { Title = "Id", CategoryType = CategoryType.RequirementSet, FieldType = FieldType.Technical },
                new() { Title = "Edit Link", CategoryType = CategoryType.RequirementSet, FieldType = FieldType.Technical }
            ];
        }
    }
}