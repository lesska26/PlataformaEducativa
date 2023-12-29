using Microsoft.EntityFrameworkCore;
using PlataformaEducativa.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using PlataformaEducativa.Correo;
using PlataformaEducativa.Logica;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<PlataformaEducativaDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConectEducativa")));


var smtpSettings = builder.Configuration.GetSection("SmtpSettings").Get<SmtpSettings>();

builder.Services.AddSingleton<PlataformaEducativa.Correo.Smtp>(options => new PlataformaEducativa.Correo.Smtp(smtpSettings));
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(p =>
    {
        p.ExpireTimeSpan = TimeSpan.FromHours(1);
        p.LoginPath = "/Usuario/Login";
        p.AccessDeniedPath = "/Usuario/AccessoDenegado";
    });

builder.Services.AddAuthorization(option =>
{
    option.FallbackPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

});
builder.Services.AddScoped<ConfiguracionService> ();
builder.Services.Configure<FormOptions>(options =>
{
    options.ValueLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = int.MaxValue;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
using(var scope = app.Services.CreateScope())
{
    var configurationServices = scope.ServiceProvider.GetRequiredService<ConfiguracionService>();
    configurationServices.Initializer();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
