using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.DataContext;
using ProgramInformationV2.Data.DataHelpers;
using ProgramInformationV2.Data.PageList;
using ProgramInformationV2.Helpers;

namespace ProgramInformationV2.Components.Pages.Configuration {
    public partial class Sources {
        [CascadingParameter]
        public required SidebarLayout Layout { get; set; }

        [Inject]
        public required ProgramRepository ProgramRepository { get; set; }

        [Inject]
        public required SourceHelper SourceHelper { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        public string NewSource { get; set; } = "";

        public string NewSourceCode { get; set; } = "";

        public Dictionary<string, string> SourceEntries { get; set; } = default!;

        protected override async Task OnInitializedAsync() {
            base.OnInitialized();
            Layout.SetSidebar(SidebarEnum.Configuration);
            SourceEntries = await ProgramRepository.ReadAsync(c => c.Sources.Where(s => s.IsActive).OrderBy(s => s.Title).ToDictionary(s => s.Code, t => $"{t.Title} owned by {t.CreatedByEmail}"));
        }

        protected async Task CreateSource() => await Layout.AddMessage(await SourceHelper.CreateSource(NewSourceCode, NewSource, await UserHelper.GetUser(AuthenticationStateProvider)));

        protected async Task RequestAccess(string key) => await Layout.AddMessage(await SourceHelper.RequestAccess(key, await UserHelper.GetUser(AuthenticationStateProvider)));
    }
}
