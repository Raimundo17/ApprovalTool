namespace InvestmentApprovalTool.Services
{
    using InvestmentApprovalTool.Data;
    using System.Text;

    public class ExportCVS
    {
        protected List<Investments> investmentsList = new();

        public ExportCVS(InvestmentService investmentService)
        {
            investmentsList = investmentService.GetAll();

            String file = @"C:\Projetos\InvestmentApprovalTool\InvestmentApprovalTool\wwwroot\";

            String separator = ",";
            StringBuilder output = new StringBuilder();

            String[] headings = { "IssueDate", "ArNumber", "IR_DRNumber", "ProjectTitle_DisposalDescription", "IRValueEuro", "IRValueDollar", };
            output.AppendLine(string.Join(separator, headings));

            foreach (Investments investment in investmentsList)
            {
                String[] newLine = { investment.IssueDate.ToString(), investment.ArNumber, investment.IR_DRNumber, investment.ProjectTitle_DisposalDescription, investment.IRValueEuro.ToString(), investment.IRValueDollar.ToString() };
                output.AppendLine(string.Join(separator, newLine));
            }

            try
            {
                File.AppendAllText(file, output.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Data could not be written to the CSV file.");
                return;
            }

            Console.WriteLine("The data has been successfully saved to the CSV file");
        }
    }
}

