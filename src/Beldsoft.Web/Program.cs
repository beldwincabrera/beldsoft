using Beldsoft.Application.Interfaces;
using Beldsoft.Application.Services;
using Beldsoft.Web.Components;
using Beldsoft.Web.Services;
using Microsoft.Extensions.Options;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = MudBlazor.Defaults.Classes.Position.BottomRight;
    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = true;
});

// Read-only singleton services
builder.Services.AddSingleton<IBlogService, BlogService>();
builder.Services.AddSingleton<ITeamService, TeamService>();
builder.Services.AddSingleton<IServiceService, ServiceService>();
builder.Services.AddSingleton<IProjectService, ProjectService>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<ITestimonialService, TestimonialService>();
builder.Services.AddSingleton<IPricingService, PricingService>();
builder.Services.AddSingleton<IFaqService, FaqService>();

// Scoped services (per SignalR circuit = per user session)
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddOptions<LeadOptions>()
    .Bind(builder.Configuration.GetSection(LeadOptions.SectionName))
    .Validate(
        options => !string.IsNullOrWhiteSpace(options.StorageDirectory),
        "Leads:StorageDirectory must identify a durable Lead storage folder.")
    .Validate(
        options => !string.IsNullOrWhiteSpace(options.LocalEmailDropDirectory),
        "Leads:LocalEmailDropDirectory must identify a non-production email drop folder.")
    .Validate(options => options.MaximumSubmissionsPerHour > 0, "Leads:MaximumSubmissionsPerHour must be positive.")
    .Validate(options => options.DuplicateWindowMinutes >= 0, "Leads:DuplicateWindowMinutes cannot be negative.")
    .Validate(
        options => options.Graph.TimeoutSeconds is >= 5 and <= 120,
        "Leads:Graph:TimeoutSeconds must be between 5 and 120.")
    .Validate(
        options => !builder.Environment.IsProduction() || options.Graph.IsConfigured,
        "Production Lead notifications require Leads:Graph:TenantId, ClientId, ClientSecret, and SenderUserId.")
    .Validate(
        options => options.HasValidRecipientAddress,
        "Leads:RecipientAddress must be a valid email address.")
    .ValidateOnStart();

if (builder.Environment.IsProduction())
{
    builder.Services.AddHttpClient(
        GraphLeadNotificationSender.HttpClientName,
        client =>
        {
            client.BaseAddress = new Uri("https://graph.microsoft.com/v1.0/");
            client.Timeout = Timeout.InfiniteTimeSpan;
        });
    builder.Services.AddSingleton<ILeadNotificationSender, GraphLeadNotificationSender>();
}
else
{
    builder.Services.AddSingleton<ILeadNotificationSender, LocalEmailDropNotificationSender>();
}

builder.Services.AddSingleton<ILeadService, LeadService>();

var app = builder.Build();

_ = app.Services.GetRequiredService<IOptions<LeadOptions>>().Value;
var leadNotificationSender = app.Services.GetRequiredService<ILeadNotificationSender>();
if (leadNotificationSender is LocalEmailDropNotificationSender localEmailDrop)
{
    app.Logger.LogInformation(
        "Non-production Lead email notifications will be written to {DropDirectory}.",
        localEmailDrop.DropDirectory);
}
else
{
    app.Logger.LogInformation(
        "Production Lead email notifications are configured for Microsoft Graph.");
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
