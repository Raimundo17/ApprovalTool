namespace InvestmentApprovalTool.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public class Investments
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Requester")]
        public int RequesterId { get; set; }
        public virtual User? Requester { get; set; }

        public DateTime? IssueDate  { get; set; }

        public string? ArNumber { get; set; }

        [Required(ErrorMessage = "IR DR Number cannot be null")]
        public string? IR_DRNumber { get; set; }

        [Required(ErrorMessage = "Please insert a valid Project Title")]
        public string? ProjectTitle_DisposalDescription  { get; set; }

        //FK
        [ForeignKey("Plant")]
        public int PlantId { get; set; }
        public virtual Plant? Plant { get; set; }

        //FK
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public virtual Country? Country { get; set; }

        public double IRValueEuro { get; set; }

        public double IRValueDollar { get; set; }

        //FK
        [ForeignKey("DocType")]
        public int DocTypeId { get; set; }
        public virtual DocType? DocType { get; set;}

        //FK
        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public virtual Status? Status { get; set; }

        [ForeignKey("ProjectType")]
        public int ProjectTypeId { get; set; }
        public virtual ProjectType? ProjectType { get; set; }

        [ForeignKey("ProcessLevel")]
        public int ProcessLevelId { get; set; }
        public virtual ProcessLevel? ProcessLevel { get; set; }

        [ForeignKey("ProgramManager")]
        public int? ProgramManagerId { get; set; }
        public virtual User? ProgramManager { get; set; }

        public string? Comments { get; set; }

        public string? RegNumber { get; set; }

        public bool Approver3hasReviewed { get; set; } = false;

        public bool Approver4hasReviewed { get; set; } = false;

        public bool Approver5hasReviewed { get; set; } = false;

        public bool Approver6hasReviewed { get; set; } = false;

        public bool Approver7hasReviewed { get; set; } = false;

        public bool Approver8hasReviewed { get; set; } = false;

        public bool Approver9hasReviewed { get; set; } = false;

        public bool Approver10hasReviewed { get; set; } = false;

        public bool Approver11hasReviewed { get; set; } = false;

        public bool Approver12hasReviewed { get; set; } = false;

        public bool Approver13hasReviewed { get; set; } = false;

        public bool Approver14hasReviewed { get; set; } = false;

        public bool Approver15hasReviewed { get; set; } = false;

        // DOCUMENTATION 
        public string? Investment_Form  { get; set; }

        public string? Disposal_Form { get; set; }

        public string? MSD { get; set; }

        public string? Exception_Letter { get; set; }

        public string? ECCLS { get; set; }

        public string? RebillPOSalesConfirmation { get; set; }

        public string? Others { get; set; }

        public string? Others_MAC { get; set; }

        public string? Others_CFI { get; set; }

        // APPROVMENT FLOW HISTORY PDF
        public string? HistoryPDF { get; set; }

        // Release Document
        public string? ReleasedDocument { get; set; }

        [AllowNull]
        public string? pending { get; set; }

        [AllowNull]
        public string? productionConceptChange { get; set; }
    }
}
