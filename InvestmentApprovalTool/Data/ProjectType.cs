namespace InvestmentApprovalTool.Data
{
    using System.ComponentModel.DataAnnotations;

    public class ProjectType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}
