using FM_API.Persistance.Database;
using FMAPI.Persistance.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
string port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
string url = $"http://0.0.0.0:{port}";
string dbconnection = Environment.GetEnvironmentVariable("FMDATABASE");

#region Cors

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
        policy.SetIsOriginAllowed(origin => true);
    });
});
#endregion

#region Project Senttings
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddControllersWithViews().AddControllersAsServices().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<DbContext, FMContext>(options => options.UseNpgsql(dbconnection));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion

// Rotativa Config
Rotativa.AspNetCore.RotativaConfiguration.Setup(builder.Environment.ContentRootPath, "./Rotativa");

#region Region Repository
builder.Services.AddTransient<EstimateRepository>();
builder.Services.AddTransient<BudgetRepository>();
builder.Services.AddTransient<RolRepository>();
builder.Services.AddTransient<TransactionRepository>();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<CategoryRepository>();
builder.Services.AddTransient<TypeRepository>();
#endregion

var app = builder.Build();
app.UseCors("AllowAnyOrigin");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
