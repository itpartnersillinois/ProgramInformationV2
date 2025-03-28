using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.DataHelpers;
using ProgramInformationV2.Helpers;

namespace ProgramInformationV2.Components.Pages.Configuration {

    public partial class RequestDeletion {
        private string _sourceCode = "";

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [CascadingParameter]
        public SidebarLayout Layout { get; set; } = default!;

        [Inject]
        public SourceHelper SourceHelper { get; set; } = default!;

        protected async Task DeleteSource() => await Layout.AddMessage(await SourceHelper.RequestDeletion(_sourceCode, await UserHelper.GetUser(AuthenticationStateProvider)));

        protected override async Task OnInitializedAsync() {
            _sourceCode = await Layout.CheckSource();
            base.OnInitialized();
        }
    }
}