using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ST10062630_PROG6212_POE.Data;

var builder = WebApplication.CreateBuilder(args);

// Load configuration from appsettings.json
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
sqlOptions => sqlOptions.EnableRetryOnFailure(
    maxRetryCount: 5, // Number of retry attempts
    maxRetryDelay: TimeSpan.FromSeconds(10), // Delay between retries
    errorNumbersToAdd: null // Optional: Specific SQL error numbers to retry
)
    )
);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Main}/{action=Index}/{id?}");

app.Run();
