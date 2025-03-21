using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Data.PageList;

namespace ProgramInformationV2.Components.Layout {
    public partial class Sidebar {
        private string _title { get; set; } = "";
        private string _titleUrl { get; set; } = "";
        private List<PageLink>? _sidebarLinks = default!;
        private readonly string _baseUrl = "";

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        public void Rebuild(SidebarEnum s) {
            var _baseUrl = NavigationManager.ToBaseRelativePath(NavigationManager.Uri);
            if (s != SidebarEnum.None) {
                var rootItem = PageGroup.GetSidebarTitle(s);
                if (rootItem != null) {
                    _titleUrl = rootItem.Url;
                    _title = rootItem.Text;
                }
                _sidebarLinks = PageGroup.GetSidebar(s);
                StateHasChanged();
            }
        }
    }
}
