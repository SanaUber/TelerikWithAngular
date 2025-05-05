using Telerik.Reporting.Cache.File;
using Telerik.Reporting.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.IO;









var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages().AddNewtonsoftJson();
builder.Services.AddControllers();
builder.Services.AddMvc();
builder.Services.TryAddSingleton<IReportServiceConfiguration>(sp => new ReportServiceConfiguration
{
	ReportingEngineConfiguration = sp.GetService<IConfiguration>(),
	HostAppId = "TelerikReportApp",
	Storage = new FileStorage(),
	ReportSourceResolver = new UriReportSourceResolver(
		System.IO.Path.Combine(GetReportsDir(sp)))
});


// Add services to the container.
builder.Services.AddControllersWithViews();
// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()    // Or .WithOrigins("http://localhost:4200") for specific origins
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
 

 

// Use CORS before routing
app.UseCors("AllowAll");

app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.Run();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAntiforgery();
app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
	// ... 
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

static string GetReportsDir(IServiceProvider sp)
{
	return Path.Combine(sp.GetService<IWebHostEnvironment>().ContentRootPath, "Reports");
}
