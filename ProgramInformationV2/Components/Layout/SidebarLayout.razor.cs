using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using ProgramInformationV2.Data.Cache;
using ProgramInformationV2.Data.PageList;
using ProgramInformationV2.Helpers;

namespace ProgramInformationV2.Components.Layout {

    public partial class SidebarLayout {
        public bool IsDirty = false;

        public required Breadcrumb BreadcrumbControl { get; set; }
        public required Sidebar SidebarControl { get; set; }

        [Inject]
        protected AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject]
        protected CacheHolder CacheHolder { get; set; } = default!;

        [Inject]
        protected IJSRuntime JsRuntime { get; set; } = default!;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        public string SourceCode { get; set; } = "";

        public async Task AddMessage(string s) => _ = await JsRuntime.InvokeAsync<bool>("alertOnScreen", s);

        public async Task<string> CheckSource() {
            var source = CacheHolder.GetCacheSource(await AuthenticationStateProvider.GetUser());
            if (string.IsNullOrWhiteSpace(source)) {
                NavigationManager.NavigateTo("/");
            }
            return source ?? "";
        }

        public async Task<string> GetCachedId() {
            var cacheItem = CacheHolder.GetItem(await AuthenticationStateProvider.GetUser());
            return cacheItem?.ItemId ?? "";
        }

        public async Task<string> GetNetId() => await AuthenticationStateProvider.GetUser();

        public async Task ClearCacheId() => await SetCacheId("");

        public async Task SetCacheId(string id) {
            var netid = await AuthenticationStateProvider.GetUser();
            var cacheItem = CacheHolder.GetItem(await AuthenticationStateProvider.GetUser());
            CacheHolder.SetCacheItem(netid, id, "");
        }

        protected override async Task OnInitializedAsync() {
            SourceCode = CacheHolder.GetCacheSource(await AuthenticationStateProvider.GetUser()) ?? "";
            await base.OnInitializedAsync();
        }

        public void SetSidebar(SidebarEnum s) {
            SidebarControl.Rebuild(s);
            BreadcrumbControl.Rebuild(s);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender) {
            if (firstRender) {
                _ = await JsRuntime.InvokeAsync<bool>("blazorMenu");
            }
        }

        public void RemoveDirty() => IsDirty = false;

        public async Task RemoveMessage() => _ = await JsRuntime.InvokeAsync<bool>("removeAlertOnScreen");

        public void SetDirty() => IsDirty = true;

        private async Task LocationChangingHandler(LocationChangingContext arg) {
            if (IsDirty) {
                if (!(await JsRuntime.InvokeAsync<bool>("confirm", $"You have unsaved changes. Are you sure?"))) {
                    arg.PreventNavigation();
                }
            }
        }
    }
}