using WebBarangBukti.Models;
using WebBarangBukti.Service;
using WebBarangBukti.Service.IService;
using WebSiram;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.IsEssential = true;
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDirectoryBrowser();

builder.Services.AddScoped<IHdTransaksiService, HdTransaksiService>();
builder.Services.AddScoped<IDtTransaksiService, DtTransaksiService>();
builder.Services.AddScoped<IHdBarangBuktiService, HdBarangBuktiService>();
builder.Services.AddScoped<IDtBarangBuktiService, DtBarangBuktiService>();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
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
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=HdTransaksi}/{action=Index}/{id?}");

app.Run();
