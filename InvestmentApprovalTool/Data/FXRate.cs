namespace InvestmentApprovalTool.Data
{
    using System.ComponentModel.DataAnnotations;

    public class FXRate
    {
        public int Id { get; set; }
        [Required]
        public double FXRateValue { get; set; }
    }
}
