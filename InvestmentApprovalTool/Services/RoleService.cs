namespace InvestmentApprovalTool.Services
{
    using InvestmentApprovalTool.Data;

    public interface IRoleService
    {
        Task<List<Role>> GetAll();
    }

    public class RoleService: IRoleService
    {
        private readonly DataContext _context;
 
        public RoleService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetAll()
        {
            var rolesList = _context.Roles.ToList();
            return rolesList;
        }
    }
}
