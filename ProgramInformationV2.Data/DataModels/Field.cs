using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgramInformationV2.Data.DataModels {

    public class Field : BaseDataItem {
        public CategoryType CategoryType { get; set; }
        public FieldType FieldType { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        public string InitialDescription { get; set; } = "";
        public string Title { get; set; } = "";
    }
}