namespace InvestmentApprovalTool.Services
{
    using InvestmentApprovalTool.Data;
    using InvestmentApprovalTool.Helpers;

    public interface IInvestmentHistoryService
    {
        Task<List<InvestmentsHistory>> GetAll();
        Task<InvestmentsHistory> GetById(int id);
        Task<InvestmentsHistory> Create(InvestmentsHistory investmentshistory);
        Task Update(InvestmentsHistory investmentshistory);
        Task Delete(InvestmentsHistory investmentshistory);
        Task<InvestmentsHistory> GetInvestmentHistoryByInvestmentId(int investmentid);    
    }

    public class InvestmentHistoryService : IInvestmentHistoryService
    {
        private readonly DataContext _context;

        public InvestmentHistoryService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<InvestmentsHistory>> GetAll()
        {
            var investmentshistory = _context.InvestmentsHistories.ToList();

            investmentshistory.ForEach(_investmentshistory =>
            {
                _context.Entry(_investmentshistory).Reference(_investmentshistory => _investmentshistory.Investments).Load();
            });

            return investmentshistory;
        }

        public async Task<InvestmentsHistory> GetInvestmentHistoryByInvestmentId(int investmentid)
        {
           var investmenthistory = _context.InvestmentsHistories.Where(x => x.InvestmentsId == investmentid)
                .First();
            ;
            return investmenthistory;
        }

        public async Task<InvestmentsHistory> GetById(int id)
        {
            return getInvestmentHistory(id);
        }

        public async Task<InvestmentsHistory> Create(InvestmentsHistory investmentshistory)
        {
            // validate
            if (_context.InvestmentsHistories.Any(x => x.Id == investmentshistory.Id))
                throw new AppException("Investment history with Id '" + investmentshistory.Id + "' already exists");

            _context.InvestmentsHistories.Add(investmentshistory);

            _context.Entry(investmentshistory).Reference(s => s.Investments).Load();
            await _context.SaveChangesAsync();

            return investmentshistory;
        }

        public async Task Update(InvestmentsHistory investmentshistory)
        {
            _context.Update(investmentshistory);
            _context.Entry(investmentshistory).Reference(s => s.Investments).Load();
            await _context.SaveChangesAsync();
        }

        public async Task Delete(InvestmentsHistory investmentshistory)
        {
            _context.Remove(investmentshistory);
            _context.Entry(investmentshistory).Reference(s => s.Investments).Load();
            _context.SaveChanges();
        }

        private InvestmentsHistory getInvestmentHistory(int id)
        {
            var investmenthistory = _context.InvestmentsHistories.Find(id);
            if (investmenthistory == null) throw new KeyNotFoundException("Investment history not found");
            return investmenthistory;
        }
    }
}
