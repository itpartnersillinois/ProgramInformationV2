using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgramInformationV2.Data.DataModels {

    public class TagSource : BaseDataItem {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        public int Order { get; set; }
        public virtual Source? Source { get; set; }
        public int SourceId { get; set; }
        public TagType TagType { get; set; }
        public string Title { get; set; } = "";
        [NotMapped]
        public string OldTitle { get; set; } = "";
        [NotMapped]
        public bool EnabledBySource { get; set; }
    }
}