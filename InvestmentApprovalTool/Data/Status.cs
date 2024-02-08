namespace InvestmentApprovalTool.Data
{
    using System.ComponentModel.DataAnnotations;

    public class Status
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? StatusDesignation { get; set; }
    }
}
