namespace InvestmentApprovalTool.Services
{
    using InvestmentApprovalTool.Data;
    using InvestmentApprovalTool.Helpers;

    public interface IPlantService
    {
        Task<List<Plant>> GetAll();
        Task<Plant> GetById(int id);
        Task<Plant> Create(Plant plant);
        Task Update(Plant plant);
        Task Delete(Plant plant);
        List<Plant> GetPlantByCountryId(int countryid);
    }

    public class PlantService : IPlantService
    {
        private readonly DataContext _context;

        public PlantService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Plant>> GetAll()
        {
            return _context.Plants.ToList();
        }

        public List<Plant> GetPlantByCountryId(int countryid)
        {
            var plant = _context.Plants.Where(x => x.CountryId == countryid)
                 .ToList();
            ;
            return plant;
        }

        public async Task<Plant> GetById(int id)
        {
            return getPlant(id);
        }

        public async Task<Plant> Create(Plant plant)
        {
            // validate
            if (_context.Plants.Any(x => x.Id == plant.Id))
                throw new AppException("Plant with Id '" + plant.Id + "' already exists");

            _context.Plants.Add(plant);
            _context.Entry(plant).Reference(s => s.Countries).Load();
            await _context.SaveChangesAsync();

            return plant;
        }

        public async Task Update(Plant plant)
        {
            _context.Update(plant);
            _context.Entry(plant).Reference(s => s.Countries).Load();
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Plant plant)
        {
            if (_context.Plants.Any(x => x.Id == plant.Id))
                _context.Remove(plant);
            _context.Entry(plant).Reference(s => s.Countries).Load();
            _context.SaveChanges();
        }

        private Plant getPlant(int id)
        {
            var plant = _context.Plants.Find(id);
            if (plant == null) throw new KeyNotFoundException("Plant not found");
            return plant;
        }
    }
}
