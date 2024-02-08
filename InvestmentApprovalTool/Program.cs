using Blazored.Modal;
using BlazorTable;
using Hangfire;
using InvestmentApprovalTool.Data;
using InvestmentApprovalTool.Helpers;
using InvestmentApprovalTool.Services;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

#if DEBUG
var connectionString = builder.Configuration.GetConnectionString("DebugConnection") ?? throw new InvalidOperationException("Connection string 'DebugConnection' not found.");
#elif DEVELOPMENT
var connectionString = builder.Configuration.GetConnectionString("DevelopmentConnection") ?? throw new InvalidOperationException("Connection string 'DevelopmentConnection' not found.");
#else
var connectionString = builder.Configuration.GetConnectionString("ReleasedConnection") ?? throw new InvalidOperationException("Connection string 'ReleasedConnection' not found.");
#endif

builder.Services.AddDbContextFactory<DataContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddHangfire(configuration => configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(builder.Configuration.GetConnectionString("ReleasedConnection"))
);

builder.Services.AddHangfireServer();
builder.Services.AddMvc();

builder.Services.AddBlazorTable();

builder.Services.AddCors();

#if DEBUG
var settings = builder.Configuration.GetSection("DebugMailSettings");
#else
var settings = builder.Configuration.GetSection("MailSettings");
#endif
builder.Services.Configure<MailSettings>(settings);
builder.Services.AddTransient<InvestmentApprovalTool.Services.IMailService, InvestmentApprovalTool.Services.MailService>();

// configure DI for application services
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IInvestmentService, InvestmentService>();
builder.Services.AddScoped<IPlantService, PlantService>();
builder.Services.AddScoped<IProjectTypeService, ProjectTypeService>();
builder.Services.AddScoped<IDocTypeService, DocTypeService>();
builder.Services.AddScoped<IFXRateService, FXRateService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IInvestmentHistoryService, InvestmentHistoryService>();
builder.Services.AddScoped<IFXRateService, FXRateService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IHistoricDocumentService, HistoricDocumentService>();
builder.Services.AddScoped<ISentMailService, SentMailService>();
builder.Services.AddTransient<IDropdownService, DropdownService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
builder.Services.AddAuthorization(o => o.FallbackPolicy = o.DefaultPolicy);
builder.Services.AddScoped<IHangfireService, HangfireService>();
builder.Services.AddBlazoredModal(); // Popup Window
builder.Services.AddBlazorBootstrap(); // BLAZOR BOOTSTRAP NUGET
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddCircuitOptions(e =>
{
    e.DetailedErrors = true;
});

var app = builder.Build();

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseStaticFiles();

#if RELEASE
string contentpath = @"E:\xxxxxxx\xxxxxxx-Investments";
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(contentpath),
    RequestPath = new PathString("/Documentation")
});
#endif

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// HANGFIRE
app.UseHangfireDashboard();
#if RELEASE || DEVELOPMENT
app.UseHangfireDashboard("/Fire/Events");
#endif

app.UseEndpoints(endpoints =>
{
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host");
    endpoints.MapHangfireDashboard();
});

app.Run();
