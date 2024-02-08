namespace InvestmentApprovalTool.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Plant
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Country")]
        public int? CountryId { get; set; }
        public virtual Country? Countries { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "must have min length of 4 and max Length of 50")]
        public string? PlantName { get; set; }

    }
}
