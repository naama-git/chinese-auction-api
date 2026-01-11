
using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Middlewares;
using ChineseAuctionAPI.Models.Exceptions;
using ChineseAuctionAPI.Repositories;
using ChineseAuctionAPI.Services;
using ChineseAuctionAPI.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.Filters;
using System.Security.Claims;
using System.Text;


namespace ChineseAuctionAPI
{
    public class Program
    {


        public static void Main(string[] args)
        {

            // serilog setting
            var configuration = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
             .Build();
            Log.Logger = new LoggerConfiguration()
           .WriteTo.Console()
           .CreateBootstrapLogger();

            try
            {

                Log.Information("Starting up the service...");

                var builder = WebApplication.CreateBuilder(args);


                builder.Host.UseSerilog((context, services, configuration) => configuration
                        .ReadFrom.Configuration(context.Configuration)
                        .Enrich.FromLogContext()
                        .Enrich.WithCorrelationId());


                //add Authentication
                var jwtSettings = builder.Configuration.GetSection("Jwt");
                var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);
                builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                    {

                        options.Events = new JwtBearerEvents
                        {
                            OnAuthenticationFailed = context =>
                                {
                                    Console.WriteLine("--- JWT Authentication Failed ---");
                                    Console.WriteLine($"Error: {context.Exception.Message}");
                                    return Task.CompletedTask;
                                },
                            OnChallenge = context =>
                            {
                                Console.WriteLine("--- JWT Challenge Triggered ---");
                                return Task.CompletedTask;
                            }
                        };
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = true,
                            ValidIssuer = jwtSettings["Issuer"],
                            ValidateAudience = true,
                            ValidAudience = jwtSettings["Audience"],
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero,
                            RoleClaimType = ClaimTypes.Role

                        };
                    });

                builder.Services.AddCors(options =>
                {
                    options.AddDefaultPolicy(policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
                });


                // Add services to the container
                builder.Services.AddControllers();

                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();

                builder.Services.AddSwaggerGen(c =>
                {
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Please enter ONLY the token (without the word 'Bearer')"
                    });
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer" // חייב להיות זהה לשם שנתת ב-AddSecurityDefinition
                                }
                            },
                            new string[] {}
                        }
                    });

                    c.OperationFilter<SecurityRequirementsOperationFilter>(true, "Bearer");
                });

                builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

                // dbContext
                builder.Services.AddDbContext<ChineseAuctionDBcontext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                );

                // dbfactory
                builder.Services.AddSingleton<DbContextFactory>();

                //AutoMapper
                builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

                // DI
                //Donor
                builder.Services.AddScoped<IDonorRepo, DonorRepository>();
                builder.Services.AddScoped<IDonorService, DonorService>();
                //User
                builder.Services.AddScoped<IUserRepo, UserRepository>();
                builder.Services.AddScoped<IUserService, UserService>();
                //Category
                builder.Services.AddScoped<ICategoryRepo, CategoryRepository>();
                builder.Services.AddScoped<ICategoryService, CategoryService>();
                //Prize
                builder.Services.AddScoped<IPrizeRepo, PrizeRepository>();
                builder.Services.AddScoped<IPrizeService, PrizeService>();

                //Ticket
                builder.Services.AddScoped<ITicketRepo, TicketRepository>();
                builder.Services.AddScoped<ITicketService, TicketService>();
                //Package
                builder.Services.AddScoped<IPackageRepo, PackageRepository>();
                builder.Services.AddScoped<IPackageService, PackageService>();
                //Winner
                builder.Services.AddScoped<IWinnerService, WinnerService>();
                builder.Services.AddScoped<IWinnerRepo, WinnerRepository>();
                builder.Services.AddScoped<IWinnerService, WinnerService>();
                builder.Services.AddScoped<IWinnerRepo, WinnerRepository>();

                //Order
                builder.Services.AddScoped<IOrderRepo, OrderRepository>();
                builder.Services.AddScoped<IOrderService, OrderService>();

                //Cart
                builder.Services.AddScoped<ICartRepo, CartRepository>();
                builder.Services.AddScoped<ICartService, CartService>();

                //Raffle
                builder.Services.AddScoped<IRaffleService, RaffleService>();

                // validations
                builder.Services.AddValidatorsFromAssemblyContaining<UserRegisterValidator>();
                builder.Services.AddValidatorsFromAssemblyContaining<UserLoginValidator>();
                builder.Services.AddValidatorsFromAssemblyContaining<DonorValidator>();
                builder.Services.AddValidatorsFromAssemblyContaining<PackageValidator>();
                builder.Services.AddValidatorsFromAssemblyContaining<PackageUpdateValidator>();


                var app = builder.Build();

                //error middleware
                app.UseMiddleware<ExceptionHandlingMiddleware>();

                // log HTTP requests
                app.UseSerilogRequestLogging(options =>
                    options.GetLevel = (httpContext, elapsed, ex) =>
                    {

                        if (ex is ErrorResponse || httpContext.Response.StatusCode < 500)
                        {
                            return LogEventLevel.Information;
                        }
                        return LogEventLevel.Error;
                    }
                );


                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseCors();

                app.UseHttpsRedirection();

                app.UseAuthentication();
                app.UseAuthorization();

                app.MapControllers();

                app.Run();
            }


            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed!");
            }

        }
    }
}
