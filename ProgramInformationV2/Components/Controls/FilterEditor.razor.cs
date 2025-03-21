using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Data.DataModels;

namespace ProgramInformationV2.Components.Controls {
    public partial class FilterEditor {
        private TagSource? _value;

        [Parameter]
        public string FilterType { get; set; } = "";

        [Parameter]
        public TagSource? Value {
            get => _value;
            set {
                if (_value == value)
                    return;

                _value = value;
                ValueChanged.InvokeAsync(value);
            }
        }

        [Parameter]
        public EventCallback<TagSource> ValueChanged { get; set; }

        [Parameter]
        public string Title { get; set; } = default!;

        [Parameter]
        public EventCallback<TagSource> MoveUpCallback { get; set; }

        public async Task MoveUp() => await MoveUpCallback.InvokeAsync(Value);

        [Parameter]
        public EventCallback<TagSource> MoveDownCallback { get; set; }

        public async Task MoveDown() => await MoveDownCallback.InvokeAsync(Value);

        [Parameter]
        public EventCallback<TagSource> RemoveCallback { get; set; }

        public async Task Remove() => await RemoveCallback.InvokeAsync(Value);

    }
}
