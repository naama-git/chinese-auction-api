
using ChineseAuctionAPI.Data;
using ChineseAuctionAPI.Interface;
using ChineseAuctionAPI.Repositories;
using ChineseAuctionAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace ChineseAuctionAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // dbContext
            builder.Services.AddDbContext<ChineseAuctionDBcontext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
            );

            // db factory
            builder.Services.AddSingleton<DbContextFactory>();

            //Auto Mapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // DI
            //Donor
            builder.Services.AddScoped<IDonorRepo, DonorRepository>();
            builder.Services.AddScoped<IDonorService, DonorService>();
            //User
            builder.Services.AddScoped<IUserRepo, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> 345624fd9c498db560c9f029a11f6cd9b9014b63
            //Category
            builder.Services.AddScoped<ICategoryRepo, CategoryRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            //Prize
            builder.Services.AddScoped<IPrizeRepo, PrizeRepository>();
            builder.Services.AddScoped<IPrizeService, PrizeService>();

<<<<<<< HEAD
=======
            builder.Services.AddScoped<IPrizeRepo, PrizeRepository>();
            builder.Services.AddScoped<IPrizeService, PrizeService>();
>>>>>>> 5152a63e98f732a86c30ebcb4a424031a389883c
=======
>>>>>>> 345624fd9c498db560c9f029a11f6cd9b9014b63

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
        }
    }
}
