using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Components.Controls;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.DataModels;
using ProgramInformationV2.Data.FieldList;
using ProgramInformationV2.Data.PageList;
using ProgramInformationV2.Search.Getters;
using ProgramInformationV2.Search.Setters;

namespace ProgramInformationV2.Components.Pages.Program {
    public partial class Overview {
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

        private IEnumerable<FieldItem> _fieldItems = default!;


        private string _id = "";

        private RichTextEditor _rteDescription = default!;

        private RichTextEditor _rteWhoShouldApply = default!;

        public Search.Models.Program ProgramItem { get; set; } = new Search.Models.Program();

        protected override async Task OnInitializedAsync() {
            Layout.SetSidebar(SidebarEnum.Program);
            if (string.IsNullOrWhiteSpace(_id)) {
                var sourceCode = await Layout.CheckSource();
                _id = await Layout.GetCachedId();
                if (!string.IsNullOrWhiteSpace(_id)) {
                    ProgramItem = await ProgramGetter.GetProgram(_id);
                    _rteDescription.InitialValue = ProgramItem.Description;
                    _rteWhoShouldApply.InitialValue = ProgramItem.WhoShouldApply;
                } else {
                    ProgramItem.Source = sourceCode;
                }
                _fieldItems = await FieldManager.GetMergedFieldItems(sourceCode, new ProgramGroup(), FieldType.Overview);
            }
            await base.OnInitializedAsync();
        }

        public async Task Save() {
            Layout.RemoveDirty();

            if (_rteDescription != null) {
                ProgramItem.Description = await _rteDescription.GetValue();
            }
            if (_rteWhoShouldApply != null) {
                ProgramItem.WhoShouldApply = await _rteWhoShouldApply.GetValue();
            }
            _ = await ProgramSetter.SetProgram(ProgramItem);
            if (string.IsNullOrEmpty(_id)) {
                _id = ProgramItem.Id;
                await Layout.SetCacheId(_id);
            }
            await Layout.AddMessage("Program saved successfully.");
        }


    }
}
