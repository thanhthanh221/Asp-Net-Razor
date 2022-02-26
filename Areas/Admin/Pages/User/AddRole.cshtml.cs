using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Razor.model;

namespace Razor.Admin.User
{
    public class AddRole : PageModel
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        // UserManager thiết lập cho tài khoản
        // SignManager thiết lập đăng nhập 
        private readonly Razor.model.Context context;
        private readonly RoleManager<IdentityRole> roleManager;
        public AddRole(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, Razor.model.Context context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.roleManager = roleManager;
            this.context = context;
        }
        // Claim ở Role
        public List<IdentityRoleClaim<string>> identityRoleClaims{set; get;}
        //Claim ở User
        public List<IdentityUserClaim<string>> identityUserClaims{set;get;}
        [TempData]
        public string StatusMessage { get; set; }
        [BindProperty]
        public AppUser user{set;get;}
        [BindProperty]
        [Display(Name = "Các Role của User")]
        public string[] Role_Name{set;get;}
        public SelectList allRoles{set;get;} 
        public async Task<IActionResult> OnGetAsync(string User_id)
        {
            if(string.IsNullOrEmpty(User_id)){
                return NotFound($"không có User_id để tìm. ");
            }
            user = await _userManager.FindByIdAsync(User_id); // tìm user đăng nhập bằng User_id
            if (user == null)
            {
                return NotFound($"không có người dùng User_id: '{User_id}'.");
            }
            //GetRolesAsync(user) => phương thức tìm những roles của user
            Role_Name = (await _userManager.GetRolesAsync(user)).ToArray<string>(); // RoleName là những role của user
            // Tất cả các roles lưu trong RoleManger trong sql sever
            // thể hiện là allrole là bằng gán tất các các role của chung dự án 
            List<string> roleNames = await roleManager.Roles.Select(r=> r.Name).ToListAsync();
            allRoles = new SelectList(roleNames);

            // B1: Lấy tất cả các Role của User 
            var List_Role_User = from r in context.Roles 
                                join User_Role in context.UserRoles on r.Id equals User_Role.RoleId 
                                where User_Role.UserId == User_id
                                select r;
            // B2 lấy tất cả các Claim của Role đó trong bảng RoleClaims
            var Claim_In_Role =  from claim in context.RoleClaims 
                                join lru in List_Role_User on claim.RoleId equals lru.Id
                                select claim;

            identityRoleClaims = await Claim_In_Role.ToListAsync();

            // Tất cả các Claim riêng của User
            identityUserClaims = await (from uc in context.UserClaims where uc.UserId == User_id select uc).ToListAsync();
            
            // Không được dùng toListAsync() linh tinh -,-

            return Page();
        }   

        public async Task<IActionResult> OnPostAsync(string User_id)
        {
            if(string.IsNullOrEmpty(User_id)){
                return NotFound($"không có User_id để tìm. ");
            }
            user = await _userManager.FindByIdAsync(User_id); // tìm user đăng nhập bằng User_id
            if (user == null)
            {
                return NotFound($"không có người dùng User_id: '{User_id}'.");
            }
            // những role đã có của user
            string[] oldRoleName = (await _userManager.GetRolesAsync(user)).ToArray();
            // những role phải xóa là những role có trong oldRoleName nhưng không có trong RoleName
            var deleteRoleName = oldRoleName.Where(r => !Role_Name.Contains(r));
            // Những role cần thêm là những role không có trong oldRoleName nhưng có trong RoleName
            var addRole = Role_Name.Where(r => !oldRoleName.Contains(r));

            List<string> roleNames = await roleManager.Roles.Select(r => r.Name).ToListAsync();
            allRoles = new SelectList(roleNames);

            //B1: Xóa các roles trong oldRoles

            IdentityResult result_Delete = await _userManager.RemoveFromRolesAsync(user, deleteRoleName);
            if(!result_Delete.Succeeded){ // Nếu xóa không thành công thì thiết lập lỗi trong ModelState
                result_Delete.Errors.ToList().ForEach(error =>{
                    ModelState.AddModelError(string.Empty, error.Description);

                });

            }
            IdentityResult result_AddRols = await _userManager.AddToRolesAsync(user, addRole);
            if(!result_AddRols.Succeeded){
                result_AddRols.Errors.ToList().ForEach(error =>{
                    ModelState.AddModelError(string.Empty, error.Description);
                });
            }
            StatusMessage = $"Cập nhật roles cho User :{user.UserName}";

            return RedirectToPage("./Index");
        }
    }
}
