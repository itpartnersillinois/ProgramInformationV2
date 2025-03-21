using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.DataHelpers;
using ProgramInformationV2.Data.DataModels;
using ProgramInformationV2.Data.FieldList;
using ProgramInformationV2.Data.PageList;

namespace ProgramInformationV2.Components.Pages.FieldsUsed {
    public partial class Programs {
        [CascadingParameter]
        public required SidebarLayout Layout { get; set; }

        [Inject]
        public required FieldManager FieldManager { get; set; }

        [Inject]
        public required SourceHelper SourceHelper { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        public bool IsUsed { get; set; }
        public IEnumerable<IGrouping<FieldType, FieldItem>> FieldItems = default!;
        public string Instructions { get; set; } = "";

        protected override async Task OnInitializedAsync() {
            Layout.SetSidebar(SidebarEnum.FieldsUsed);
            var sourceCode = await Layout.CheckSource();
            var targetGroup = new ProgramGroup();
            Instructions = targetGroup.Instructions;
            (IsUsed, FieldItems) = await FieldManager.MergeFieldItems(targetGroup, sourceCode);
            await base.OnInitializedAsync();
        }

        public void SaveUsedChange(bool isUsed) {
            IsUsed = isUsed;
            Layout.SetDirty();
        }

        public async Task SaveChanges() {
            Layout.RemoveDirty();
            await FieldManager.SaveFieldItems(await Layout.CheckSource(), CategoryType.Program, IsUsed, FieldItems.SelectMany(a => a));
            await Layout.AddMessage("Information saved");

        }

    }
}
