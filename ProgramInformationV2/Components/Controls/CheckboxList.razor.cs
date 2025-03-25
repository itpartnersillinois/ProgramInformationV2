using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.FieldList;
using ProgramInformationV2.Search.Models;

namespace ProgramInformationV2.Components.Controls {

    public partial class CheckboxList {
        private FormatType? _value;

        [Parameter]
        public IEnumerable<FieldItem> FieldItems { get; set; } = default!;

        [CascadingParameter]
        public SidebarLayout Layout { get; set; } = default!;

        [Parameter]
        public string Title { get; set; } = "";

        [Parameter]
        public FormatType? Value {
            get => _value;
            set {
                if (_value == value)
                    return;
                if (Layout != null)
                    Layout.SetDirty();
                _value = value;
                _ = ValueChanged.InvokeAsync(value ?? FormatType.None);
            }
        }

        [Parameter]
        public EventCallback<FormatType> ValueChanged { get; set; }

        public bool GetFieldItemActive() => FieldItems == null ? false : FieldItems.FirstOrDefault(f => f.Title == Title)?.ShowItem ?? true;

        public string GetFieldItemDescription() => FieldItems == null ? "" : FieldItems.FirstOrDefault(f => f.Title == Title)?.Description ?? "";

        protected void ChangeFormat(FormatType format, ChangeEventArgs args) {
            if (args.Value != null && Value != null && (bool) args.Value) {
                Value |= format;
            } else if (args.Value != null && Value != null) {
                Value &= ~format;
            }
        }
    }
}