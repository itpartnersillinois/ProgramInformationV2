using Blazored.TextEditor;
using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.FieldList;

namespace ProgramInformationV2.Components.Controls {
    public partial class RichTextEditor {
        private BlazoredTextEditor? _quillItem = default!;

        [CascadingParameter]
        public required SidebarLayout Layout { get; set; }

        [Parameter]
        public string Title { get; set; } = "";

        public string InitialValue { get; set; } = "";

        public string Id => Title.Replace(" ", "_").ToLowerInvariant();

        [Parameter]
        public IEnumerable<FieldItem> FieldItems { get; set; } = default!;

        public string GetFieldItemDescription() => FieldItems == null ? "" : FieldItems.FirstOrDefault(f => f.Title == Title)?.Description ?? "";
        public bool GetFieldItemActive() => FieldItems == null ? false : FieldItems.FirstOrDefault(f => f.Title == Title)?.ShowItem ?? true;

        public async Task<string> GetValue() => _quillItem == null ? "" : await _quillItem.GetHTML();

    }
}
