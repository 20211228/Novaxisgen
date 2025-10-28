using Microsoft.EntityFrameworkCore;
using Novaxisgen.Models;
using Novaxisgen.Services;


var builder = WebApplication.CreateBuilder(args);
// namespace chứa DbContext của bạn

builder.Logging.ClearProviders();
builder.Logging.AddConsole();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<NovaxisgenContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("NovaxisgenDB")));
builder.Services.AddSession();

// lãi hàng ngày
builder.Services.AddScoped<DailyEarningService>();
builder.Services.AddHostedService<DailyEarningBackgrService>();

// lãi trên lãi
builder.Services.AddScoped<LaiTrenLaiService>();
builder.Services.AddHostedService<DailyLaiTrenLai>();

//lãi nhị phân
builder.Services.AddScoped<BinaryComService>();

//lên vip
builder.Services.AddScoped<VipService>();

// Tinh hoa hong lanh dao
builder.Services.AddScoped<HoaHongLanhDaoService>();

// EtherScan
builder.Services.AddHttpClient<EtherscanService>();
builder.Services.AddHostedService<TransactionScannerService>();



// lãi trực tiếp
builder.Services.AddScoped<DirectCommissionService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=LandingPages}/{action=LandingPage}/{id?}");

app.Run();
