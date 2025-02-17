using System.Text.Json;

namespace ProgramInformationV2.Data.DataModels {

    public abstract class BaseDataItem {
        public abstract int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}