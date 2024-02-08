namespace InvestmentApprovalTool.Data
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? NetId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        public DateTime? createdOn { get; set; }
        public string? createdBy { get; set; }
        public bool isActive { get; set; }
        
        [AllowNull]
        public bool? IsAdmin { get; set; }

        //public virtual ICollection<Role> Roles { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }

        //FK
        [ForeignKey("Plant")]
        public int? PlantId { get; set; }
        public virtual Plant? Plant { get; set; }

        //FK
        [ForeignKey("Country")]
        public int? CountryId { get; set; }
        public virtual Country? Country { get; set; }
    }
}
