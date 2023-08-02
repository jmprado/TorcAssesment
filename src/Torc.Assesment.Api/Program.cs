using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using Torc.Assesment.Api.Model;
using Torc.Assesment.Api.Validators;
using Torc.Assesment.Dal;
using Torc.Assesment.Dal.Mapping;
using Torc.Assesment.Entities.ViewModel;
using TorcAssesment.BusinessLogic;

try
{
    string appName = "torc_assesment";

    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration.AddJsonFile("logConfig.json");

    // Extras - Adding Serilog to API
    builder.Host.UseSerilog((hostContext, services, configuration) =>
    {
        configuration.ReadFrom.Configuration(hostContext.Configuration);
    });

    builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    }));

    // Add services to the container.
    builder.Services.AddControllers();
    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddScoped<IUnityOfWork, UnityOfWork>();
    builder.Services.AddScoped<IOrderRepository, OrderRepository>();
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<IValidator<CreateOrderModel>, CreateOrderValidator>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<IValidator<User>, UserValidator>();
    builder.Services.AddScoped<IOrders, Orders>();

    builder.Services.AddAutoMapper(typeof(MappingProfile));

    builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true
        };
    });

    builder.Services.AddAuthorization();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Torc Assesment API",
            Version = "v1"
        });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
        });
    });

    builder.Services.AddCors(p => p.AddPolicy(appName, builder =>
    {
        builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
    }));

    var app = builder.Build();
    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseCors(appName);
    app.UseAuthorization();
    app.MapControllers();

    Log.Information("Torc Assesment API host started");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Torc Assesment API host terminated unexpectedly");
}
finally
{
    Log.Information("Server Shutting down...");
    Log.CloseAndFlush();
}