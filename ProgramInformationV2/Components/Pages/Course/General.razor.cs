using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.DataHelpers;
using ProgramInformationV2.Data.DataModels;
using ProgramInformationV2.Data.FieldList;
using ProgramInformationV2.Data.PageList;
using ProgramInformationV2.Search.Getters;
using ProgramInformationV2.Search.Helpers;
using ProgramInformationV2.Search.Setters;
using FieldType = ProgramInformationV2.Data.DataModels.FieldType;

namespace ProgramInformationV2.Components.Pages.Course {

    public partial class General {
        private string _oldTitle = "";
        private SidebarEnum _sidebar = SidebarEnum.None;

        [Inject]
        public BulkEditor BulkEditor { get; set; } = default!;

        public ProgramInformationV2.Search.Models.Course CourseItem { get; set; } = default!;
        public IEnumerable<FieldItem> FieldItems { get; set; } = default!;

        [CascadingParameter]
        public SidebarLayout Layout { get; set; } = default!;

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

        public async Task Save() {
            Layout.RemoveDirty();
            _ = await CourseSetter.SetCourse(CourseItem);
            await Layout.SetCacheId(CourseItem.Id);
            await Layout.SetSidebar(_sidebar, CourseItem.Title);
            await Layout.Log(CategoryType.Course, FieldType.General, CourseItem);
            await Layout.AddMessage("Course saved successfully.");
            if (!string.IsNullOrWhiteSpace(_oldTitle) && (_oldTitle != CourseItem.Title)) {
                _ = await BulkEditor.UpdateRequirementSets(CourseItem.Id, CourseItem.Title, CourseItem.Url);
                _oldTitle = CourseItem.Title;
            }
        }

        protected override async Task OnInitializedAsync() {
            var sourceCode = await Layout.CheckSource();
            var id = await Layout.GetCachedId();
            _sidebar = await SourceHelper.DoesSourceUseItem(sourceCode, CategoryType.Section) ? SidebarEnum.CourseWithSection : SidebarEnum.Course;

            if (!string.IsNullOrWhiteSpace(id)) {
                CourseItem = await CourseGetter.GetCourse(id);
                _oldTitle = CourseItem.Title;
                await Layout.SetSidebar(_sidebar, CourseItem.Title);
            } else {
                CourseItem = new ProgramInformationV2.Search.Models.Course {
                    Source = sourceCode
                };
                await Layout.SetSidebar(SidebarEnum.None, "New Course", true);
            }
            FieldItems = await FieldManager.GetMergedFieldItems(sourceCode, new CourseGroup(), FieldType.General);
            await base.OnInitializedAsync();
        }
    }
}