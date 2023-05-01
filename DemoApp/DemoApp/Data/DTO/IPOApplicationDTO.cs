using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DemoApp.Data.DTO
{
    public class IPOApplicationDTO
    {
        //Application Details
        public int IpoApplicationId { get; set; }
        public int UserId { get; set; }
        public int InstrumentId { get; set; } = default!;
        public bool IsApproved { get; set; }
        public bool IsExecuted { get; set; }

        [Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal ApplicationAmount { get; set; }
        [Column(TypeName = "date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime CreatedDate { get; set; }

        //IPOInformation

        public string InstrumentName { get; set; } = default!;
        [Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal Facevalue { get; set; }
        [Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal Premium { get; set; }
        [Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal IpoRate { get; set; }
        [Column(TypeName = "money"), DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal MinimumAmount { get; set; }
        [Column(TypeName = "date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }

        //Company
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = default!;


        //Extra
        public decimal? totalAmount { get; set; }
        public string? totalAmountinword { get; set; }
    }
}
