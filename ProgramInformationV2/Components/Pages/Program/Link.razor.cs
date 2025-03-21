using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Components.Controls;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.DataModels;
using ProgramInformationV2.Data.FieldList;
using ProgramInformationV2.Data.PageList;
using ProgramInformationV2.Search.Getters;
using ProgramInformationV2.Search.Setters;

namespace ProgramInformationV2.Components.Pages.Program {
    public partial class Link {
        [CascadingParameter]
        public required SidebarLayout Layout { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        protected ProgramSetter ProgramSetter { get; set; } = default!;

        [Inject]
        protected ProgramGetter ProgramGetter { get; set; } = default!;

        [Inject]
        protected FieldManager FieldManager { get; set; } = default!;

        public IEnumerable<FieldItem> FieldItems { get; set; } = default!;

        private ImageControl _imageDetailProgramImage = default!;
        private ImageControl _imageProgramImage = default!;

        private string _id = "";

        public Search.Models.Program ProgramItem { get; set; } = new Search.Models.Program();

        protected override async Task OnInitializedAsync() {
            Layout.SetSidebar(SidebarEnum.Program);
            if (string.IsNullOrWhiteSpace(_id)) {
                var sourceCode = await Layout.CheckSource();
                _id = await Layout.GetCachedId();
                if (!string.IsNullOrWhiteSpace(_id)) {
                    ProgramItem = await ProgramGetter.GetProgram(_id);
                } else {
                    ProgramItem.Source = sourceCode;
                }
                FieldItems = await FieldManager.GetMergedFieldItems(sourceCode, new ProgramGroup(), FieldType.Link);
            }
            await base.OnInitializedAsync();
        }

        public async Task Save() {
            Layout.RemoveDirty();
            if (_imageProgramImage != null) {
                _ = await _imageProgramImage.SaveFileToPermanent();
            }
            if (_imageDetailProgramImage != null) {
                _ = await _imageDetailProgramImage.SaveFileToPermanent();
            }
            _ = await ProgramSetter.SetProgram(ProgramItem);
            if (string.IsNullOrEmpty(_id)) {
                _id = ProgramItem.Id;
                await Layout.SetCacheId(_id);
            }
        }

    }
}
