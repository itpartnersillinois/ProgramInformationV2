using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Search.Models;

namespace ProgramInformationV2.Components.Controls {

    public partial class SearchGenericItem {

        [Parameter]
        public EventCallback<string> EditClicked { get; set; }

        [Parameter]
        public List<GenericItem> GenericItems { get; set; } = default!;

        public bool IsEditDisabled => SelectedItemId == null || string.IsNullOrWhiteSpace(SelectedItemId);

        [Parameter]
        public string ItemTitle { get; set; } = "";

        [Parameter]
        public EventCallback<string> SearchClicked { get; set; }

        [Parameter]
        public string SearchItem { get; set; } = "";

        [Parameter]
        public string SelectedItemId { get; set; } = "";

        public void Edit() {
            EditClicked.InvokeAsync();
        }

        public void Search() {
            SearchClicked.InvokeAsync();
        }

        protected async Task FilterChange(ChangeEventArgs e) {
            SearchItem = e.Value?.ToString() ?? "";
            await SearchClicked.InvokeAsync();
        }
    }
}