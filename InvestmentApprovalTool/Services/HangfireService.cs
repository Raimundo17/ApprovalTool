using InvestmentApprovalTool.Data;

namespace InvestmentApprovalTool.Services
{
    public interface IHangfireService
    {
        public Task FireEmail();
    }

    public class HangfireService : IHangfireService
    {
        private readonly IInvestmentService _investment;

        private readonly IUserService _user;
        private readonly IMailService _mail;

        public HangfireService(IInvestmentService investment, IUserService user, IMailService mail)
        {
            _investment = investment;
            _user = user;
            _mail = mail;
        }

        public async Task FireEmail()
        {
            List<string> emailList = new List<string>();

            try
            {
                foreach (var investments in _investment.GetAll().Where(x => x.StatusId == 2))
                {
                    // Se o investment está no nível 3 ( PLANT ME) e o aprovador ainda não aprovou vai para a lista
                    if (investments.Approver3hasReviewed == false & investments.ProcessLevelId == 3)
                    {
                        if (investments.CountryId == 1 | investments.CountryId == 6) // Portugal
                        {
                            // Traz o aprovador por fabrica
                            var approver = await _user.GetUserByLevelAndPlant(3, investments.PlantId);
                            emailList.Add(approver.Email);
                        }
                        else
                        {
                            // Traz o aprovador por país
                            var approver = await _user.GetUserByLevelAndCountry(3, investments.CountryId);
                            emailList.Add(approver.Email);
                        }
                    }
                    // CLUSTER ME
                    else if (investments.Approver4hasReviewed == false & investments.ProcessLevelId == 4)
                    {
                        var approver = await _user.GetUserByLevelAndCountry(4, investments.CountryId);
                        emailList.Add(approver.Email);
                    }
                    // PLANT FINANCE
                    else if (investments.Approver5hasReviewed == false & investments.ProcessLevelId == 5)
                    {
                        if(investments.CountryId == 7) // Poland
                        {
                            var approver = await _user.GetUserByLevelAndPlant(5, investments.PlantId);
                            emailList.Add(approver.Email);
                        }
                        else
                        {
                            var approver = await _user.GetUserByLevelAndCountry(5, investments.CountryId);
                            emailList.Add(approver.Email);
                        }

                    }
                    // PLANT GENERAL MANAGEMENT
                    else if (investments.Approver6hasReviewed == false & investments.ProcessLevelId == 6)
                    {
                        if(investments.CountryId == 7) // Poland
                        {
                            var approver = await _user.GetUserByLevelAndPlant(6, investments.PlantId);
                            emailList.Add(approver.Email);

                        }
                        else
                        {
                            var approver = await _user.GetUserByLevelAndCountry(6, investments.CountryId);
                            emailList.Add(approver.Email);
                        }

                    }
                    // MEC CAPITAL TEAM
                    else if (investments.Approver7hasReviewed == false & investments.ProcessLevelId == 7)
                    {
                        List<User> approver7list = new List<User>();
                        approver7list = await _user.GetLevelByIdList(7);
                        foreach (var email7 in approver7list)
                        {
                            string seven = email7.Email;
                            emailList.Add(seven);
                        }
                    }
                    // PROGRAM MANAGER
                    else if (investments.Approver8hasReviewed == false & investments.ProcessLevelId == 8)
                    {
                        var approver = await _user.GetbyId(investments.ProgramManagerId.Value);
                        emailList.Add(approver.Email);
                    }
                    // CAPITAL MANAGER
                    else if (investments.Approver9hasReviewed == false & investments.ProcessLevelId == 9)
                    {
                        var approver = await _user.GetUserByLevel(9);
                        emailList.Add(approver.Email);
                    }
                    // ADVANCE MANAGER
                    else if (investments.Approver10hasReviewed == false & investments.ProcessLevelId == 10)
                    {
                        var approver = await _user.GetUserByLevel(10);
                        emailList.Add(approver.Email);
                    }
                    // ME DIRECTOR
                    else if (investments.Approver11hasReviewed == false & investments.ProcessLevelId == 11)
                    {
                        var approver = await _user.GetUserByLevel(11);
                        emailList.Add(approver.Email);
                    }
                    // KRAKOW FINANCE LEADER
                    else if (investments.Approver12hasReviewed == false & investments.ProcessLevelId == 12)
                    {
                        var approver = await _user.GetUserByLevel(12);
                        emailList.Add(approver.Email);
                    }
                    // REGIONAL PBU FINANCE DIRECTOR
                    else if (investments.Approver13hasReviewed == false & investments.ProcessLevelId == 13)
                    {
                        var approver = await _user.GetUserByLevel(13);
                        emailList.Add(approver.Email);
                    }
                    // REGIONAL PBU MANAGING DIRECTOR
                    else if (investments.Approver14hasReviewed == false & investments.ProcessLevelId == 14)
                    {
                        var approver = await _user.GetUserByLevel(14);
                        emailList.Add(approver.Email);
                    }
                    // KRAKOW FINANCE TEAM
                    else if (investments.Approver15hasReviewed == false & investments.ProcessLevelId == 15)
                    {
                        var approver = await _user.GetUserByLevel(15);
                        emailList.Add(approver.Email);
                    }

                    if (emailList.Count != 0)
                    {
                        await _mail.SendEmailAsync(new MailRequest
                        {
                            ToEmail = emailList,
                            Subject = "Investment Approval Tool Requisition reminder with IR/DR Number:" + " " + investments.IR_DRNumber,
                            Body = @"<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml' xmlns:o='urn:schemas-microsoft-com:office:office'><head><!--[if gte mso 9]><xml>
                <o:OfficeDocumentSettings>
                    <o:AllowPNG/>
                    <o:PixelsPerInch>96</o:PixelsPerInch>
                </o:OfficeDocumentSettings>
            </xml><![endif]-->
            <meta http-equiv='Content-Type' content='text/html; charset=utf-8'><title>POS History Matrix Tool</title>

                <meta http-equiv='X-UA-Compatible' content='IE=edge'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0 '>
                <meta name='format-detection' content='telephone=no'>
                <style type='text/css'>
                    body {
                        margin: 0;
                        padding: 0;
                        -webkit-text-size-adjust: 100% !important;
                        -ms-text-size-adjust: 100% !important;
                        -webkit-font-smoothing: antialiased !important;
                        font-family:Arial, sans-serif !important;
                    }

                    img {
                        border: 0 !important;
                        outline: none !important;
                    }

                    p {
                        Margin: 0px !important;
                        Padding: 0px !important;
                    }

                    table {
                        border - collapse: collapse;
                        mso-table-lspace: 0px;
                        mso-table-rspace: 0px;
                    }

                    td,
                    a,
                    span {
                        border - collapse: collapse;
                        mso-line-height-rule: exactly;
                    }

                    .ExternalClass * {
                        line - height: 100%;
                    }

                    .em_gray a {
                        color: #979797;
                        text-decoration: none;
                    }

                    .em_black a {
                        color: #171717;
                        text-decoration: none;
                    }

                    .em_white a {
                        color: #ffffff;
                        text-decoration: none;
                    }

                    @media only screen and (min-width:481px) and (max-width:599px) {
                        .em_main_table {
                            width: 100% !important;
                        }
                        .em_wrapper {
                            width: 100% !important;
                        }
                        .em_hide {
                            display: none !important;
                        }
                        .em_full_img {
                            width: 100% !important;
                            height: auto !important;
                        }
                        .em_full_img img {
                            width: 100% !important;
                            height: auto !important;
                        }
                        .em_side15 {
                            width: 15px !important;
                        }
                        .em_aside15 {
                            padding-left: 15px !important;
                            padding-right: 15px !important;
                        }
                        .em_ptop {
                            padding-top: 20px !important;
                        }
                        .em_pbottom {
                            padding-bottom: 20px !important;
                        }
                        .em_h20 {
                            height: 20px !important;
                            font-size: 1px !important;
                            line-height: 1px !important;
                        }
                    }

                    @media screen and (max-width: 480px) {
                        .em_main_table {
                            width: 100% !important;
                        }
                        .em_wrapper {
                            width: 100% !important;
                        }
                        .em_hide {
                            display: none !important;
                        }
                        .em_full_img {
                            width: 100% !important;
                            height: auto !important;
                        }
                        .em_full_img img {
                            width: 100% !important;
                            height: auto !important;
                        }
                        .em_side15 {
                            width: 15px !important;
                        }
                        .em_aside15 {
                            padding-left: 15px !important;
                            padding-right: 15px !important;
                        }
                        .em_ptop {
                            padding-top: 20px !important;
                        }
                        .em_pbottom {
                            padding-bottom: 20px !important;
                        }
                        .em_h20 {
                            height: 20px !important;
                            font-size: 1px!important;
                            line-height: 1px!important;
                        }
                        u + .em_body .em_full_wrap {
                            width: 100% !important;
                            width: 100vw !important;
                        }
                        .em_font_16 {
                            font-size: 15px !important;
                        }
                    }
                  /* froala table styling support */
                    table.table-with-border{
                      border: 1px solid #ccc;
                      border-collapse:collapse;
                    }
                    table.table-with-border tr{
                      border: 1px solid #ccc;
                    }
                    table.table-with-border td{
                      border: 1px solid #ccc;
                    }
                    table.table-with-alternate-rows tr:nth-child(even){
                      background: #CCC
                    }
                </style>
            </head>

            <!-- HIDDEN PREHEADER TEXT FOR OUTLOOK -->
            <body>
            <div style='display: none; mso-hide: all; width: 0px; height: 0px; max-width: 0px; max-height: 0px; font-size: 0px; line-height: 0px; overflow:hidden;'>
                    This is a reminder that an investment requires your Review / Approval " + "on" + investments.IssueDate + "with" +
            "Investment Number: " + investments.Id + "<br> Project: " + investments.ProjectTitle_DisposalDescription +
            "<br> Program Manager " + investments.ProgramManager +
            @" Click <a href='xxxxxxxxxxxxxx'>HERE</a> to view the request.
            </div>


                <!-- == Header Section == -->
                <table width='100%' border='0' cellspacing='0' cellpadding='0' class='em_full_wrap' bgcolor='#f5f5f5'>
                    <tr>
                        <td align='center' valign='top'>
                            <table align='center' width='600' border='0' cellspacing='0' cellpadding='0' class='em_main_table' style='width:600px; table-layout:fixed;' bgcolor='#1d232f'>
                                <tr>
                                    <td align='center' valign='top' style='padding:13px 15px 13px 20px;' class='em_aside15'>
                                        <table border='0' cellspacing='0' cellpadding='0' align='left'>
                                            <tr>

                                                <td align='center' valign='top'>
                                                    <img src='https://assets.socialchorus.com/production/2531/program_header_images/4733771b-0f8d-46a3-a0a2-4605b904b4ff.png' width='109' alt='Goxxxxxxx' border='0' style='display:block;max-width:109px;color:#ffffff;font-family:Arial, sans-serif;font-size:15px;line-height:20px;font-weight:bold;'>
                                                </td>

                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <!-- == //Header Section == -->
                <!-- == Body Section == -->
                <div class='js-editable-block'>

                    <table width='100%' border='0' cellspacing='0' cellpadding='0' class='em_full_wrap' bgcolor='#f5f5f5'>
                        <tr>
                            <td align='center' valign='top'>
                                <table align='center' width='600' border='0' cellspacing='0' cellpadding='0' class='em_main_table' style='width:600px; table-layout:fixed;' bgcolor='#ffffff'>
                                    <tr>
                                        <td align='center' valign='top'>
                                            <table width='600' border='0' cellspacing='0' cellpadding='0' align='center' style='width:600px;' class='em_wrapper'>
                                                <tr>
                                                    <td width='30' style='width:30px;' class='em_side15'>&nbsp;</td>
                                                    <td align='center' valign='top'>
                                                        <table width='540' border='0' cellspacing='0' cellpadding='0' align='center' style='width:540px;' class='em_wrapper'>
                                                            <tr>
                                                                <td height='28' style='height:28px;' class='em_h20'>&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td align='center' class='em_black' style='color:#171717;font-family:Helvetica, Arial, sans-serif;font-size:18px;line-height:24px; text-align: left'>

                                                         Dear xxxxxxx Investment Tool User,<br> <br>
                    This is a reminder that an investment requires your Review / Approval. <br>
                    <br>
                    " + "<br> Investment Ar: " + investments.ArNumber + " with IR Number: " + investments.IR_DRNumber + " <br>" +
                                                      "<br> Comments:" + investments.Comments + "<br> <br> ACTION REQUIRED: <br> <br> Please log in to the xxxxxx Investment Tool to review / approve / reject the request. " + @"
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width='30' style='width:30px;' class='em_side15'>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width='100%' border='0' cellspacing='0' cellpadding='0' class='em_full_wrap' bgcolor='#f5f5f5'>
                        <tr>
                            <td align='center' valign='top'>
                                <table align='center' width='600' border='0' cellspacing='0' cellpadding='0' class='em_main_table' style='width:600px; table-layout:fixed;' bgcolor='#ffffff'>
                                    <!-- Banner section -->
                                    <tr>
                                        <td align='center' valign='top'></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table width='100%' border='0' cellspacing='0' cellpadding='0' class='em_full_wrap' bgcolor='#f5f5f5'>
                        <tr>
                            <td align='center' valign='top'>
                                <table align='center' width='600' border='0' cellspacing='0' cellpadding='0' class='em_main_table' style='width:600px; table-layout:fixed;' bgcolor='#ffffff'>
                                    <!-- //Banner section -->
                                    <!-- Title Text and Cta section -->
                                        <tr>
                                            <td align='center' valign='top'>
                                                <table width='600' border='0' cellspacing='0' cellpadding='0' align='center' style='width:600px;' class='em_wrapper'>
                                                    <tr>
                                                        <td width='30' style='width:30px;' class='em_side15'>&nbsp;</td>
                                                        <td align='center' valign='top'>
                                                            <table width='520' border='0' cellspacing='0' cellpadding='0' align='center' style='width:520px;' class='em_wrapper'>
                                                                <tr>
                                                                    <td height='30' style='height:30px;' class='em_h20'>&nbsp;</td>
                                                                </tr>


                                                                <tr>
                                                                    <td align='center' valign='top' style='padding-bottom:25px;' class='em_pbottom'>
                                                                        <table width='180' border='0' cellspacing='0' cellpadding='0' align='center' style='width:180px;border-radius:2px;' bgcolor='#448aff'>
                                                                            <tr>
                                                                                <td align='center' class='em_white' height='40' style='color:#FFFFFF;
                                                                font-family:Helvetica, Arial, sans-serif;font-size:14px;font-weight:bold;text-transform:uppercase;height:40px;'>
                                                                                    <a href='http://xxxxxxx' target='_blank' style='color:#FFFFFF;
                                                                   text-decoration:none;display:block;line-height:40px;' clicktracking='off'>

                                                                        Go to Website

                                                                </a>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td width='50' style='width:50px;' class='em_side15'>&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </a>
                                    <!-- Title Text and Cta section -->
                                </table>
                            </td>
                        </tr>
                    </table>

                </div>
                <!-- == //Body Section == -->
                <!-- == Footer Section == -->
                <table width='100%' border='0' cellspacing='0' cellpadding='0' class='em_full_wrap' bgcolor='#f5f5f5'>
                    <tr>
                        <td align='center' valign='top'>
                            <table align='center' width='600' border='0' cellspacing='0' cellpadding='0' class='em_main_table' style='width:600px; table-layout:fixed;'>
                                <tr>
                                    <td align='center' class='em_ptop em_pbottom em_gray em_font_16' style='color:#979797;font-family:Helvetica, Arial, sans-serif;font-size:16px;line-height:20px;padding:50px 10px;'>
            </td>
                                </tr>
                                <tr>
                                    <td class='em_hide' style='line-height:1px;min-width:600px;background-color:#f5f5f5;'><img alt='' src='images/spacer.gif' height='1' width='600' style='max-height:1px; min-height:1px; display:block; width:600px; min-width:600px;' border='0'></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <!-- == //Footer Section == -->


            </body>
            </html>"
                        });

                        emailList.Clear();
                    }
                }
            }

            catch (Exception)
            {
                return;
            }


        }
    }
}
