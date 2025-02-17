using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Data.PageList;

namespace ProgramInformationV2.Components.Layout {

    public partial class Breadcrumb {
        public List<PageLink?>? Breadcrumbs { get; set; }
        public IEnumerable<PageLink> GoodBreadcrumbs => Breadcrumbs?.Where(b => b != null).Select(b => b ?? new("", "")) ?? [];

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        protected override void OnInitialized() {
            Breadcrumbs = new List<PageLink?> { { PageGroup.Get("home") } };
            var url = NavigationManager.ToBaseRelativePath(NavigationManager.Uri).Split('/');
            foreach (var urlitem in url) {
                Breadcrumbs.Add(PageGroup.Get(urlitem));
            }
            base.OnInitialized();
        }
    }
}