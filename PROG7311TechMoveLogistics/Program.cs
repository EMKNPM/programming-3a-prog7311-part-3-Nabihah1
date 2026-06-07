using Microsoft.EntityFrameworkCore;
using PROG7311TechMoveLogistics.APIServices;
using PROG7311TechMoveLogistics.Data;
using PROG7311TechMoveLogistics.Services; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();


//add this to connect to the database 
//builder.Services.AddDbContext<DataContext>(options =>
//options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient<ICurrencyService, CurrencyService>();

//builder.Services.AddScoped<IContractService, ContractService>();

//builder.Services.AddScoped<IServiceRequestService, ServiceRequestService>();

//part 3: adding http client so it can connect to api 
builder.Services.AddHttpClient();

builder.Services.AddHttpClient<IClientAPIService,
    ClientApiService>(client =>
    {
        client.BaseAddress =new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
    });

builder.Services.AddHttpClient<IContractApiService,
    ContractApiService>(client =>
    {
        client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
    });

builder.Services.AddHttpClient<IServiceRequestApiService,
    ServiceRequestApiService>(client =>
    {
        client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]!);
    });



var app = builder.Build();
app.UseDeveloperExceptionPage();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
