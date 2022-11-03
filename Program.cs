using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using HCI_Project;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
void ConfigureServices(IServiceCollection services)
{
    string[] lines = File.ReadAllLines(@"C:\Users\Public\Documents\registration.txt");
    string connString = string.Format("Host={0};Username={1};Database=registration", lines[0], lines[1]);
    var builder = new NpgsqlConnectionStringBuilder(connString)
    {
        Password = lines[2]
    };
    services.AddDbContext<registrationContext>(options => options.UseNpgsql(builder.ConnectionString));
}
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
