using InvestmentApprovalTool.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InvestmentApprovalTool.Pages
{
    public class CascadingDropdownComponentBase : ComponentBase
    {
        [Inject] public IDropdownService DropdownService { get; set; }
        protected CascadingDropdownViewModel CascadingVM { get; set; } = new CascadingDropdownViewModel();

        protected override void OnInitialized()
        {
            CascadingVM.ListofCountries = DropdownService.ListofCountries();
            CascadingVM.CountryId = "";
            CascadingVM.ListofPlants = DropdownService.ListofAllPlants();
            {
                new SelectListItem()
                {
                    Text = "Select",
                    Value = ""
                };
            };

            CascadingVM.PlantId = "";

            CascadingVM.ListofProgramManagers = DropdownService.ListofAllProgramManagers();
            {
                new SelectListItem()
                {
                    Text = "Select",
                    Value = ""
                };
            };

            CascadingVM.ProgramManagerId = "";
        }

        public void OnCountryChange(string value)
        {
            if (value != null)
            {
                CascadingVM.CountryId = value.ToString();
                CascadingVM.PlantId = "";
                CascadingVM.ListofPlants = DropdownService.ListofPlants(Convert.ToInt32(CascadingVM.CountryId));
                this.StateHasChanged();
            }
        }

        protected void OnPlantChange(string value)
        {
            if (value != null)
            {
                CascadingVM.PlantId = value.ToString();
            }
        }

        protected void OnProgramManagerChange(string value)
        {
            if (value != null)
            {
                CascadingVM.ProgramManagerId = value.ToString();
            }
        }

        protected async void FormSubmitted()
        {
            var selectedCountry = CascadingVM.CountryId;
            var selectedState = CascadingVM.PlantId;
            var selectedProgramManager = CascadingVM.ProgramManagerId;
        }
    }
}
