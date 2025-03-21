using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using ProgramInformationV2.Components.Layout;
using ProgramInformationV2.Data.FieldList;
using ProgramInformationV2.Data.Uploads;

namespace ProgramInformationV2.Components.Controls {
    public partial class ImageControl {
        private string? _value;
        private const string _tempName = "-temp";
        private readonly int _maxAllowedSize = 2000;
        private string _originalValue = "";
        private string _latestFileName = "";
        private UploaderStatusEnum _uploaderStatus = UploaderStatusEnum.Untouched;

        [CascadingParameter]
        public required SidebarLayout Layout { get; set; }

        [Parameter]
        public string Title { get; set; } = "";

        [Parameter]
        public string ItemId { get; set; } = "";

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

        public string? ValueUrl => string.IsNullOrWhiteSpace(Value) ? "" : Value + "?cache=" + Guid.NewGuid().ToString();

        [Inject]
        protected UploadStorage UploadStorage { get; set; } = default!;


        public string Id => Title.Replace(" ", "_").ToLowerInvariant();
        public string IdUpload => Id + "_upload";

        public bool ShowExternalUrl => !UploadStorage.IsPartOfPath(Value ?? "");

        [Parameter]
        public IEnumerable<FieldItem> FieldItems { get; set; } = default!;

        public string GetFieldItemDescription() => FieldItems == null ? "" : FieldItems.FirstOrDefault(f => f.Title == Title)?.Description ?? "";
        public bool GetFieldItemActive() => FieldItems == null ? false : FieldItems.FirstOrDefault(f => f.Title == Title)?.ShowItem ?? true;

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        public async Task<bool> SaveFileToTemp(InputFileChangeEventArgs e) {
            if (e.File.Size > 1024 * _maxAllowedSize) {
                await Layout.AddMessage($"File is too large -- size of file is {float.Round(e.File.Size / (float) (1024 * 1000), 2)}MB and maximum size is {_maxAllowedSize / 1000}MB");
                return false;
            }
            _latestFileName = ItemId + "_" + Id + Path.GetExtension(e.File.Name);
            var filename = await UploadStorage.Upload(ItemId + "_" + Id + _tempName, e.File.ContentType, e.File.OpenReadStream(maxAllowedSize: 1024 * _maxAllowedSize));
            Value = UploadStorage.GetFullPath(filename);
            Layout.SetDirty();
            _uploaderStatus = UploaderStatusEnum.Uploaded;
            return true;
        }

        public void DeleteFile() {
            if (string.IsNullOrWhiteSpace(_originalValue)) {
                _originalValue = Value ?? "";
            }
            Value = "";
            Layout.SetDirty();
            _uploaderStatus = UploaderStatusEnum.Deleted;
        }

        public async Task<bool> SaveFileToPermanent() {
            if (_uploaderStatus == UploaderStatusEnum.Uploaded && !string.IsNullOrWhiteSpace(Value)) {
                Value = await UploadStorage.Move(_latestFileName, Value);
                return true;
            } else if (_uploaderStatus == UploaderStatusEnum.Deleted && !string.IsNullOrWhiteSpace(_originalValue)) {
                _ = await UploadStorage.Delete(_originalValue);
                return true;
            }
            return false;
        }
    }
}
