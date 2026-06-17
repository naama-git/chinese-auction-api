
using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Middlewares;
using ChineseAuctionAPI.Models.Exceptions;
using ChineseAuctionAPI.Repositories;
using ChineseAuctionAPI.Services;
using ChineseAuctionAPI.Validations;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
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


                // Authentication
                var jwtSettings = builder.Configuration.GetSection("Jwt");
                string? jwtKey = jwtSettings["Key"];

                if (string.IsNullOrEmpty(jwtKey))
                {
                    jwtKey = Environment.GetEnvironmentVariable("Jwt__Key");
                }

                if (string.IsNullOrEmpty(jwtKey))
                {
                    if (builder.Environment.IsDevelopment())
                    {
                        jwtKey = "This_Is_A_Super_Secret_Key_For_Development_Only_12345";
                    }
                    else
                    {
                        throw new Exception("FATAL: JWT Key is not configured!");
                    }
                }

                var key = Encoding.UTF8.GetBytes(jwtKey);

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

                // CORS
                builder.Services.AddCors(options =>
                {
                    options.AddDefaultPolicy(policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
                });

                //Rate limiting
                builder.Services.AddRateLimiter(options =>
                {
                    options.AddSlidingWindowLimiter("rateLimiting", opt =>
                    {
                        opt.Window = TimeSpan.FromMinutes(1);
                        opt.SegmentsPerWindow = 6;           
                        opt.PermitLimit = 100;
                        opt.QueueLimit = 0;
                    });

                    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
                });

                // Add services to the container
                builder.Services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;

                    });
                
                // Redis cache
                var redisConnectionString = builder.Configuration.GetConnectionString("Redis");
                builder.Services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = redisConnectionString;
                    options.InstanceName = "MyApp_"; 
                });

               

                // API Explorer
                builder.Services.AddEndpointsApiExplorer();
                
                // Swagger
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
                                    Id = "Bearer" 
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
                    options.UseSqlServer(
                        builder.Configuration.GetConnectionString("DefaultConnection"),
                        sqlOptions => sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorNumbersToAdd: null
                            )
                        )
                    
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

                // Kafka
                builder.Services.AddSingleton<IKafkaProducerService, KafkaProducerService>();

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

                // ---- Activaion ----

                // Static files
                app.UseStaticFiles();
                
                // Swagger on
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                // cors on
                app.UseCors();

                // rate limiting on 
                app.UseRateLimiter();

                app.UseHttpsRedirection();

                // auth on
                app.UseAuthentication();
                app.UseAuthorization();
                
                // controllers on
                app.MapControllers();

                // app on
                app.Run();
            }


            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed!");
            }

        }
    }
}