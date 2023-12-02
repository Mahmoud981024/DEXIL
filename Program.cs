using GraduationProject.Models;
using GraduationProject.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace GraduationProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<Context>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("con"));

            });
            builder.Services.AddScoped<IService, ServiceRepo>();
            builder.Services.AddScoped<IServiceDetail, ServiceDetailRepo>();
            builder.Services.AddScoped<IRequest, RequestRepo>();
            builder.Services.AddScoped<IPayment, PaymentRepo>();
            builder.Services.AddScoped<IDescription, DescriptionRepo>();
            
            builder.Services.AddIdentity<ApplactionUser, IdentityRole>().AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();
          
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Service}/{action=GetAll}/{id?}");

            app.Run();
        }
    }
}