using ProgramInformationV2.Data.DataModels;

namespace ProgramInformationV2.Data.FieldList {

    public class RequirementSetGroup : BaseGroup {

        public RequirementSetGroup() {
            CategoryType = CategoryType.RequirementSet;
            Instructions = "Customize the fields used for requirement sets. Note that you need to have credentials enabled to use requirement sets effectively. You can edit custom instructions for each field based on your use case.";
            FieldTypeInstructions = new Dictionary<FieldType, string> {
                [FieldType.General] = "General information about the requirement set like credit hours.",
                [FieldType.Technical] = "Technical details used for internal purposes."
            };

            FieldItems = [
                new() { Title = "Title", CategoryType = CategoryType.RequirementSet, FieldType = FieldType.General, IsRequired = true, InitialDescription = "Name your requirement set. This will appear on credential pages." },
                new() { Title = "Internal Title", CategoryType = CategoryType.RequirementSet, FieldType = FieldType.General, InitialDescription = "Create an internal name for the requirement set. This will not display anywhere, it is for internal use only." },
                new() { Title = "Description", CategoryType = CategoryType.RequirementSet, FieldType = FieldType.General, InitialDescription = "This text should describe the requirement set. It will be on the credential page." },
                new() { Title = "Minimum Credit Hours", CategoryType = CategoryType.RequirementSet, FieldType = FieldType.General },
                new() { Title = "Maximum Credit Hours", CategoryType = CategoryType.RequirementSet, FieldType = FieldType.General },
                new() { Title = "Is This Shared?", CategoryType = CategoryType.RequirementSet, FieldType = FieldType.Technical, InitialDescription = "Share this requirement set to be used by other credentials." },
                new() { Title = "Id", CategoryType = CategoryType.RequirementSet, FieldType = FieldType.Technical, InitialDescription = "The ID of the item, which may be used in a CMS to pull the item and display it on a webpage." },
                new() { Title = "Edit Link", CategoryType = CategoryType.RequirementSet, FieldType = FieldType.Technical, InitialDescription = "This is a quick link to edit this item directly." }
            ];
        }
    }
}