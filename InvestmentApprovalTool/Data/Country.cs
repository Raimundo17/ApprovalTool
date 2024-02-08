namespace InvestmentApprovalTool.Data
{
    using System.ComponentModel.DataAnnotations;

    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "must have min length of 10 and max Length of 50")]
        public string? CountryName { get; set; }
    }
}
