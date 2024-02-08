namespace InvestmentApprovalTool.Services
{
    using InvestmentApprovalTool.Data;
    using InvestmentApprovalTool.Helpers;
    using System.Diagnostics.Metrics;

    public interface IDocTypeService
    {
        Task<List<DocType>> GetAll();
        Task<DocType> GetById(int id);
        Task<DocType> Create(DocType docType);
        Task Update(DocType docType);
        Task Delete(DocType docType);
    }

    public class DocTypeService : IDocTypeService
    {
        private readonly DataContext _context;

        public DocTypeService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<DocType>> GetAll()
        {
            var doctype = _context.DocTypes.ToList();
            return doctype;
        }

        public async Task<DocType> GetById(int id)
        {
            return getDocType(id);
        }

        public async Task<DocType> Create(DocType docType)
        {
            // validate
            if (_context.DocTypes.Any(x => x.Id == docType.Id))
                throw new AppException("Doc Type with Id '" + docType.Id + "' already exists");

            _context.DocTypes.Add(docType);
            await _context.SaveChangesAsync();
            return docType;
        }

        public async Task Update(DocType docType)
        {
            _context.Update(docType);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(DocType docType)
        {
            if (_context.DocTypes.Any(x => x.Id == docType.Id))
                _context.Remove(docType);
            _context.SaveChanges();
        }

        private DocType getDocType(int id)
        {
            var docType = _context.DocTypes.Find(id);
            if (docType == null) throw new KeyNotFoundException("DocType not found");
            return docType;
        }
    }
}
