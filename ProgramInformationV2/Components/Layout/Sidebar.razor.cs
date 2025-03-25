using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Data.PageList;

namespace ProgramInformationV2.Components.Layout {

    public partial class Sidebar {

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        private string _baseUrl { get; set; } = "";
        private List<PageLink>? _sidebarLinks { get; set; } = default!;
        private string _title { get; set; } = "";

        public void Rebuild(SidebarEnum s, string title) {
            _baseUrl = "/" + NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
            if (s != SidebarEnum.None) {
                _title = title;
                _sidebarLinks = PageGroup.GetSidebar(s);
                StateHasChanged();
            }
        }
    }
}