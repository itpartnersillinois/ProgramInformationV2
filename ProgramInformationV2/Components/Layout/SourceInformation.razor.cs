using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ProgramInformationV2.Data.Cache;

namespace ProgramInformationV2.Components.Layout {
    public partial class SourceInformation {
        public string Source { get; set; } = "";
        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject]
        protected CacheHolder CacheHolder { get; set; } = default!;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        protected override async Task OnInitializedAsync() {

            base.OnInitialized();
        }
    }
}
