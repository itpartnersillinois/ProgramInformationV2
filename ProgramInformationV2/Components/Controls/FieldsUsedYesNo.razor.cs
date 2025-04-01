using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Components.Layout;

namespace ProgramInformationV2.Components.Controls {

    public partial class FieldsUsedYesNo {
        private bool _value;

        [CascadingParameter]
        public SidebarLayout Layout { get; set; } = default!;

        [Parameter]
        public string Title { get; set; } = "";

        [Parameter]
        public bool Value {
            get => _value;
            set {
                if (_value == value)
                    return;

                _value = value;
                ValueChanged.InvokeAsync(value);
            }
        }

        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        public void SaveUsedChange(bool value) {
            Value = value;
            Layout.SetDirty();
        }
    }
}