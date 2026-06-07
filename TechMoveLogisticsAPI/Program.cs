using PROG7311TechMoveLogisticsAPI.Data;
using TechMoveLogisticsAPI.Services;
using TechMoveLogisticsAPI.Repositories;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

//Swagger API 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllersWithViews();

//register the repos
builder.Services.AddScoped< IContractRepo, ContractRepo>();
builder.Services.AddScoped< IClientRepo,  ClientRepo>();
builder.Services.AddScoped< IServiceRequestRepo, ServiceRequestRepo>();


//register the services 
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<IServiceRequestService, ServiceRequestService>();


//registers the currency service 
builder.Services.AddHttpClient<ICurrencyService, CurrencyService>();

//configure sql connection in the API 
builder.Services.AddDbContext<DataContext>(options =>
           options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Auto-apply migrations on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DataContext>();
    db.Database.Migrate();
}

app.UseSwagger();

app.UseSwaggerUI();


app.MapGet("/", () => Results.Redirect("/swagger"));

// Configure the HTTP request pipeline.

   
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();




namespace TechMoveLogisticsAPI
{
    public partial class Program
    {

    }
}

