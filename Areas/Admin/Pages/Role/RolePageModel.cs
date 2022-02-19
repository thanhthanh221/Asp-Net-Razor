using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Razor.model;
namespace Razor.Admin.Role{
    public class Role_PageModel : PageModel {
        protected readonly RoleManager<IdentityRole> roleManager;
        protected readonly Razor.model.Context context;
        [TempData] // Nội dung sẽ lưu ở trong ss của phiên hoạt động
        public string StatusMessage{set;get;}
        public Role_PageModel(RoleManager<IdentityRole> roleManager,Razor.model.Context context){
            this.roleManager = roleManager;
            this.context = context;
        }

    }

}