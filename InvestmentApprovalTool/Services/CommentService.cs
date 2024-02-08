namespace InvestmentApprovalTool.Services
{
    using InvestmentApprovalTool.Data;
    using InvestmentApprovalTool.Helpers;

    public interface ICommentService
    {
        Task<List<Comment>> GetAll();
        Task<Comment> GetById(int id);
        Task<Comment> Create(Comment comment);
        Task Update(Comment comment);
        Task Delete(Comment comment);
        Task<Comment> GetCommentByInvestmentId(int investmentid);
    }

    public class CommentService : ICommentService
    {
        private readonly DataContext _context;

        public CommentService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAll()
        {
            var comment = _context.Comments.ToList();
            return comment;
        }

        public async Task<Comment> GetCommentByInvestmentId(int investmentid)
        {
            var comment = _context.Comments.Where(x => x.InvestmentsId == investmentid)
                 .First();
            ;
            return comment;
        }

        public async Task<Comment> GetById(int id)
        {
            return getComment(id);
        }

        public async Task<Comment> Create(Comment comment)
        {
            // validate
            if (_context.Comments.Any(x => x.Id == comment.Id))
                throw new AppException("Comment with Id '" + comment.Id + "' already exists");

            _context.Comments.Add(comment);

            _context.Entry(comment).Reference(s => s.Investments).Load();
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task Update(Comment comment)
        {
            _context.Update(comment);
            _context.Entry(comment).Reference(s => s.Investments).Load();
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Comment comment)
        {
            if (_context.Comments.Any(x => x.Id == comment.Id))
                _context.Remove(comment);
            _context.SaveChanges();
        }

        private Comment getComment(int id)
        {
            var comment = _context.Comments.Find(id);
            if (comment == null) throw new KeyNotFoundException("Comment not found");
            return comment;
        }
    }
}