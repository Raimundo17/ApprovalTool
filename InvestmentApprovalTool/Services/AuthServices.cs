namespace InvestmentApprovalTool.Services
{
  
    public interface IAuthService
    {
        Task<bool> isRegistred(string contextid);
        Task<bool> GetUserIsRegistred();
        Task<bool> GetUserIsAdmin();
    }

    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        public bool userIsRegistred;
        public bool userIsAdmin;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> isRegistred(string contextid)
        {
            if (_userService.GetbyNetId(contextid) != null)
            {
                userIsRegistred = true;
                return true;
            }
            else return false;
        }

        public async Task<bool> GetUserIsRegistred()
        {
            return userIsRegistred;
        }

        public async Task<bool> GetUserIsAdmin()
        {
            return userIsAdmin;
        }
    }
}
