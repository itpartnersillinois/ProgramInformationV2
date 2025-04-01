using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.DataHelpers;
using ProgramInformationV2.Data.DataModels;
using ProgramInformationV2.Data.FieldList;
using ProgramInformationV2.Data.PageList;

namespace ProgramInformationV2.Components.Pages.FieldsUsed {

    public partial class RequirementSets {
        public Dictionary<FieldType, string> FieldGroupInstructions = default!;
        public IEnumerable<IGrouping<FieldType, FieldItem>> FieldItems = default!;

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject]
        public FieldManager FieldManager { get; set; } = default!;

        public string Instructions { get; set; } = "";

        public bool IsUsed { get; set; }

        [CascadingParameter]
        public SidebarLayout Layout { get; set; } = default!;

        [Inject]
        public SourceHelper SourceHelper { get; set; } = default!;

        public async Task SaveChanges() {
            Layout.RemoveDirty();
            await FieldManager.SaveFieldItems(await Layout.CheckSource(), CategoryType.RequirementSet, IsUsed, FieldItems.SelectMany(a => a));
            await Layout.AddMessage("Information saved");
        }

        public void SaveUsedChange(bool isUsed) {
            IsUsed = isUsed;
            Layout.SetDirty();
        }

        protected override async Task OnInitializedAsync() {
            await Layout.SetSidebar(SidebarEnum.FieldsUsed, "Fields Used");
            var sourceCode = await Layout.CheckSource();
            var targetGroup = new RequirementSetGroup();
            Instructions = targetGroup.Instructions;
            FieldGroupInstructions = targetGroup.FieldTypeInstructions;
            (IsUsed, FieldItems) = await FieldManager.MergeFieldItems(targetGroup, sourceCode);
            await base.OnInitializedAsync();
        }
    }
}