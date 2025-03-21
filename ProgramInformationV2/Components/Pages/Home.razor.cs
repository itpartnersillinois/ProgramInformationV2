using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using ProgramInformationV2.Data.Cache;
using ProgramInformationV2.Data.DataContext;
using ProgramInformationV2.Helpers;

namespace ProgramInformationV2.Components.Pages {

    public partial class Home {

        [Inject]
        public required ProgramRepository ProgramRepository { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        [Inject]
        protected CacheHolder CacheHolder { get; set; } = default!;

        protected Dictionary<string, string> Sources { get; set; } = default!;

        private string _source = "";

        protected override async Task OnInitializedAsync() {
            var email = await UserHelper.GetUser(AuthenticationStateProvider);
            Sources = await ProgramRepository.ReadAsync(c => c.SecurityEntries.Include(se => se.Source).Where(se => se.IsActive && !se.IsRequested && se.Email == email).ToDictionary(se => se.Source?.Code ?? "", se2 => se2.Source?.Title ?? ""));
            if (Sources.Count == 1) {
                _source = Sources.First().Key;
                CacheHolder.SetCacheSource(email, _source);
            } else if (CacheHolder.HasCachedItem(email)) {
                _source = CacheHolder.GetCacheSource(email) ?? "";
            }
            base.OnInitialized();
        }

        protected async Task ChangeSource(ChangeEventArgs e) {
            _source = e.Value?.ToString() ?? "";
            var email = await UserHelper.GetUser(AuthenticationStateProvider);
            CacheHolder.SetCacheSource(email, _source);
        }
    }
}