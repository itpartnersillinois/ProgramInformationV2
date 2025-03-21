using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgramInformationV2.Data.DataModels {

    public class FieldSource : BaseDataItem {
        public CategoryType CategoryType { get; set; }
        public string Description { get; set; } = "";

        public string Title { get; set; } = "";

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        public bool ShowItem { get; set; }

        public virtual Source? Source { get; set; }
        public int SourceId { get; set; }
    }
}