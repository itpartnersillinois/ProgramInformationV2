using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.FieldList;
using ProgramInformationV2.Data.PageList;
using ProgramInformationV2.Search.Getters;
using ProgramInformationV2.Search.Setters;
using FieldType = ProgramInformationV2.Data.DataModels.FieldType;

namespace ProgramInformationV2.Components.Pages.Program {

    public partial class General {
        private string _id = "";

        public IEnumerable<FieldItem> FieldItems { get; set; } = default!;

        [CascadingParameter]
        public SidebarLayout Layout { get; set; } = default!;

        public Search.Models.Program ProgramItem { get; set; } = new Search.Models.Program();

        [Inject]
        protected FieldManager FieldManager { get; set; } = default!;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        protected ProgramGetter ProgramGetter { get; set; } = default!;

        [Inject]
        protected ProgramSetter ProgramSetter { get; set; } = default!;

        public async Task Save() {
            Layout.RemoveDirty();
            _ = await ProgramSetter.SetProgram(ProgramItem);
            if (string.IsNullOrEmpty(_id)) {
                _id = ProgramItem.Id;
                await Layout.SetCacheId(_id);
            }
        }

        protected override async Task OnInitializedAsync() {
            var title = "New Program";
            if (string.IsNullOrWhiteSpace(_id)) {
                var sourceCode = await Layout.CheckSource();
                _id = await Layout.GetCachedId();
                if (!string.IsNullOrWhiteSpace(_id)) {
                    ProgramItem = await ProgramGetter.GetProgram(_id);
                    title = ProgramItem.Title;
                } else {
                    ProgramItem.Source = sourceCode;
                }
                FieldItems = await FieldManager.GetMergedFieldItems(sourceCode, new ProgramGroup(), FieldType.General);
            }
            Layout.SetSidebar(SidebarEnum.Program, title);
            await base.OnInitializedAsync();
        }
    }
}