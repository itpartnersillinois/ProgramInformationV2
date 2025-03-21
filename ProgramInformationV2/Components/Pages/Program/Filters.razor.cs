using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.DataHelpers;
using ProgramInformationV2.Data.DataModels;
using ProgramInformationV2.Search.Getters;
using ProgramInformationV2.Search.Setters;

namespace ProgramInformationV2.Components.Pages.Program {
    public partial class Filters {
        [CascadingParameter]
        public required SidebarLayout Layout { get; set; }

        [Inject]
        protected FilterHelper FilterHelper { get; set; } = default!;
        [Inject]
        protected ProgramSetter ProgramSetter { get; set; } = default!;

        [Inject]
        protected ProgramGetter ProgramGetter { get; set; } = default!;

        public IEnumerable<IGrouping<TagType, TagSource>> FilterTags { get; set; } = [];

        public IEnumerable<TagSource>? DepartmentTags => FilterTags?.Where(f => f.Key == TagType.Department).SelectMany(x => x);

        public IEnumerable<TagSource>? Tags => FilterTags?.Where(f => f.Key == TagType.Tag).SelectMany(x => x);

        public IEnumerable<TagSource>? SkillTags => FilterTags?.Where(f => f.Key == TagType.Skill).SelectMany(x => x);

        public Search.Models.Program ProgramItem { get; set; } = new Search.Models.Program();


        protected override async Task OnInitializedAsync() {
            var sourceCode = await Layout.CheckSource();
            FilterTags = await FilterHelper.GetAllFilters(sourceCode);
            var id = await Layout.GetCachedId();
            ProgramItem = await ProgramGetter.GetProgram(id);
            foreach (var tag in FilterTags.SelectMany(x => x)) {
                if (ProgramItem.DepartmentList.Contains(tag.Title) && tag.TagType == TagType.Department) {
                    tag.EnabledBySource = true;
                }
                if (ProgramItem.TagList.Contains(tag.Title) && tag.TagType == TagType.Tag) {
                    tag.EnabledBySource = true;
                }
                if (ProgramItem.SkillList.Contains(tag.Title) && tag.TagType == TagType.Skill) {
                    tag.EnabledBySource = true;
                }
            }
        }

        public async Task Save() {
            ProgramItem.DepartmentList = DepartmentTags?.Where(t => t.EnabledBySource).Select(t => t.Title).ToList() ?? new List<string>();
            ProgramItem.SkillList = SkillTags?.Where(t => t.EnabledBySource).Select(t => t.Title).ToList() ?? new List<string>();
            ProgramItem.TagList = Tags?.Where(t => t.EnabledBySource).Select(t => t.Title).ToList() ?? new List<string>();
            Layout.RemoveDirty();
            _ = await ProgramSetter.SetProgram(ProgramItem);
        }
    }
}
