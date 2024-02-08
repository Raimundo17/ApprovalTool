namespace InvestmentApprovalTool.Data
{
    using System.ComponentModel.DataAnnotations;

    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
    }
}
