using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Data.PageList;

namespace ProgramInformationV2.Components.Layout {

    public partial class Sidebar {

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        private string _baseUrl { get; set; } = "";
        private SidebarEnum _sidebar { get; set; } = default!;
        private List<PageLink>? _sidebarLinks { get; set; } = default!;
        private string _title { get; set; } = "";

        public void Rebuild(SidebarEnum s, string title, string quickLinkText, string quickLinkUrl) {
            _baseUrl = "/" + NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
            _title = title;
            _sidebar = s;
            _sidebarLinks = PageGroup.GetSidebar(s);
            if (_sidebarLinks != null && !string.IsNullOrWhiteSpace(quickLinkText)) {
                _sidebarLinks.Add(new PageLink(quickLinkText, quickLinkUrl) { IsEmphasized = true });
            }
            StateHasChanged();
        }
    }
}