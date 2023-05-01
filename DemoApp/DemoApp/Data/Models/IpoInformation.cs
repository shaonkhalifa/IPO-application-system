using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoApp.Data.Models
{
    public class IpoInformation
    {
        [Key]
        public int InstrumentId { get; set; }
        public string InstrumentName { get; set; } = default!;
        [Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal Facevalue { get; set; }
        [Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal Premium { get; set; }
        [Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal IpoRate { get; set;}
        [Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal MinimumAmount { get; set; }
        [Column(TypeName = "date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }

        public virtual ICollection<IpoApplicationDetails> ApplicationDetails { get; set; } = default!;
    }
}
