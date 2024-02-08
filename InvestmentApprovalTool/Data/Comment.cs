namespace InvestmentApprovalTool.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Investments")]
        public int InvestmentsId { get; set; }
        public virtual Investments? Investments { get; set; }

        public string? Approver3 { get; set; }

        public string? Approver3Comment { get; set; }
        public string? Approver4 { get; set; }

        public string? Approver4Comment { get; set; }

        public string? Approver5 { get; set; }

        public string? Approver5Comment { get; set; }

        public string? Approver6 { get; set; }

        public string? Approver6Comment { get; set; }

        public string? Approver7 { get; set; }

        public string? Approver7Comment { get; set; }

        public string? Approver8 { get; set; }

        public string? Approver8Comment { get; set; }

        public string? Approver9 { get; set; }

        public string? Approver9Comment { get; set; }

        public string? Approver10 { get; set; }

        public string? Approver10Comment { get; set; }

        public string? Approver11 { get; set; }

        public string? Approver11Comment { get; set; }

        public string? Approver12 { get; set; }

        public string? Approver12Comment { get; set; }

        public string? Approver13 { get; set; }

        public string? Approver13Comment { get; set; }

        public string? Approver14 { get; set; }

        public string? Approver14Comment { get; set; }

        public string? Approver15 { get; set; }

        public string? Approver15Comment { get; set; }
    }
}
