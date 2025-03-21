using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.DataModels;

namespace ProgramInformationV2.Components.Controls {
    public partial class FilterToggle {
        [CascadingParameter]
        public required SidebarLayout Layout { get; set; }

        [Parameter]
        public TagSource Filter { get; set; } = default!;

        public string FilterId => Regex.Replace(Filter.Title, "[^A-Za-z0-9 -]", "").ToLowerInvariant();
    }
}
