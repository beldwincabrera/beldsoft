using Beldsoft.Application.Interfaces;
using Beldsoft.Application.Services;
using Beldsoft.Web.Components;
using Beldsoft.Web.Services;
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
builder.Services.Configure<ContactStorageOptions>(
    builder.Configuration.GetSection(ContactStorageOptions.SectionName));
builder.Services.AddSingleton<IContactService, FileContactService>();

var app = builder.Build();

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
