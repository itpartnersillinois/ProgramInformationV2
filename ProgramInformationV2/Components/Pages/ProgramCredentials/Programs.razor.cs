using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Components.Controls;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.DataHelpers;
using ProgramInformationV2.Data.DataModels;
using ProgramInformationV2.Data.PageList;
using ProgramInformationV2.Search.Getters;
using ProgramInformationV2.Search.Models;

namespace ProgramInformationV2.Components.Pages.ProgramCredentials {

    public partial class Programs {
        private SearchGenericItem _searchGenericItem = default!;

        private string _sourceCode = "";
        private bool? _usePrograms;

        [CascadingParameter]
        public SidebarLayout Layout { get; set; } = default!;

        public List<GenericItem> ProgramList { get; set; } = default!;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        protected ProgramGetter ProgramGetter { get; set; } = default!;

        [Inject]
        protected SourceHelper SourceHelper { get; set; } = default!;

        protected async Task ChooseProgram() {
            if (!string.IsNullOrWhiteSpace(_searchGenericItem.SelectedItemId)) {
                await Layout.SetCacheId(_searchGenericItem.SelectedItemId);
                NavigationManager.NavigateTo("/program/general");
            }
        }

        protected async Task GetPrograms() {
            ProgramList = await ProgramGetter.GetAllProgramsBySource(_sourceCode, _searchGenericItem == null ? "" : _searchGenericItem.SearchItem);
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync() {
            Layout.SetSidebar(SidebarEnum.ProgramCredential, "Programs and Credentials");
            _sourceCode = await Layout.CheckSource();
            _usePrograms = await SourceHelper.DoesSourceUseItem(_sourceCode, CategoryType.Program);
            await GetPrograms();
            await base.OnInitializedAsync();
        }

        protected async Task SetNewProgram() {
            await Layout.ClearCacheId();
            NavigationManager.NavigateTo("/program/general");
        }
    }
}