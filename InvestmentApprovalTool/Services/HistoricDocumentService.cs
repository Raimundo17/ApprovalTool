namespace InvestmentApprovalTool.Services
{
    using InvestmentApprovalTool.Data;
    using PdfSharp.Drawing;
    using PdfSharp.Pdf;

    public interface IHistoricDocumentService
    {
        public string CreateHistoricDocument(Investments investments, InvestmentsHistory investmentshistory);
    }

    public class HistoricDocumentService : IHistoricDocumentService
    {
        public string CreateHistoricDocument(Investments investments, InvestmentsHistory investmentshistory)
        {
            // Quando o flow chegar ao fim gera o ficheiro de histórico
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont font = new XFont("Verdana", 8, XFontStyle.Bold);

            // Investment information
            gfx.DrawString(investments.IR_DRNumber, font, XBrushes.Black,
            new XRect(0, -400, page.Width, page.Height),
            XStringFormat.Center);
            gfx.DrawString(investments.ProjectTitle_DisposalDescription, font, XBrushes.Black,
    new XRect(0, -390, page.Width, page.Height),
    XStringFormat.Center);

            // --- Requester ---
            investmentshistory.ProcesslevelDate2 = investments.IssueDate;
            investmentshistory.Approval2role = "Requester";
            investmentshistory.Approval2 = investments.Requester.Name;

            gfx.DrawString(investmentshistory.ProcesslevelDate2.ToString(), font, XBrushes.Black,
            new XRect(0, -370, page.Width, page.Height),
            XStringFormat.Center);
            gfx.DrawString(investmentshistory.Approval2role, font, XBrushes.Black,
            new XRect(0, -360, page.Width, page.Height),
            XStringFormat.Center);
            gfx.DrawString(investmentshistory.Approval2, font, XBrushes.Black,
    new XRect(0, -350, page.Width, page.Height),
    XStringFormat.Center);

            // --- Approver 3 ---
            if (investmentshistory.ProcesslevelDate3 == null | investmentshistory.Approval3 == null | investmentshistory.Approval3role == null)
            {
                investmentshistory.ProcesslevelDate3 = investments.IssueDate;
                investmentshistory.Approval3 = "N/A";
                investmentshistory.Approval3role = "N/A";

                gfx.DrawString(investmentshistory.ProcesslevelDate3.ToString(), font, XBrushes.Black,
        new XRect(0, -330, page.Width, page.Height),
        XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval3role, font, XBrushes.Black,
        new XRect(0, -320, page.Width, page.Height),
        XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval3, font, XBrushes.Black,
            new XRect(0, -310, page.Width, page.Height),
            XStringFormat.Center);
            }
            else
            {
                gfx.DrawString(investmentshistory.ProcesslevelDate3.ToString(), font, XBrushes.Black,
new XRect(0, -330, page.Width, page.Height),
XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval3role, font, XBrushes.Black,
        new XRect(0, -320, page.Width, page.Height),
        XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval3, font, XBrushes.Black,
            new XRect(0, -310, page.Width, page.Height),
            XStringFormat.Center);
            }

            // --- Approver 4 ---
            if (investmentshistory.ProcesslevelDate4 == null | investmentshistory.Approval4 == null | investmentshistory.Approval4role == null)
            {
                investmentshistory.ProcesslevelDate4 = investments.IssueDate;
                investmentshistory.Approval4 = "N/A";
                investmentshistory.Approval4role = "N/A";

                gfx.DrawString(investmentshistory.ProcesslevelDate4.ToString(), font, XBrushes.Black,
        new XRect(0, -290, page.Width, page.Height),
        XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval4role, font, XBrushes.Black,
            new XRect(0, -280, page.Width, page.Height),
            XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval4, font, XBrushes.Black,
            new XRect(0, -270, page.Width, page.Height),
            XStringFormat.Center);
            }
            else
            {
                gfx.DrawString(investmentshistory.ProcesslevelDate4.ToString(), font, XBrushes.Black,
        new XRect(0, -290, page.Width, page.Height),
        XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval4role, font, XBrushes.Black,
            new XRect(0, -280, page.Width, page.Height),
            XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval4, font, XBrushes.Black,
            new XRect(0, -270, page.Width, page.Height),
            XStringFormat.Center);
            }

            // --- Approver 5 ---
            if (investmentshistory.ProcesslevelDate5 == null | investmentshistory.Approval5 == null | investmentshistory.Approval5role == null)
            {
                investmentshistory.ProcesslevelDate5 = investments.IssueDate;
                investmentshistory.Approval5 = "N/A";
                investmentshistory.Approval5role = "N/A";

                gfx.DrawString(investmentshistory.ProcesslevelDate5.ToString(), font, XBrushes.Black,
        new XRect(0, -250, page.Width, page.Height),
        XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval5role, font, XBrushes.Black,
            new XRect(0, -240, page.Width, page.Height),
            XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval5, font, XBrushes.Black,
            new XRect(0, -230, page.Width, page.Height),
            XStringFormat.Center);
            }
            else
            {
                gfx.DrawString(investmentshistory.ProcesslevelDate5.ToString(), font, XBrushes.Black,
new XRect(0, -250, page.Width, page.Height),
XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval5role, font, XBrushes.Black,
            new XRect(0, -240, page.Width, page.Height),
            XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval5, font, XBrushes.Black,
            new XRect(0, -230, page.Width, page.Height),
            XStringFormat.Center);
            }

            // --- Approver 6 ---
            if (investmentshistory.ProcesslevelDate6 == null | investmentshistory.Approval6 == null | investmentshistory.Approval6role == null)
            {
                investmentshistory.ProcesslevelDate6 = investments.IssueDate;
                investmentshistory.Approval6 = "N/A";
                investmentshistory.Approval6role = "N/A";

                gfx.DrawString(investmentshistory.ProcesslevelDate6.ToString(), font, XBrushes.Black,
    new XRect(1, -210, page.Width, page.Height),
    XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval6role, font, XBrushes.Black,
            new XRect(0, -200, page.Width, page.Height),
            XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval6, font, XBrushes.Black,
        new XRect(0, -190, page.Width, page.Height),
        XStringFormat.Center);
            }
            else
            {

                gfx.DrawString(investmentshistory.ProcesslevelDate6.ToString(), font, XBrushes.Black,
    new XRect(1, -210, page.Width, page.Height),
    XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval6role, font, XBrushes.Black,
            new XRect(0, -200, page.Width, page.Height),
            XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval6, font, XBrushes.Black,
        new XRect(0, -190, page.Width, page.Height),
        XStringFormat.Center);
            }

            // --- Approver 7 ---
            gfx.DrawString(investmentshistory.ProcesslevelDate7.ToString(), font, XBrushes.Black,
        new XRect(0, -170, page.Width, page.Height),
        XStringFormat.Center);
            gfx.DrawString(investmentshistory.Approval7role, font, XBrushes.Black,
    new XRect(0, -160, page.Width, page.Height),
    XStringFormat.Center);
            gfx.DrawString(investmentshistory.Approval7, font, XBrushes.Black,
        new XRect(0, -150, page.Width, page.Height),
        XStringFormat.Center);

            // --- Approver 8 ---
            if (investmentshistory.ProcesslevelDate8 == null | investmentshistory.Approval8 == null | investmentshistory.Approval8role == null)
            {
                investmentshistory.ProcesslevelDate8 = investments.IssueDate;
                investmentshistory.Approval8 = "N/A";
                investmentshistory.Approval8role = "N/A";

                gfx.DrawString(investmentshistory.ProcesslevelDate8.ToString(), font, XBrushes.Black,
      new XRect(0, -130, page.Width, page.Height),
      XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval8role, font, XBrushes.Black,
            new XRect(0, -120, page.Width, page.Height),
            XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval8, font, XBrushes.Black,
            new XRect(0, -110, page.Width, page.Height),
            XStringFormat.Center);
            }
            else
            {
                gfx.DrawString(investmentshistory.ProcesslevelDate8.ToString(), font, XBrushes.Black,
        new XRect(0, -130, page.Width, page.Height),
        XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval8role, font, XBrushes.Black,
            new XRect(0, -120, page.Width, page.Height),
            XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval8, font, XBrushes.Black,
            new XRect(0, -110, page.Width, page.Height),
            XStringFormat.Center);
            }

            //--- Approver 9 ---
            gfx.DrawString(investmentshistory.ProcesslevelDate9.ToString(), font, XBrushes.Black,
    new XRect(0, -90, page.Width, page.Height),
    XStringFormat.Center);
            gfx.DrawString(investmentshistory.Approval9role, font, XBrushes.Black,
        new XRect(0, -80, page.Width, page.Height),
        XStringFormat.Center);
            gfx.DrawString(investmentshistory.Approval9, font, XBrushes.Black,
        new XRect(0, -70, page.Width, page.Height),
        XStringFormat.Center);


            // --- Approver 10 ---
            gfx.DrawString(investmentshistory.ProcesslevelDate10.ToString(), font, XBrushes.Black,
    new XRect(0, -50, page.Width, page.Height),
    XStringFormat.Center);
            gfx.DrawString(investmentshistory.Approval10role, font, XBrushes.Black,
        new XRect(0, -40, page.Width, page.Height),
        XStringFormat.Center);
            gfx.DrawString(investmentshistory.Approval10, font, XBrushes.Black,
        new XRect(0, -30, page.Width, page.Height),
        XStringFormat.Center);

            // --- Approver 11 ---
            gfx.DrawString(investmentshistory.ProcesslevelDate11.ToString(), font, XBrushes.Black,
    new XRect(0, -10, page.Width, page.Height),
    XStringFormat.Center);
            gfx.DrawString(investmentshistory.Approval11role, font, XBrushes.Black,
        new XRect(0, 0, page.Width, page.Height),
        XStringFormat.Center);
            gfx.DrawString(investmentshistory.Approval11, font, XBrushes.Black,
        new XRect(0, 10, page.Width, page.Height),
        XStringFormat.Center);

            // --- Approver 12 ---
            gfx.DrawString(investmentshistory.ProcesslevelDate12.ToString(), font, XBrushes.Black,
    new XRect(0, 30, page.Width, page.Height),
    XStringFormat.Center);
            gfx.DrawString(investmentshistory.Approval12role, font, XBrushes.Black,
        new XRect(0, 40, page.Width, page.Height),
        XStringFormat.Center);
            gfx.DrawString(investmentshistory.Approval12, font, XBrushes.Black,
        new XRect(0, 50, page.Width, page.Height),
        XStringFormat.Center);


            // --- Approver 13 ---
            if (investmentshistory.ProcesslevelDate13 == null | investmentshistory.Approval13 == null | investmentshistory.Approval13role == null)
            {
                investmentshistory.ProcesslevelDate13 = investments.IssueDate;
                investmentshistory.Approval13 = "N/A";
                investmentshistory.Approval13role = "N/A";

                gfx.DrawString(investmentshistory.ProcesslevelDate13.ToString(), font, XBrushes.Black,
       new XRect(0, 70, page.Width, page.Height),
       XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval13role, font, XBrushes.Black,
            new XRect(0, 80, page.Width, page.Height),
            XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval13, font, XBrushes.Black,
            new XRect(0, 90, page.Width, page.Height),
            XStringFormat.Center);
            }
            else
            {
                gfx.DrawString(investmentshistory.ProcesslevelDate13.ToString(), font, XBrushes.Black,
        new XRect(0, 70, page.Width, page.Height),
        XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval13role, font, XBrushes.Black,
            new XRect(0, 80, page.Width, page.Height),
            XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval13, font, XBrushes.Black,
            new XRect(0, 90, page.Width, page.Height),
            XStringFormat.Center);
            }

            // --- Approver 14 ---
            if (investmentshistory.ProcesslevelDate14 == null | investmentshistory.Approval14 == null | investmentshistory.Approval14role == null)
            {
                investmentshistory.ProcesslevelDate14 = investments.IssueDate;
                investmentshistory.Approval14 = "N/A";
                investmentshistory.Approval14role = "N/A";

                gfx.DrawString(investmentshistory.ProcesslevelDate14.ToString(), font, XBrushes.Black,
       new XRect(0, 110, page.Width, page.Height),
       XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval14role, font, XBrushes.Black,
            new XRect(0, 120, page.Width, page.Height),
            XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval14, font, XBrushes.Black,
            new XRect(0, 130, page.Width, page.Height),
            XStringFormat.Center);
            }
            else
            {
                gfx.DrawString(investmentshistory.ProcesslevelDate14.ToString(), font, XBrushes.Black,
        new XRect(0, 110, page.Width, page.Height),
        XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval14role, font, XBrushes.Black,
            new XRect(0, 120, page.Width, page.Height),
            XStringFormat.Center);
                gfx.DrawString(investmentshistory.Approval14, font, XBrushes.Black,
            new XRect(0, 130, page.Width, page.Height),
            XStringFormat.Center);
            }

            // --- Approver 15 ---
            gfx.DrawString(investmentshistory.ProcesslevelDate15.ToString(), font, XBrushes.Black,
    new XRect(0, 150, page.Width, page.Height),
    XStringFormat.Center);
            gfx.DrawString(investmentshistory.Approval15role, font, XBrushes.Black,
        new XRect(0, 160, page.Width, page.Height),
        XStringFormat.Center);
            gfx.DrawString(investmentshistory.Approval15, font, XBrushes.Black,
        new XRect(0, 170, page.Width, page.Height),
        XStringFormat.Center);

            // Salva o historic document
            string fileName = "Historic_Of_Approvers_" + investments.RegNumber + ".pdf";

#if DEBUG
               string historyPath = @"C:\Projetos\InvestmentApprovalTool\InvestmentApprovalTool\wwwroot\Documentation\" + fileName;
#elif RELEASE
    string historyPath = @"E:\\xxxxxxx\xxxxxxx-Investments\" + fileName;
#else
            string historyPath = @"\\xxxxxxx\xxxxxxx\InvestmentApprovalTool_test\wwwroot\Documentation\" + fileName;
#endif
            document.Save(historyPath);

            page.Close();
            gfx.Dispose();
            document.Close();

            return historyPath;
        }
    }
}
