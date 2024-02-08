namespace InvestmentApprovalTool.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class InvestmentsHistory
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Investments")]
        public int InvestmentsId { get; set; }
        public virtual Investments? Investments  { get; set; }

        public DateTime? ProcesslevelDate2 { get; set; }
        public string? Approval2role { get; set; }
        public string? Approval2 { get; set; }

        public DateTime? ProcesslevelDate3 { get; set; }
        public string? Approval3role { get; set; }
        public string? Approval3 { get; set; }

        public DateTime? ProcesslevelDate4 { get; set; }
        public string? Approval4role { get; set; }
        public string? Approval4 { get; set; }

        public DateTime? ProcesslevelDate5 { get; set; }
        public string? Approval5role { get; set; }
        public string? Approval5 { get; set; }

        public DateTime? ProcesslevelDate6 { get; set; }
        public string? Approval6role { get; set; }
        public string? Approval6 { get; set; }

        public DateTime? ProcesslevelDate7 { get; set; }
        public string? Approval7role { get; set; }
        public string? Approval7 { get; set; }

        public DateTime? ProcesslevelDate8 { get; set; }
        public string? Approval8role { get; set; }
        public string? Approval8 { get; set; }

        public DateTime? ProcesslevelDate9 { get; set; }
        public string? Approval9role { get; set; }
        public string? Approval9 { get; set; }

        public DateTime? ProcesslevelDate10 { get; set; }
        public string? Approval10role { get; set; }
        public string? Approval10 { get; set; }

        public DateTime? ProcesslevelDate11 { get; set; }
        public string? Approval11role { get; set; }
        public string? Approval11 { get; set; }

        public DateTime? ProcesslevelDate12 { get; set; }
        public string? Approval12role { get; set; }
        public string? Approval12 { get; set; }

        public DateTime? ProcesslevelDate13 { get; set; }
        public string? Approval13role { get; set; }
        public string? Approval13 { get; set; }

        public DateTime? ProcesslevelDate14 { get; set; }
        public string? Approval14role { get; set; }
        public string? Approval14 { get; set; }

        public DateTime? ProcesslevelDate15 { get; set; }
        public string? Approval15role { get; set; }
        public string? Approval15 { get; set; }

    }
}
