using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.Cache;
using ProgramInformationV2.Data.PageList;
using ProgramInformationV2.Helpers;

namespace ProgramInformationV2.Components.Pages.Configuration {

    public partial class Testing {

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [CascadingParameter]
        public SidebarLayout Layout { get; set; } = default!;

        [Inject]
        protected CacheHolder CacheHolder { get; set; } = default!;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        public async Task SwitchToTesting() {
            var email = await UserHelper.GetUser(AuthenticationStateProvider);
            CacheHolder.SetCacheSource(email, "test");
            NavigationManager.NavigateTo("/", true);
        }

        protected override async Task OnInitializedAsync() {
            await base.OnInitializedAsync();
            await Layout.SetSidebar(SidebarEnum.Configuration, "Configuration");
        }
    }
}