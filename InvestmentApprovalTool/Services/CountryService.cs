namespace InvestmentApprovalTool.Services
{
    using InvestmentApprovalTool.Data;
    using InvestmentApprovalTool.Helpers;

    public interface ICountryService
    {
        Task<List<Country>> GetAll();
        Task<Country> GetById(int id);
        Task<Country> Create(Country country);
        Task Update(Country country);
        Task Delete(Country country);
    }

    public class CountryService : ICountryService
    {
        private readonly DataContext _context;

        public CountryService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Country>> GetAll()
        {
            var country = _context.Countries.ToList();
            return country;
        }

        public async Task<Country> GetById(int id)
        {
            return getCountry(id);
        }

        public async Task<Country> Create(Country country)
        {
            // validate
            if (_context.Countries.Any(x => x.Id == country.Id))
                throw new AppException("Country with Id '" + country.Id + "' already exists");

            _context.Countries.Add(country);
            await _context.SaveChangesAsync();

            return country;
        }

        public async Task Update(Country country)
        {
            _context.Update(country);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Country country)
        {
            if (_context.Countries.Any(x => x.Id == country.Id))
                _context.Remove(country);
            _context.SaveChanges();
        }

        private Country getCountry(int id)
        {
            var country = _context.Countries.Find(id);
            if (country == null) throw new KeyNotFoundException("Country not found");
            return country;
        }
    }
}
