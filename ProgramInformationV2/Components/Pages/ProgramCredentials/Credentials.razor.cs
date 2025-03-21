using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.PageList;

namespace ProgramInformationV2.Components.Pages.ProgramCredentials {
    public partial class Credentials {
        [CascadingParameter]
        public required SidebarLayout Layout { get; set; }

        protected override async Task OnInitializedAsync() {
            Layout.SetSidebar(SidebarEnum.ProgramCredential);
            var sourceCode = await Layout.CheckSource();
            await base.OnInitializedAsync();
        }

    }
}
