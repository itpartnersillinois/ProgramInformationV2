using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.FieldList;

namespace ProgramInformationV2.Components.Controls {
    public partial class LargeText {
        private string? _value;

        [CascadingParameter]
        public required SidebarLayout Layout { get; set; }

        [Parameter]
        public string Title { get; set; } = "";

        [Parameter]
        public string? Value {
            get => _value;
            set {
                if (_value == value)
                    return;

                _value = value;
                ValueChanged.InvokeAsync(value);
            }
        }

        public string Id => Title.Replace(" ", "_").ToLowerInvariant();

        [Parameter]
        public IEnumerable<FieldItem> FieldItems { get; set; } = default!;

        public string GetFieldItemDescription() => FieldItems == null ? "" : FieldItems.FirstOrDefault(f => f.Title == Title)?.Description ?? "";
        public bool GetFieldItemActive() => FieldItems == null ? false : FieldItems.FirstOrDefault(f => f.Title == Title)?.ShowItem ?? true;

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }
    }
}
