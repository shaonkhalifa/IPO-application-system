using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoApp.Data.Models
{
    public class IpoApplicationDetails
    {
        [Key]
        public int IpoApplicationId { get; set; }
        [ForeignKey("Users")]
        public int UserId { get; set; }

        [ForeignKey("IpoInformation")]
        public int InstrumentId { get; set; }

        public bool IsApproved { get; set; }

        public bool IsExecuted { get; set; }

        [Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal ApplicationAmount { get; set; }
        [Column(TypeName = "date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreatedDate { get; set; }

        public virtual User Users { get; set; } = default!;

        public virtual  IpoInformation  IpoInformation { get; set; } = default!;

    }
}
