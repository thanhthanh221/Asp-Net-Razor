using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.model;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Razor.Admin.User
{
    public class AppUser_Role : AppUser{
        public string Role_Name_User{set;get;}
        
    }
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> userManager; // Nơi lưu trữ tài khoản User
        [TempData]
        public string StatusMessage{set;get;}
        public IndexModel(UserManager<AppUser> userManager)
        {
            this.userManager = userManager;
        }
        [BindProperty]
        public List<AppUser_Role> AppUsers{set;get;}

        public async Task OnGetAsync()
        {
            var kq = await userManager.Users.OrderBy(r => r.Id).ToListAsync();
            
            AppUsers = (kq.Select( p => new AppUser_Role() {
                Id = p.Id,
                UserName = p.UserName,                
            })).ToList();

            foreach (var item in AppUsers)
            {
                // Phương thức tìm Role trong user
                var findRole = await userManager.GetRolesAsync(item);
                foreach (var role in findRole){
                   item.Role_Name_User +=" "+role; 
                   
                }
                
            }
        }
        public IActionResult OnPost(){
            return Page();
        }
    }
}
