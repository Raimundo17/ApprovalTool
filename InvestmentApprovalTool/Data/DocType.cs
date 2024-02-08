namespace InvestmentApprovalTool.Data
{
    using System.ComponentModel.DataAnnotations;

    public class DocType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}
