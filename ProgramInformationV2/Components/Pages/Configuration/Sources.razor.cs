using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.DataContext;
using ProgramInformationV2.Data.DataHelpers;
using ProgramInformationV2.Data.DataModels;
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
            SourceEntries = await ProgramRepository.ReadAsync(c => c.Sources.Where(s => s.IsActive).OrderBy(s => s.Title).ToDictionary(s => s.Code, t => $"{t.Title} owned by {t.CreatedByEmail}"));
        }

        protected async Task CreateSource() {
            var email = await UserHelper.GetUser(AuthenticationStateProvider);
            var message = await SourceHelper.CreateSource(NewSourceCode, NewSource, email);
            await Layout.AddMessage(message);
        }

        protected async Task<int> RequestAccess(string key) {
            var source = await ProgramRepository.ReadAsync(c => c.Sources.FirstOrDefault(s => s.Code == key));
            if (source == null) {
                await Layout.AddMessage($"Source Code not found");
                return 0;
            }

            var email = await UserHelper.GetUser(AuthenticationStateProvider);
            var existingItem = await ProgramRepository.ReadAsync(c => c.SecurityEntries.FirstOrDefault(s => s.SourceId == source.Id && s.Email == email));
            if (existingItem != null) {
                if (existingItem.IsActive) {
                    await Layout.AddMessage($"You already have access");
                } else if (existingItem.IsRequested) {
                    await Layout.AddMessage($"You entry is pending");
                }
                return 0;
            }

            var value = await ProgramRepository.CreateAsync(new SecurityEntry(email, source.Id, true));
            await Layout.AddMessage($"Requested access to code {key}");
            return value;
        }
    }
}
