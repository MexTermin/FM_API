using FM_API.Persistance.Database;
using FMAPI.Persistance.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
var url = $"http://0.0.0.0:{port}";
string dbconnection = Environment.GetEnvironmentVariable("FMDATABASE");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

#region Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
        policy.SetIsOriginAllowed(origin => true);
    });
});
#endregion

builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<DbContext, FMContext>(options => options.UseNpgsql(dbconnection));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#region Region Repository
builder.Services.AddTransient<EstimateRepository>();
builder.Services.AddTransient<SpentRepository>();
builder.Services.AddTransient<IncomeRepository>();
builder.Services.AddTransient<BudgetRepository>();
builder.Services.AddTransient<BudgetYearsRepository>();
builder.Services.AddTransient<RolRepository>();
builder.Services.AddTransient<TransactionRepository>();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<CategoryRepository>();
#endregion

var app = builder.Build();
app.UseCors();


// Configure the HTTP request pipeline.


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run(url);
