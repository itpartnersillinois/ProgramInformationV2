using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using ProgramInformationV2.Data.Cache;
using ProgramInformationV2.Data.DataHelpers;
using ProgramInformationV2.Data.DataModels;
using ProgramInformationV2.Helpers;

namespace ProgramInformationV2.Components.Pages {

    public partial class Home {
        private string _source = "";

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        public bool UseCourses { get; set; }

        public bool UseCredentials { get; set; }

        public bool UsePrograms { get; set; }

        public bool UseRequirementSets { get; set; }

        [Inject]
        protected CacheHolder CacheHolder { get; set; } = default!;

        [Inject]
        protected SourceHelper SourceHelper { get; set; } = default!;

        protected Dictionary<string, string> Sources { get; set; } = default!;

        protected async Task ChangeSource(ChangeEventArgs e) {
            _source = e.Value?.ToString() ?? "";
            var email = await UserHelper.GetUser(AuthenticationStateProvider);
            CacheHolder.SetCacheSource(email, _source);
            await ChangeBoxes();
        }

        protected override async Task OnInitializedAsync() {
            var email = await UserHelper.GetUser(AuthenticationStateProvider);
            Sources = await SourceHelper.GetSources(email);
            if (Sources.Count == 1) {
                _source = Sources.First().Key;
                CacheHolder.SetCacheSource(email, _source);
            } else if (CacheHolder.HasCachedItem(email)) {
                _source = CacheHolder.GetCacheSource(email) ?? "";
            }
            await ChangeBoxes();
            base.OnInitialized();
        }

        private async Task ChangeBoxes() {
            if (!string.IsNullOrWhiteSpace(_source)) {
                UseCredentials = await SourceHelper.DoesSourceUseItem(_source, CategoryType.Credential);
                UseCourses = await SourceHelper.DoesSourceUseItem(_source, CategoryType.Course);
                UseRequirementSets = await SourceHelper.DoesSourceUseItem(_source, CategoryType.RequirementSet);
                UsePrograms = await SourceHelper.DoesSourceUseItem(_source, CategoryType.Program);
            }
        }
    }
}