using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Data.FieldList;

namespace ProgramInformationV2.Components.Controls {

    public partial class IdList {

        [Parameter]
        public IEnumerable<FieldItem> FieldItems { get; set; } = default!;

        [Parameter]
        public string Id { get; set; } = "";

        [Parameter]
        public string Title { get; set; } = "";

        public bool GetFieldItemActive() => FieldItems == null ? false : FieldItems.FirstOrDefault(f => f.Title == Title)?.ShowItem ?? true;

        public string GetFieldItemDescription() => FieldItems == null ? "" : FieldItems.FirstOrDefault(f => f.Title == Title)?.Description ?? "";
    }
}