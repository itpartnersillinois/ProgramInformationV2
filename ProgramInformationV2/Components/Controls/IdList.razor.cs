using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using ProgramInformationV2.Data.FieldList;

namespace ProgramInformationV2.Components.Controls {

    public partial class IdList {

        [Parameter]
        public IEnumerable<FieldItem> FieldItems { get; set; } = default!;

        [Parameter]
        public string Id { get; set; } = "";

        [Parameter]
        public string Title { get; set; } = "";

        [Inject]
        protected IJSRuntime JsRuntime { get; set; } = default!;

        public async Task CopyToClipboard() {
            _ = await JsRuntime.InvokeAsync<bool>("copyToClipboard", Id);
            _ = await JsRuntime.InvokeAsync<bool>("alertOnScreen", Title + " copied to clipboard and can be pasted to your document");
        }

        public bool GetFieldItemActive() => FieldItems == null ? false : FieldItems.FirstOrDefault(f => f.Title == Title)?.ShowItem ?? true;

        public string GetFieldItemDescription() => FieldItems == null ? "" : FieldItems.FirstOrDefault(f => f.Title == Title)?.Description ?? "";
    }
}