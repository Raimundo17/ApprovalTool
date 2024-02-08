namespace InvestmentApprovalTool.Services
{
    using InvestmentApprovalTool.Data;
    using InvestmentApprovalTool.Helpers;

    public interface IInvestmentService
    {
        List<Investments> GetAll();
        Task<List<Investments>> GetAllAsync();
        Task<Investments> GetById(int id);
        Task<Investments> Create(Investments investments);
        Task Update(Investments investments);
        Task Delete(Investments investments);
        Task<List<Investments>> GetInvestmentsByPMId(int userId, int processlevel);
        Task<List<Investments>> GetAllPendingsByUserId(int id, int processlevel);
        Task<List<Investments>> GetAllPendingsByProcessLevelAndPlant(int processlevel, int plantId);
        Task<List<Investments>> GetInvestmentsHistoryRequesterCountry(int countryId);
        Task<List<Investments>> GetAllPendingsByUserIdRequester(int countryID);
        Task<List<Investments>> GetInvestmentsHistoryByPlant(int roleid, int plantId);
        Task<List<Investments>> GetInvestmentsHistory(int roleid);

        Task<List<Investments>> GetInvestmentsHistoryRequesterCountryActualYear(int countryId);

        Task<List<Investments>> GetInvestmentsHistoryByPlantActualYear(int roleid, int plantId);

        Task<List<Investments>> GetInvestmentsHistoryActualYear(int roleid);
    }

    public class InvestmentService : IInvestmentService
    {
        private readonly DataContext _context;
        
        public InvestmentService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Investments>> GetInvestmentsByPMId(int userId, int processlevel)
        {
            var investments = new List<Investments>();

            investments = _context.Investments.Where(x => x.ProgramManagerId == userId && x.Approver8hasReviewed == false && x.ProcessLevelId == processlevel ).ToList();

            investments.ForEach(_investments =>
            {
                _context.Entry(_investments).Reference(_investments => _investments.ProjectType).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Country).Load();
                _context.Entry(_investments).Reference(_investments => _investments.DocType).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Status).Load();
                _context.Entry(_investments).Reference(_investments => _investments.ProcessLevel).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Plant).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Requester).Load();
            });

            return investments;
        }

        public List<Investments> GetAll()
        {
            var investments = _context.Investments.ToList();

            investments.ForEach(_investments =>
            {
                _context.Entry(_investments).Reference(_investments => _investments.ProjectType).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Country).Load();
                _context.Entry(_investments).Reference(_investments => _investments.DocType).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Status).Load();
                _context.Entry(_investments).Reference(_investments => _investments.ProcessLevel).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Plant).Load();
            });

            return investments;
        }

        public async Task<List<Investments>> GetAllAsync()
        {
            var investments = _context.Investments.ToList();

            investments.ForEach(_investments =>
            {
                _context.Entry(_investments).Reference(_investments => _investments.ProjectType).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Country).Load();
                _context.Entry(_investments).Reference(_investments => _investments.DocType).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Status).Load();
                _context.Entry(_investments).Reference(_investments => _investments.ProcessLevel).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Plant).Load();
            });

            return investments;
        }

        // Trás todos os investments que tenham de ser aprovados para o aprovador
        public async Task<List<Investments>> GetAllPendingsByUserId(int id, int processlevel)
        {
            var investments = new List<Investments>();

            investments = _context.Investments.Where(x => x.StatusId == 2 & x.ProcessLevelId == processlevel).ToList();

            investments.ForEach(_investments =>
            {
                _context.Entry(_investments).Reference(_investments => _investments.ProjectType).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Country).Load();
                _context.Entry(_investments).Reference(_investments => _investments.DocType).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Status).Load();
                _context.Entry(_investments).Reference(_investments => _investments.ProcessLevel).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Plant).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Requester).Load();
            });

            return investments;
        }

        public async Task<List<Investments>> GetAllPendingsByProcessLevelAndPlant(int processlevel, int plantId)
        {
            var investments = new List<Investments>();

            investments = _context.Investments.Where(x => x.StatusId == 2 & x.ProcessLevelId == processlevel & x.PlantId == plantId).ToList();

            investments.ForEach(_investments =>
            {
                _context.Entry(_investments).Reference(_investments => _investments.ProjectType).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Country).Load();
                _context.Entry(_investments).Reference(_investments => _investments.DocType).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Status).Load();
                _context.Entry(_investments).Reference(_investments => _investments.ProcessLevel).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Plant).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Requester).Load();
            });

            return investments;
        }
        
        // Trás os investimentos que sejam drafts para os requesters, filtrados por país
        public async Task<List<Investments>> GetAllPendingsByUserIdRequester(int countryID)
        {
            var investments = new List<Investments>();

            investments = _context.Investments.Where(x => x.StatusId == 1 & x.CountryId == countryID).ToList();

            investments.ForEach(_investments =>
            {
                _context.Entry(_investments).Reference(_investments => _investments.ProjectType).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Country).Load();
                _context.Entry(_investments).Reference(_investments => _investments.DocType).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Status).Load();
                _context.Entry(_investments).Reference(_investments => _investments.ProcessLevel).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Plant).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Requester).Load();
            });

            return investments;
        }

        // Tras os investimentos que os aprovadores ja aprovaram
        public async Task<List<Investments>> GetInvestmentsHistory(int roleid)
        {
            var investments = new List<Investments>();

            investments = _context.Investments.Where(x => x.StatusId != 1).ToList();

            foreach (var item in investments)
            {
                if (roleid == 3)
                {
                    investments = _context.Investments.Where(x => x.Approver3hasReviewed == true & x.StatusId != 1).ToList();
                }
                else if (roleid == 4)
                {
                    investments = _context.Investments.Where(x => x.Approver4hasReviewed == true & x.StatusId != 1).ToList();
                }
                else if (roleid == 5)
                {
                    investments = _context.Investments.Where(x => x.Approver5hasReviewed == true & x.StatusId != 1).ToList();
                }
                else if (roleid == 6)
                {
                    investments = _context.Investments.Where(x => x.Approver6hasReviewed == true & x.StatusId != 1).ToList();
                }
                else if (roleid == 7)
                {
                    investments = _context.Investments.Where(x => x.Approver7hasReviewed == true & x.StatusId != 1).ToList();
                }
                else if (roleid == 8)
                {
                    investments = _context.Investments.Where(x => x.Approver8hasReviewed == true & x.StatusId != 1).ToList();
                }
                else if (roleid == 9)
                {
                    investments = _context.Investments.Where(x => x.Approver9hasReviewed == true & x.StatusId != 1).ToList();
                }
                else if (roleid == 10)
                {
                    investments = _context.Investments.Where(x => x.Approver10hasReviewed == true & x.StatusId != 1).ToList();
                }
                else if (roleid == 11)
                {
                    investments = _context.Investments.Where(x => x.Approver11hasReviewed == true & x.StatusId != 1).ToList();
                }
                else if (roleid == 12)
                {
                    investments = _context.Investments.Where(x => x.Approver12hasReviewed == true & x.StatusId != 1).ToList();
                }
                else if (roleid == 13)
                {
                    investments = _context.Investments.Where(x => x.Approver13hasReviewed == true & x.StatusId != 1).ToList();
                }
                else if (roleid == 14)
                {
                    investments = _context.Investments.Where(x => x.Approver14hasReviewed == true & x.StatusId != 1).ToList();
                }
                else if (roleid == 15)
                {
                    investments = _context.Investments.Where(x => x.Approver15hasReviewed == true & x.StatusId != 1).ToList();
                }
            }

            investments.ForEach(_investments =>
            {
                _context.Entry(_investments).Reference(_investments => _investments.ProjectType).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Country).Load();
                _context.Entry(_investments).Reference(_investments => _investments.DocType).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Status).Load();
                _context.Entry(_investments).Reference(_investments => _investments.ProcessLevel).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Plant).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Requester).Load();
            });

            return investments;
        }

       
        // Tras os investimentos que os aprovadores ja aprovaram
        public async Task<List<Investments>> GetInvestmentsHistoryByPlant(int roleid, int plantId)
        {
            var investments = new List<Investments>();

            investments = _context.Investments.Where(x => x.StatusId != 1 & x.PlantId == plantId).ToList();

            foreach (var item in investments)
            {
                if (roleid == 3)
                {
                    investments = _context.Investments.Where(x => x.Approver3hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId).ToList();
                }
                else if (roleid == 4)
                {
                    investments = _context.Investments.Where(x => x.Approver4hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId).ToList();
                }
                else if (roleid == 5)
                {
                    investments = _context.Investments.Where(x => x.Approver5hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId).ToList();
                }
                else if (roleid == 6)
                {
                    investments = _context.Investments.Where(x => x.Approver6hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId).ToList();
                }
                else if (roleid == 7)
                {
                    investments = _context.Investments.Where(x => x.Approver7hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId).ToList();
                }
                else if (roleid == 8)
                {
                    investments = _context.Investments.Where(x => x.Approver8hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId).ToList();
                }
                else if (roleid == 9)
                {
                    investments = _context.Investments.Where(x => x.Approver9hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId).ToList();
                }
                else if (roleid == 10)
                {
                    investments = _context.Investments.Where(x => x.Approver10hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId).ToList();
                }
                else if (roleid == 11)
                {
                    investments = _context.Investments.Where(x => x.Approver11hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId).ToList();
                }
                else if (roleid == 12)
                {
                    investments = _context.Investments.Where(x => x.Approver12hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId).ToList();
                }
                else if (roleid == 13)
                {
                    investments = _context.Investments.Where(x => x.Approver13hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId).ToList();
                }
                else if (roleid == 14)
                {
                    investments = _context.Investments.Where(x => x.Approver14hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId).ToList();
                }
                else if (roleid == 15)
                {
                    investments = _context.Investments.Where(x => x.Approver15hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId).ToList();
                }
            }

            investments.ForEach(_investments =>
            {
                _context.Entry(_investments).Reference(_investments => _investments.ProjectType).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Country).Load();
                _context.Entry(_investments).Reference(_investments => _investments.DocType).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Status).Load();
                _context.Entry(_investments).Reference(_investments => _investments.ProcessLevel).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Plant).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Requester).Load();
            });

            return investments;
        }
       
        // Histórico do Requester
        public async Task<List<Investments>> GetInvestmentsHistoryRequesterCountry(int countryId)
        {
            var investments = new List<Investments>();

            investments = _context.Investments.Where(x => x.StatusId == 4 | x.StatusId == 2 | x.StatusId == 5 | x.StatusId == 3 && x.CountryId == countryId).ToList();

            investments.ForEach(_investments =>
            {
                _context.Entry(_investments).Reference(_investments => _investments.ProjectType).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Country).Load();
                _context.Entry(_investments).Reference(_investments => _investments.DocType).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Status).Load();
                _context.Entry(_investments).Reference(_investments => _investments.ProcessLevel).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Plant).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Requester).Load();
            });

            return investments;
        }

        public async Task<Investments> GetById(int id)
        {
            return getInvestment(id);
        }

        public async Task<Investments> Create(Investments investments)
        {
            // validate
            if (_context.Investments.Any(x => x.Id == investments.Id))
                throw new AppException("Investment with Id '" + investments.Id + "' already exists");

            _context.Investments.Add(investments);
            investments.IssueDate = DateTime.Now;

            _context.Entry(investments).Reference(s => s.ProjectType).Load();
            _context.Entry(investments).Reference(s => s.Country).Load();
            _context.Entry(investments).Reference(s => s.Plant).Load();
            _context.Entry(investments).Reference(s => s.DocType).Load();
            _context.Entry(investments).Reference(s => s.Status).Load();
            _context.Entry(investments).Reference(s => s.Requester).Load();
            _context.Entry(investments).Reference(s => s.ProgramManager).Load();
            _context.Entry(investments).Reference(s => s.ProcessLevel).Load();
            await _context.SaveChangesAsync();

            return investments;
        }

        public async Task Update(Investments investments)
        {
            _context.Update(investments);
            _context.Entry(investments).Reference(s => s.ProjectType).Load();
            _context.Entry(investments).Reference(s => s.Country).Load();
            _context.Entry(investments).Reference(s => s.Plant).Load();
            _context.Entry(investments).Reference(s => s.DocType).Load();
            _context.Entry(investments).Reference(s => s.Status).Load();
            _context.Entry(investments).Reference(s => s.ProcessLevel).Load();
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Investments investments)
        {
            _context.Remove(investments);
            _context.Entry(investments).Reference(s => s.ProjectType).Load();
            _context.Entry(investments).Reference(s => s.Country).Load();
            _context.Entry(investments).Reference(s => s.Plant).Load();
            _context.Entry(investments).Reference(s => s.DocType).Load();
            _context.Entry(investments).Reference(s => s.Status).Load();
            _context.Entry(investments).Reference(s => s.ProcessLevel).Load();
            _context.SaveChanges();
        }

        private Investments getInvestment(int id)
        {
            var investment = _context.Investments.Find(id);
            if (investment == null) throw new KeyNotFoundException("Investment not found");
            return investment;
        }

        public async Task<List<Investments>> GetInvestmentsHistoryRequesterCountryActualYear(int countryId)
        {
            var investments = new List<Investments>();

            investments = _context.Investments.Where(x => x.StatusId == 4 | x.StatusId == 2 | x.StatusId == 5 | x.StatusId == 3 && x.CountryId == countryId && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();

            investments.ForEach(_investments =>
            {
                _context.Entry(_investments).Reference(_investments => _investments.ProjectType).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Country).Load();
                _context.Entry(_investments).Reference(_investments => _investments.DocType).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Status).Load();
                _context.Entry(_investments).Reference(_investments => _investments.ProcessLevel).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Plant).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Requester).Load();
            });

            return investments;
        }

        public async Task<List<Investments>> GetInvestmentsHistoryByPlantActualYear(int roleid, int plantId)
        {
            var investments = new List<Investments>();

            investments = _context.Investments.Where(x => x.StatusId != 1 & x.PlantId == plantId && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();

            foreach (var item in investments)
            {
                if (roleid == 3)
                {
                    investments = _context.Investments.Where(x => x.Approver3hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
                else if (roleid == 4)
                {
                    investments = _context.Investments.Where(x => x.Approver4hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
                else if (roleid == 5)
                {
                    investments = _context.Investments.Where(x => x.Approver5hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
                else if (roleid == 6)
                {
                    investments = _context.Investments.Where(x => x.Approver6hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
                else if (roleid == 7)
                {
                    investments = _context.Investments.Where(x => x.Approver7hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
                else if (roleid == 8)
                {
                    investments = _context.Investments.Where(x => x.Approver8hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
                else if (roleid == 9)
                {
                    investments = _context.Investments.Where(x => x.Approver9hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
                else if (roleid == 10)
                {
                    investments = _context.Investments.Where(x => x.Approver10hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
                else if (roleid == 11)
                {
                    investments = _context.Investments.Where(x => x.Approver11hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
                else if (roleid == 12)
                {
                    investments = _context.Investments.Where(x => x.Approver12hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
                else if (roleid == 13)
                {
                    investments = _context.Investments.Where(x => x.Approver13hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
                else if (roleid == 14)
                {
                    investments = _context.Investments.Where(x => x.Approver14hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
                else if (roleid == 15)
                {
                    investments = _context.Investments.Where(x => x.Approver15hasReviewed == true & x.StatusId != 1 & x.PlantId == plantId && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
            }

            investments.ForEach(_investments =>
            {
                _context.Entry(_investments).Reference(_investments => _investments.ProjectType).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Country).Load();
                _context.Entry(_investments).Reference(_investments => _investments.DocType).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Status).Load();
                _context.Entry(_investments).Reference(_investments => _investments.ProcessLevel).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Plant).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Requester).Load();
            });

            return investments;
        }

        public async Task<List<Investments>> GetInvestmentsHistoryActualYear(int roleid)
        {
            var investments = new List<Investments>();

            investments = _context.Investments.Where(x => x.StatusId != 1).ToList();

            foreach (var item in investments)
            {
                if (roleid == 3)
                {
                    investments = _context.Investments.Where(x => x.Approver3hasReviewed == true & x.StatusId != 1 && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
                else if (roleid == 4)
                {
                    investments = _context.Investments.Where(x => x.Approver4hasReviewed == true & x.StatusId != 1 && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
                else if (roleid == 5)
                {
                    investments = _context.Investments.Where(x => x.Approver5hasReviewed == true & x.StatusId != 1 && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
                else if (roleid == 6)
                {
                    investments = _context.Investments.Where(x => x.Approver6hasReviewed == true & x.StatusId != 1 && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
                else if (roleid == 7)
                {
                    investments = _context.Investments.Where(x => x.Approver7hasReviewed == true & x.StatusId != 1 && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
                else if (roleid == 8)
                {
                    investments = _context.Investments.Where(x => x.Approver8hasReviewed == true & x.StatusId != 1 && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
                else if (roleid == 9)
                {
                    investments = _context.Investments.Where(x => x.Approver9hasReviewed == true & x.StatusId != 1 && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
                else if (roleid == 10)
                {
                    investments = _context.Investments.Where(x => x.Approver10hasReviewed == true & x.StatusId != 1 && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
                else if (roleid == 11)
                {
                    investments = _context.Investments.Where(x => x.Approver11hasReviewed == true & x.StatusId != 1 && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
                else if (roleid == 12)
                {
                    investments = _context.Investments.Where(x => x.Approver12hasReviewed == true & x.StatusId != 1 && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
                else if (roleid == 13)
                {
                    investments = _context.Investments.Where(x => x.Approver13hasReviewed == true & x.StatusId != 1 && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
                else if (roleid == 14)
                {
                    investments = _context.Investments.Where(x => x.Approver14hasReviewed == true & x.StatusId != 1 && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
                else if (roleid == 15)
                {
                    investments = _context.Investments.Where(x => x.Approver15hasReviewed == true & x.StatusId != 1 && x.IssueDate.Value.Year == DateTime.Now.Year).ToList();
                }
            }

            investments.ForEach(_investments =>
            {
                _context.Entry(_investments).Reference(_investments => _investments.ProjectType).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Country).Load();
                _context.Entry(_investments).Reference(_investments => _investments.DocType).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Status).Load();
                _context.Entry(_investments).Reference(_investments => _investments.ProcessLevel).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Plant).Load();
                _context.Entry(_investments).Reference(_investments => _investments.Requester).Load();
            });

            return investments;
        }
    }
}
