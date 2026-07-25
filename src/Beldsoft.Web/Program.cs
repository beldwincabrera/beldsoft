using Beldsoft.Application.Interfaces;
using Beldsoft.Application.Services;
using Beldsoft.Web.Components;
using Microsoft.AspNetCore.ResponseCompression;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddHttpContextAccessor();

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
    options.Level = System.IO.Compression.CompressionLevel.Fastest);

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
builder.Services.AddScoped<IContactService, ContactService>();

var app = builder.Build();

app.Use(async (context, next) =>
{
    context.Response.OnStarting(() =>
    {
        context.Response.Headers.XContentTypeOptions = "nosniff";
        context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
        context.Response.Headers["Permissions-Policy"] = "camera=(), microphone=(), geolocation=()";
        context.Response.Headers["X-Frame-Options"] = "SAMEORIGIN";
        return Task.CompletedTask;
    });

    await next();
});

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseResponseCompression();

if (!app.Environment.IsDevelopment())
{
    app.Use(async (context, next) =>
    {
        var requestPath = context.Request.Path.Value;
        var isVersionableAsset =
            !string.IsNullOrEmpty(requestPath) &&
            Path.HasExtension(requestPath) &&
            !requestPath.EndsWith(".html", StringComparison.OrdinalIgnoreCase);

        if (isVersionableAsset)
        {
            context.Response.OnStarting(() =>
            {
                if (context.Response.StatusCode == StatusCodes.Status200OK)
                {
                    const int sevenDaysInSeconds = 60 * 60 * 24 * 7;
                    context.Response.Headers.CacheControl =
                        $"public,max-age={sevenDaysInSeconds}";
                }

                return Task.CompletedTask;
            });
        }

        await next();
    });
}

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = context =>
    {
        if (!app.Environment.IsDevelopment())
        {
            const int sevenDaysInSeconds = 60 * 60 * 24 * 7;
            context.Context.Response.Headers.CacheControl =
                $"public,max-age={sevenDaysInSeconds}";
        }
    }
});
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
