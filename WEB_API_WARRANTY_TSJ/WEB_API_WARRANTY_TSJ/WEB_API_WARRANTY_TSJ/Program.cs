using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WEB_API_WARRANTY_TSJ.Help;
using WEB_API_WARRANTY_TSJ.Models;
using WEB_API_WARRANTY_TSJ.ModelsDBERP;
using WEB_API_WARRANTY_TSJ.Repositories;
using WEB_API_WARRANTY_TSJ.Repositories.IRepositories;
using WEB_API_WARRANTY_TSJ.Services;
using WEB_API_WARRANTY_TSJ.Services.IService;

var builder = WebApplication.CreateBuilder(args);
var configurationDev = new ConfigurationBuilder().AddJsonFile($"appsettings.Development.json");
//var configurationProd = new ConfigurationBuilder().AddJsonFile($"appsettings.Production.json");

// Add services to the container.
var configDev = configurationDev.Build();
//var configProd = configurationProd.Build();

var connectionString = configDev.GetConnectionString("warrantyContext").ToString();
var connectionStringERP = configDev.GetConnectionString("erpContext").ToString();

//var connectionString = configProd.GetConnectionString("warrantyContext").ToString();
//var connectionStringERP = configProd.GetConnectionString("erpContext").ToString();
builder.Services.AddDbContext<DBWARContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<ERPContext>(options => options.UseSqlServer(connectionStringERP));

UrlPathFile.filePathTemplateQRCode = Path.Combine(builder.Environment.ContentRootPath, "TemplateBarcode", "RptQRCodeBusa.rdlc");
UrlPathFile.filePathTemplateActivationCode = Path.Combine(builder.Environment.ContentRootPath, "TemplateBarcode", "RptActivationQRBusa.rdlc");
UrlPathFile.filePathTempFile = Path.Combine(builder.Environment.ContentRootPath, "TempFile");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILoginRepositories, LoginRepositories>();
builder.Services.AddScoped<IBarcodeSerialQRRepositories, BarcodeSerialQRRepositories>();
builder.Services.AddScoped<IActivationQRRepositories, ActivationQRRepositories>();
builder.Services.AddScoped<IWarrantyRepositories, WarrantyRepositories>();
builder.Services.AddScoped<ILogRequestActivationQRRepositories, LogRequestActivationRepositories>();
builder.Services.AddScoped<ITemplatePrintRepositories, TemplatePrintRepositories>();
builder.Services.AddScoped<IPrinterService, PrinterService>();
builder.Services.AddScoped<IErrorRepositories, ErrorRepositories>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
     
        policy.AllowAnyOrigin() 
              .AllowAnyMethod() 
              .AllowAnyHeader(); 
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true
        };
    });



builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Web API Warranty TSJ",
        Description = "Warranty TSJ"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token"

    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//if (app.Environment.IsProduction())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}


app.UseHttpsRedirection();

app.UseAuthentication(); // <-- first
app.UseAuthorization(); // <-- second

app.MapControllers();

//app.Use(async (context, next) =>
//{
//    context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
//    await next.Invoke();
//});

app.UseCors("AllowAll");
app.Run();
