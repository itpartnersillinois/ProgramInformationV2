using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Data.PageList;

namespace ProgramInformationV2.Components.Layout {
    public partial class Sidebar {
        public string Title { get; set; } = "";
        public string TitleUrl { get; set; } = "";
        public string BaseUrl { get; set; } = "";
        public List<PageLink>? SidebarLinks { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        protected override void OnInitialized() {
            var url = NavigationManager.ToBaseRelativePath(NavigationManager.Uri).Split('/').Reverse();
            foreach (var urlItem in url) {
                if (string.IsNullOrEmpty(Title)) {
                    var rootItem = PageGroup.GetSidebarTitle(urlItem);
                    if (rootItem != null) {
                        TitleUrl = rootItem.Url;
                        Title = rootItem.Text;
                    }
                    SidebarLinks = PageGroup.GetSidebar(urlItem);
                }
            }
            base.OnInitialized();
        }
    }
}
