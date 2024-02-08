namespace InvestmentApprovalTool.Services
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.ComponentModel.DataAnnotations;

    public class CascadingDropdownViewModel
    {
        [Required(ErrorMessage = "Country is Required")]
        public string CountryId { get; set; }
        public List<SelectListItem> ListofCountries { get; set; }

        [Required(ErrorMessage = "Plant is Required")]
        public string PlantId { get; set; }
        public List<SelectListItem> ListofPlants { get; set; }

        [Required(ErrorMessage = "Program Manager is Required")]
        public string ProgramManagerId { get; set; }
        public List<SelectListItem> ListofProgramManagers { get; set; }
    }
}
