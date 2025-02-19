using ApiBarangBukti.Models;
using ApiBarangBukti.Repository;
using ApiBarangBukti.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IHdBarangBukti, HdBarangBuktiRepositories>();
builder.Services.AddScoped<IDtBarangBukti, DtBarangBuktiRepositories>();
builder.Services.AddScoped<IHdTransaksi, HdTransaksiRepositories>();
builder.Services.AddScoped<IDtTransaksi, DtTransaksiRepositories>();
builder.Services.AddScoped<ILogUser, LogUserRepositories>();



builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Barang Bukti", Version = "v1" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

var Configuration = builder.Configuration;
builder.Services.AddDbContext<DbsiramContext>(options =>
        options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
