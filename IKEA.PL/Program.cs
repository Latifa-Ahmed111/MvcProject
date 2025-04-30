using IKEA.BLL.Services.DepartmentsServices;
using IKEA.BLL.Services.EmployeeServices;
using IKEA.DAl.Persistance.Data;
using IKEA.DAl.Persistance.Repositories.Departments;
using IKEA.DAl.Persistance.Repositories.Employees;
using Microsoft.EntityFrameworkCore;

namespace IKEA.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configure Services 
            builder.Services.AddControllersWithViews();
            //with each request Clr Craete Dbcontxet 
            builder.Services.AddDbContext<ApplicationDbcontext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection"));
            });
            //when any one want to make object of IDepartmentsReposaiotry make it of DepartmentsReposatiory or (Oracle DepartmentReopsartory) and so on 

            builder.Services.AddScoped<IDepartmentsReposaiotry, DepartmentsReposatiory>();
            builder.Services.AddScoped<IDepartmentsServices, DepartmentsServices>();
            builder.Services.AddScoped<IEmployeesReposaitory, EmployeeReposaitory>();
            builder.Services.AddScoped<IEmployeeServices, EmployeeService>();
            #endregion


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            #region Configure Pipline 

            if (!app.Environment.IsDevelopment())
            { 
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            #endregion

            app.Run();
        }
    }
}
