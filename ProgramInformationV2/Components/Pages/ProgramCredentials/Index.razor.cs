using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.PageList;

namespace ProgramInformationV2.Components.Pages.ProgramCredentials {

    public partial class Index {

        [CascadingParameter]
        public SidebarLayout Layout { get; set; } = default!;

        protected override async Task OnInitializedAsync() {
            await Layout.SetSidebar(SidebarEnum.ProgramCredential, "Programs and Credentials");
            var sourceCode = await Layout.CheckSource();
            await base.OnInitializedAsync();
        }
    }
}