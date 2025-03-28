using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.FieldList;

namespace ProgramInformationV2.Components.Controls {

    public partial class DateText {
        private DateTime? _value;

        [Parameter]
        public IEnumerable<FieldItem> FieldItems { get; set; } = default!;

        public string Id => Title.Replace(" ", "_").ToLowerInvariant();

        [CascadingParameter]
        public SidebarLayout Layout { get; set; } = default!;

        [Parameter]
        public string Title { get; set; } = "";

        [Parameter]
        public DateTime? Value {
            get => _value;
            set {
                if (_value == value)
                    return;

                _value = value;
                ValueChanged.InvokeAsync(value);
            }
        }

        [Parameter]
        public EventCallback<DateTime?> ValueChanged { get; set; }

        public bool GetFieldItemActive() => FieldItems == null ? false : FieldItems.FirstOrDefault(f => f.Title == Title)?.ShowItem ?? true;

        public string GetFieldItemDescription() => FieldItems == null ? "" : FieldItems.FirstOrDefault(f => f.Title == Title)?.Description ?? "";
    }
}