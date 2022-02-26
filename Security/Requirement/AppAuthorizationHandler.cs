using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Razor.model;
using System.Security.Claims;
namespace Razor.Security.Requirements
{
    // Để là 1 AuHandler thì phải khai triển từ Interface IAuthorizationHander 
    public class AppAuthorizationHandler : IAuthorizationHandler
    {
        // Thêm các dịch vụ logger và thông tin đăng nhập vào
        private readonly ILogger<AppAuthorizationHandler> logger;
        private readonly UserManager<AppUser> userManager;

        public AppAuthorizationHandler(ILogger<AppAuthorizationHandler> logger, UserManager<AppUser> userManager)
        {
            this.logger = logger;
            this.userManager = userManager;
        }

        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            // Tất cả các requirements
            logger.LogInformation(context.Resource?.GetType().Name);
            List<IAuthorizationRequirement> requirements = context.PendingRequirements.ToList();
            foreach (var item in requirements)
            {
                if(item is ThanhVienVipRequirement){
                    // Kiểm tra chính sách có thỏa mãn với điều kiện hay không
                    logger.LogInformation("Kiểm tra có thỏa mãn không");
                    if(ChinhSach3(context.User,(ThanhVienVipRequirement)item)){
                        context.Succeed(item);
                    }

                }               
            }

            return Task.CompletedTask;
        }
        private bool ChinhSach3(ClaimsPrincipal user, ThanhVienVipRequirement requirement)
        {
            // Phương thức tìm user 
            var appuserTask = userManager.GetUserAsync(user);
            // Đợi cho cái task bên trên hoàn thành
            Task.WaitAll(appuserTask);
            AppUser appUser = appuserTask.Result ; // => Task vụ hoàn thành
            // Kiểm tra xem có thỏa mãn điều kiện này không
            var hash = appUser.Count_DonHang;
            var hash1 = appUser.Monney_Use;
            bool kq = (appUser.Count_DonHang >= requirement.SoLuong && appUser.Monney >= requirement.SoTien) || user.IsInRole("Admin");
            return appUser.Count_DonHang >= requirement.SoLuong && appUser.Monney >= requirement.SoTien;
        }
    }
}