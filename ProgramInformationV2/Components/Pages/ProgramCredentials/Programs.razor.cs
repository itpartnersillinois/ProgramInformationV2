using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.PageList;
using ProgramInformationV2.Search.Getters;
using ProgramInformationV2.Search.Models;

namespace ProgramInformationV2.Components.Pages.ProgramCredentials {
    public partial class Programs {
        [CascadingParameter]
        public required SidebarLayout Layout { get; set; }

        public string SelectedId { get; set; } = "";

        public List<GenericItem> ProgramList { get; set; } = default!;
        [Inject]
        protected ProgramGetter ProgramGetter { get; set; } = default!;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        protected override async Task OnInitializedAsync() {
            Layout.SetSidebar(SidebarEnum.ProgramCredential);
            var sourceCode = await Layout.CheckSource();
            ProgramList = await ProgramGetter.GetAllProgramsBySource(sourceCode);
            await base.OnInitializedAsync();
        }

        protected async Task ChooseProgram() {
            if (!string.IsNullOrWhiteSpace(SelectedId)) {
                await Layout.SetCacheId(SelectedId);
                NavigationManager.NavigateTo("/program/general");
            }
        }

        protected async Task SetNewProgram() {
            await Layout.ClearCacheId();
            NavigationManager.NavigateTo("/program/general");
        }
    }
}
