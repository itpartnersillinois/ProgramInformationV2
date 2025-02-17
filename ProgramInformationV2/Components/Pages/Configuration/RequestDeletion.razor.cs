using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.DataContext;

namespace ProgramInformationV2.Components.Pages.Configuration {
    public partial class RequestDeletion {
        [CascadingParameter]
        public required SidebarLayout Layout { get; set; }

        [Inject]
        public required ProgramRepository ProgramRepository { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        protected override async Task OnInitializedAsync() {
            await Layout.CheckSource();
            base.OnInitialized();
        }

        protected async Task<int> DeleteSource() {
            return 0;
        }
    }
}
