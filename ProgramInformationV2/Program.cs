using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using ProgramInformationV2.Components;
using ProgramInformationV2.Data.Cache;
using ProgramInformationV2.Data.DataContext;
using ProgramInformationV2.Data.DataHelpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));
builder.Services.AddControllersWithViews()
    .AddMicrosoftIdentityUI();

builder.Services.AddAuthorization(options => {
    options.FallbackPolicy = options.DefaultPolicy;
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddWebOptimizer(pipeline => {
    pipeline.AddJavaScriptBundle("/js/site.js", "/wwwroot/js/*.js").UseContentRoot();
    pipeline.AddCssBundle("/css/site.css", "/wwwroot/css/*.css").UseContentRoot();
});

builder.Services.AddDbContextFactory<ProgramContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection")).EnableSensitiveDataLogging(true));
builder.Services.AddScoped<ProgramRepository>();
builder.Services.AddSingleton<CacheHolder>();
builder.Services.AddScoped<SourceHelper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.UseWebOptimizer();

app.Lifetime.ApplicationStarted.Register(() => {
    var factory = app.Services.GetService<IServiceScopeFactory>() ?? throw new NullReferenceException("service scope factory is null");
    using var serviceScope = factory.CreateScope();
    var context = serviceScope.ServiceProvider.GetRequiredService<ProgramContext>();
    _ = context.Database.EnsureCreated();
});

app.Run();
