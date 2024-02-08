namespace InvestmentApprovalTool.Services
{
    using InvestmentApprovalTool.Data;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public interface IDropdownService
    {
        List<SelectListItem> ListofCountries();
        List<SelectListItem> ListofPlants(int countryId);
        List<SelectListItem> ListofAllPlants();
        List<SelectListItem> ListofAllProgramManagers();
    }

    public class DropdownService : IDropdownService
    {
        private readonly DataContext _context;

        public DropdownService(DataContext context)
        {
            _context = context;
        }

        public List<SelectListItem> ListofCountries()
        {
            try
            {
                var listofCountries = (from countries in _context.Countries.AsNoTracking()
                                       select new SelectListItem()
                                       {
                                           Text = countries.CountryName,
                                           Value = countries.Id.ToString()
                                       }
                    ).ToList();

                listofCountries.Insert(0, new SelectListItem()
                {
                    Value = "",
                    Text = "---Select---"
                });

                return listofCountries;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> ListofPlants(int countryId)
        {
            try
            {
                var listofplants = (from plants in _context.Plants.AsNoTracking()
                                    where plants.CountryId == countryId
                                    select new SelectListItem()
                                    {
                                        Text = plants.PlantName,
                                        Value = plants.Id.ToString()
                                    }
                    ).ToList();
                listofplants.Insert(0, new SelectListItem()
                {
                    Value = "",
                    Text = "---Select---"
                });
                return listofplants;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> ListofAllPlants()
        {
            try
            {
                var listofPlants = (from plants in _context.Plants.AsNoTracking()
                                       select new SelectListItem()
                                       {
                                           Text = plants.PlantName,
                                           Value = plants.Id.ToString()
                                       }
                    ).ToList();

                listofPlants.Insert(0, new SelectListItem()
                {
                    Value = "",
                    Text = "---Select---"
                });

                return listofPlants;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<SelectListItem> ListofAllProgramManagers()
        {
            try
            {
                var listofProgramManagers = (from programManagers in _context.Users.AsNoTracking().Where(x => x.RoleId == 8)
                                    select new SelectListItem()
                                    {
                                        Text = programManagers.Name,
                                        Value = programManagers.Id.ToString()
                                    }
                    ).ToList();

                listofProgramManagers.Insert(0, new SelectListItem()
                {
                    Value = "",
                    Text = "---Select---"
                });

                return listofProgramManagers;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
