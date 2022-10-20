using System.Text;
using BookStore.BL.CommandHandlers.BookHandlers;
using BookStore.BL.Services.HostedServices;
using BookStore.Caches;
using BookStore.DL.Repositories.MsSQL;
using BookStore.Extensions;
using BookStore.HealthChecks;
using BookStore.Middleware;
using BookStore.Models.Models;
using BookStore.Models.Models.Configurations;
using BookStore.Models.Models.Users;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

var logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(theme: AnsiConsoleTheme.Literate)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));

builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services.RegisterRepos()
    .RegisterServices()
    .AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** your JWT Bearer token in the textbox below",
        Reference = new OpenApiReference()
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    x.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    x.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {jwtSecurityScheme, Array.Empty<string>() }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
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

builder.Services.AddHealthChecks()
    .AddCheck<SqlHealthCheck>("SQL Server")
    .AddCheck<CustomHealthCheck>("Custom")
    .AddUrlGroup(new Uri("https://google.bg"),name:"Google Service");

builder.Services.AddMediatR(typeof(GetAllBooksHandler).Assembly);

builder.Services.AddIdentity<User, UserRole>()
    .AddUserStore<UserStore>()
    .AddRoleStore<UserRolesStore>();

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("Admin",policy =>
    {
        policy.RequireClaim("Admin");
    });
});

builder.Services.Configure<KafkaConfiguration>(
    builder.Configuration.GetSection(nameof(KafkaConfiguration)));

builder.Services.Configure<MongoPurchaseConfiguration>(
    builder.Configuration.GetSection(nameof(MongoPurchaseConfiguration)));
builder.Services.Configure<MongoShoppingCart>(
    builder.Configuration.GetSection(nameof(MongoShoppingCart)));

builder.Services.AddHostedService<DeliveryAndPurchaseHS>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

//app.MapHealthChecks("/health");
app.RegisterHealthChecks();
app.UseMiddleware<ErrorHandlerMiddleware>();
//app.UseMiddleware<ConsoleWriteMiddleware>();
app.Run();
