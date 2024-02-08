namespace InvestmentApprovalTool.Services
{
    using InvestmentApprovalTool.Data;
    using InvestmentApprovalTool.Helpers;

    public interface IFXRateService
    {
        Task<List<FXRate>> GetAll();
        Task<FXRate> GetById(int id);
        Task<FXRate> Create(FXRate fXRate);
        Task Update(FXRate fXRate);
        Task Delete(FXRate fXRate);
        Task<FXRate> GetFxRate();
    }

    public class FXRateService : IFXRateService
    {
        private readonly DataContext _context;

        public FXRateService(DataContext context)
        {
            _context = context;
        }

        public async Task<FXRate> GetFxRate()
        {
            return _context.FXRates.Single();
        }

        public async Task<List<FXRate>> GetAll()
        {
            return _context.FXRates.ToList();
        }

        public async Task<FXRate> GetById(int id)
        {
            return getFXRate(id);
        }

        public async Task<FXRate> Create(FXRate fXRate)
        {
            // validate
            if (_context.FXRates.Any(x => x.Id == fXRate.Id))
                throw new AppException("FX Rate with Id '" + fXRate.Id + "' already exists");

            _context.FXRates.Add(fXRate);
            await _context.SaveChangesAsync();

            return fXRate;
        }

        public async Task Update(FXRate fXRate)
        {
            _context.Update(fXRate);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(FXRate fXRate)
        {
            if (_context.FXRates.Any(x => x.Id == fXRate.Id))
                _context.Remove(fXRate);
            _context.SaveChanges();
        }

        private FXRate getFXRate(int id)
        {
            var fxrate = _context.FXRates.Find(id);
            if (fxrate == null) throw new KeyNotFoundException("FX  not found");
            return fxrate;
        }
    }
}
