using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.FieldList;

namespace ProgramInformationV2.Components.Controls {

    public partial class DayOfWeekCheckboxList {
        private List<DayOfWeek>? _value;

        [Parameter]
        public IEnumerable<FieldItem> FieldItems { get; set; } = default!;

        [CascadingParameter]
        public SidebarLayout Layout { get; set; } = default!;

        [Parameter]
        public string Title { get; set; } = "";

        [Parameter]
        public List<DayOfWeek>? Value {
            get => _value;
            set {
                if (_value == value)
                    return;
                if (Layout != null)
                    Layout.SetDirty();
                _value = value;
                _ = ValueChanged.InvokeAsync(value);
            }
        }

        [Parameter]
        public EventCallback<List<DayOfWeek>?> ValueChanged { get; set; }

        public bool GetFieldItemActive() => FieldItems == null ? false : FieldItems.FirstOrDefault(f => f.Title == Title)?.ShowItem ?? true;

        public string GetFieldItemDescription() => FieldItems == null ? "" : FieldItems.FirstOrDefault(f => f.Title == Title)?.Description ?? "";

        protected void ChangeDate(DayOfWeek date, ChangeEventArgs args) {
            if (args.Value != null && Value != null && (bool) args.Value && !Value.Contains(date)) {
                Value.Add(date);
            } else if (args.Value != null && Value != null && Value.Contains(date)) {
                Value.Remove(date);
            }
        }
    }
}