using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.DataContext;
using ProgramInformationV2.Data.DataHelpers;
using ProgramInformationV2.Data.PageList;
using ProgramInformationV2.Helpers;

namespace ProgramInformationV2.Components.Pages.Configuration {

    public partial class Sources {

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [CascadingParameter]
        public SidebarLayout Layout { get; set; } = default!;

        public string NewSource { get; set; } = "";

        public string NewSourceCode { get; set; } = "";

        [Inject]
        public ProgramRepository ProgramRepository { get; set; } = default!;

        public Dictionary<string, string> SourceEntries { get; set; } = default!;

        [Inject]
        public SourceHelper SourceHelper { get; set; } = default!;

        protected async Task CreateSource() => await Layout.AddMessage(await SourceHelper.CreateSource(NewSourceCode, NewSource, await UserHelper.GetUser(AuthenticationStateProvider)));

        protected override async Task OnInitializedAsync() {
            base.OnInitialized();
            await Layout.SetSidebar(SidebarEnum.Configuration, "Configuration");
            SourceEntries = await ProgramRepository.ReadAsync(c => c.Sources.Where(s => s.IsActive).OrderBy(s => s.Title).ToDictionary(s => s.Code, t => $"{t.Title} owned by {t.CreatedByEmail}"));
        }

        protected async Task RequestAccess(string key) => await Layout.AddMessage(await SourceHelper.RequestAccess(key, await UserHelper.GetUser(AuthenticationStateProvider)));
    }
}