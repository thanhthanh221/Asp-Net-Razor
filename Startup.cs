using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Razor.model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Album.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Html;


namespace Razor
{
    public class Startup
    {
        public string ContentRootPath { set; get;}
        public Startup(IConfiguration configuration,IWebHostEnvironment env)
        {
            Configuration = configuration;
            ContentRootPath = env.ContentRootPath;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddDbContext<Context>(options =>{
                string value = Configuration.GetConnectionString("Context");
                options.UseSqlServer(value);
            });
            services.AddOptions();
            IConfiguration mailSetting = Configuration.GetSection("MailSettings");
            services.AddSingleton<IEmailSender,SendMailService>();


            services.Configure<MailSettings>(mailSetting);
            services.AddIdentity<AppUser,IdentityRole>().AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();       
            services.Configure<IdentityOptions> (options => {
                // Thiết lập về Password
                options.Password.RequireDigit = true; // bắt phải có số
                options.Password.RequireLowercase = true; // bắt phải có chữ thường
                options.Password.RequireNonAlphanumeric = false; // Không bắt ký tự đặc biệt
                options.Password.RequireUppercase = true; // bắt buộc chữ in
                options.Password.RequiredLength = 6;     // Số ký tự tối thiểu của password
                options.Password.RequiredUniqueChars = 0; // Số ký tự riêng biệt

                // Cấu hình Lockout - khóa user
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMilliseconds(5); // Khóa 5 phút
                options.Lockout.MaxFailedAccessAttempts = 5; // Thất bại 5 lầ thì khóa
                options.Lockout.AllowedForNewUsers = true;

                // Cấu hình về User.
                options.User.AllowedUserNameCharacters = // các ký tự đặt tên user
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ạ ế";
                options.User.RequireUniqueEmail = true;  // Email là duy nhất

                // Cấu hình đăng nhập.
                options.SignIn.RequireConfirmedEmail = false;            // Cấu hình xác thực địa chỉ email (email phải tồn tại)
                options.SignIn.RequireConfirmedPhoneNumber = false;     // Xác thực số điện thoại
                options.SignIn.RequireConfirmedAccount = false;


            });
            services.ConfigureApplicationCookie(options =>{
                options.LoginPath="/Identity/Account/Login";
                options.LogoutPath ="/Identity/Account/Logout";
                options.AccessDeniedPath ="/Identity/Account/AccessDenied";

            });
            services.AddAuthentication()
                .AddGoogle(options=>{
                    IConfiguration google_config = Configuration.GetSection("Authentication:Google");
                    options.ClientId= google_config["ClientId"];
                    options.ClientSecret= google_config["ClientSecret"];
                    options.CallbackPath="/Login_Google";
                })
                .AddFacebook(options =>{
                    IConfiguration Facebook_config = Configuration.GetSection("Authentication:Facebook");
                    options.AppId= Facebook_config["AppId"];
                    options.AppSecret= Facebook_config["AppSecret"];
                    options.CallbackPath="/Login_Facebook";

                });
                
                
                
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseStatusCodePagesWithRedirects("/Error");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
