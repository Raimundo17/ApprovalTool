namespace InvestmentApprovalTool.Data;

using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

public class DataContext : DbContext
{

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; } = null!;

    public virtual DbSet<DocType> DocTypes { get; set; } = null!;
    public virtual DbSet<ProjectType> ProjectTypes { get; set; } = null!;

    public virtual DbSet<Investments> Investments { get; set; } = null!;

    public virtual DbSet<Plant> Plants { get; set; } = null!;

    public virtual DbSet<Status> Statuses { get; set; } = null!;

    public virtual DbSet<ProcessLevel> ProcessLevels { get; set; } = null!;

    public virtual DbSet<FXRate> FXRates { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;
    public virtual DbSet<Role> Roles { get; set; } = null!;

    public virtual DbSet<InvestmentsHistory> InvestmentsHistories { get; set; } = null!;

    public virtual DbSet<Comment> Comments { get; set; } = null!;
}
