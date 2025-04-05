using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Components.Controls;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.DataHelpers;
using ProgramInformationV2.Data.DataModels;
using ProgramInformationV2.Data.FieldList;
using ProgramInformationV2.Data.PageList;
using ProgramInformationV2.Search.Getters;
using ProgramInformationV2.Search.Setters;

namespace ProgramInformationV2.Components.Pages.Course {

    public partial class Overview {
        private RichTextEditor _rteDescription = default!;

        private RichTextEditor _rteWhoShouldApply = default!;

        public ProgramInformationV2.Search.Models.Course CourseItem { get; set; } = default!;

        [CascadingParameter]
        public SidebarLayout Layout { get; set; } = default!;

        public string QuickLinkUrl { get; set; } = string.Empty;

        [Inject]
        protected CourseGetter CourseGetter { get; set; } = default!;

        [Inject]
        protected CourseSetter CourseSetter { get; set; } = default!;

        [Inject]
        protected FieldManager FieldManager { get; set; } = default!;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        protected SourceHelper SourceHelper { get; set; } = default!;

        private IEnumerable<FieldItem> _fieldItems { get; set; } = default!;

        public async Task Save() {
            Layout.RemoveDirty();
            if (_rteDescription != null) {
                CourseItem.Details = await _rteDescription.GetValue();
            }
            if (_rteWhoShouldApply != null) {
                CourseItem.ExternalDetails = await _rteWhoShouldApply.GetValue();
            }
            _ = await CourseSetter.SetCourse(CourseItem);
            await Layout.Log(CategoryType.Course, FieldType.Overview, CourseItem);
            await Layout.AddMessage("Course saved successfully.");
        }

        protected override async Task OnInitializedAsync() {
            var sourceCode = await Layout.CheckSource();
            var id = await Layout.GetCachedId();
            if (string.IsNullOrWhiteSpace(id)) {
                NavigationManager.NavigateTo("/");
            }
            StateHasChanged();
            CourseItem = await CourseGetter.GetCourse(id);
            _rteDescription.InitialValue = CourseItem.Details;
            _rteWhoShouldApply.InitialValue = CourseItem.ExternalDetails;
            _fieldItems = await FieldManager.GetMergedFieldItems(sourceCode, new CourseGroup(), FieldType.Overview);
            var sidebar = await SourceHelper.DoesSourceUseItem(sourceCode, CategoryType.Section) ? SidebarEnum.CourseWithSection : SidebarEnum.Course;
            Layout.SetSidebar(sidebar, CourseItem.Title);
            QuickLinkUrl = await Layout.GetCachedQuickLink();
            await base.OnInitializedAsync();
        }
    }
}