namespace InvestmentApprovalTool.Services
{
    using InvestmentApprovalTool.Data;
    using InvestmentApprovalTool.Helpers;
    using Microsoft.AspNetCore.Components;

    public interface IUserService
    {
        Task<List<User>> GetUsersOfPlant(int plantId);
        Task<List<User>> GetAll();
        Task<User> GetbyNetId(string netID);
        Task<User> GetbyId(int ID);
        Task<User> Create(User user);
        Task SetUserbyNetId(string netId);
        Task Update(int id, User model);
        Task SubmitUser(User model);
        Task<User> GetUserByLevel(int processLevel);
        Task<List<User>> GetLevelByIdList(int processLevel);
        Task<User> GetUserByLevelAndPlant(int processLevel, int plantId);
        Task<User> GetUserByLevelAndCountry(int processLevel, int countryId);
        Task<List<User>> GetUserByLevelAndCountryList(int processLevel, int countryId);
        Task<List<User>> GetUserByLevelAndPlantList(int processLevel, int plantId);
        Task<List<User>> GetUserByNetIdList(string netid);
        Task Delete(User user);
        Task DeleteById(int id);
    }

    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public User currentUser;

        public UserService(DataContext context)
        {
            _context = context;

        }

        // Get the user of the process level of where the investment is now and from a specific country
        public async Task<List<User>> GetUsersOfPlant(int plantId)
        {
            var users = _context.Users
            .Where(a => a.PlantId == plantId).ToList();

            return users;
        }

        public async Task<List<User>> GetUserByNetIdList(string netid)
        {
            var users = _context.Users
            .Where(a => a.NetId == netid & a.isActive == true).ToList();

            return users;
        }

        // Get All Users
        public async Task<List<User>> GetAll()
        {
            var usersList = _context.Users.OrderBy(z => z.RoleId).ToList();

            return usersList;
        }

        // Get the user of the level of the process level of where the investment is now
        public async Task<User> GetUserByLevel(int processLevel)
        {
            var user = _context.Users
            .Where(a => a.RoleId == processLevel & a.isActive == true)
            .First();
            return user;
        }

        // Get the user of the process level of where the investment is now and from a specific plant
        public async Task<User> GetUserByLevelAndPlant(int processLevel, int plantId)
        {
            var user = _context.Users
            .Where(a => a.RoleId == processLevel & a.PlantId == plantId & a.isActive == true).First();

            return user;
        }

        // Get the user of the process level of where the investment is now and from a specific country
        public async Task<User> GetUserByLevelAndCountry(int processLevel, int countryId)
        {
            var user = _context.Users
            .Where(a => a.RoleId == processLevel & a.CountryId == countryId & a.isActive == true).First();

            return user;
        }

        // Get the user of the process level of where the investment is now and from a specific country
        public async Task<List<User>> GetUserByLevelAndCountryList(int processLevel, int countryId)
        {
            var users = _context.Users
            .Where(a => a.RoleId == processLevel & a.CountryId == countryId & a.isActive == true).ToList();

            return users;
        }

        public async Task<List<User>> GetUserByLevelAndPlantList(int processLevel, int plantId)
        {
            var users = _context.Users
            .Where(a => a.RoleId == processLevel & a.PlantId == plantId & a.isActive == true).ToList();

            return users;
        }

        // Get the List of users of the level of the process level of where the investment is now
        public async Task<List<User>> GetLevelByIdList(int processLevel)
        {
            var userList = _context.Users
            .Where(a => a.RoleId == processLevel & a.isActive == true).ToList();

            return userList;
        }

        public async Task<User> GetbyNetId(string netId)
        {
            var user = _context.Users
                .Where(id => id.NetId.Contains(netId) & id.isActive == true)
                .FirstOrDefault();

            _context.Entry(user);
            return user;
        }

        public async Task SetUserbyNetId(string netId)
        {
            currentUser = _context.Users
                .Where(id => id.NetId.Contains(netId))
                .FirstOrDefault();

            _context.Entry(currentUser);
        }

        public async Task<User> GetbyId(int id)
        {
            return getUser(id);
        }

        private User getUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

        public async Task SubmitUser(User model)
        {
            try
            {
                // validate
                if (_context.Users.Any(x => x.Id == model.Id)) Update(model.Id, model);
                else Create(model);
            }
            catch (Exception)
            {
                throw new AppException("Error on processing Submit form");
            }
        }

        public async Task<User> Create(User user)
        {
            if (_context.Users.Any(x => x.Id == user.Id))
                throw new AppException("User with Id '" + user.Id + "' already exists");

            user.createdOn = DateTime.Now;
            user.createdBy = currentUser.Name;

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public async Task Update(int id, User model)
        {
            _context.Users.Update(model);
            _context.SaveChanges();
        }

        public async Task Delete(User user)
        {
            if (_context.Users.Any(x => x.Id == user.Id))
                _context.Remove(user);
            _context.SaveChanges();
        }

        public async Task DeleteById(int id)
        {
            var user = _context.Users.Find(id);
            
            if (user == null) throw new KeyNotFoundException("User not found");
            
            _context.Remove(user);
            
            _context.SaveChanges();
        }
    }
}
