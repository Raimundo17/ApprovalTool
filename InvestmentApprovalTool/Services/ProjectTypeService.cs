namespace InvestmentApprovalTool.Services
{
    using InvestmentApprovalTool.Data;
    using InvestmentApprovalTool.Helpers;

    public interface IProjectTypeService
    {
        Task<List<ProjectType>> GetAll();
        Task<ProjectType> GetById(int id);
        Task<ProjectType> Create(ProjectType projectType);
        Task Update(ProjectType projectType);
        Task Delete(ProjectType projectType);
    }

    public class ProjectTypeService : IProjectTypeService
    {
        private readonly DataContext _context;

        public ProjectTypeService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectType>> GetAll()
        {
            return _context.ProjectTypes.ToList();
        }

        public async Task<ProjectType> GetById(int id)
        {
            return getProjectType(id);
        }

        public async Task<ProjectType> Create(ProjectType projectType)
        {
            // validate
            if (_context.ProjectTypes.Any(x => x.Id == projectType.Id))
                throw new AppException("Project Type with Id '" + projectType.Id + "' already exists");

            _context.ProjectTypes.Add(projectType);
            await _context.SaveChangesAsync();

            return projectType;
        }

        public async Task Update(ProjectType projectType)
        {
            _context.Update(projectType);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(ProjectType projectType)
        {
            if (_context.ProjectTypes.Any(x => x.Id == projectType.Id))
                _context.Remove(projectType);
            _context.SaveChanges();
        }

        private ProjectType getProjectType(int id)
        {
            var projectType = _context.ProjectTypes.Find(id);
            if (projectType == null) throw new KeyNotFoundException("ProjectType not found");
            return projectType;
        }
    }
}
