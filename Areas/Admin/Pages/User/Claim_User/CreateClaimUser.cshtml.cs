using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.model;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;

namespace Razor.Admin.Role.Claims_User
{
    public class CreateClaimUser : PageModel
    {
        public RoleManager<IdentityRole> roleManager{set;get;}
        public readonly UserManager<AppUser> userManager;
        public readonly Razor.model.Context context;
        public CreateClaimUser(RoleManager<IdentityRole> roleManager, Razor.model.Context context, UserManager<AppUser> userManager)
        {
            this.roleManager = roleManager;
            this.context = context;
            this.userManager = userManager;
        }
        public AppUser appUser{set;get;}
        public IdentityRoleClaim<string> identityRoleClaim{set;get;}
        [TempData]
        public string StatusMessage{set;get;}
        public class InputModel{
            [Display(Name ="Type của Claim")]
            [Required(ErrorMessage ="Phải Nhận {0}")]
            [StringLength(256,ErrorMessage ="Tên Không được quá dài")]
            public string Claim_Type{set;get;}
            [Display(Name ="Type của Claim")]
            [Required(ErrorMessage ="Phải Nhận {0}")]
            [StringLength(256,ErrorMessage ="Tên Không được quá dài")]
            public string Claim_Value{set;get;}
        }
        [BindProperty]
        public InputModel Input{set;get;}

        public async Task<IActionResult> OnPostAsync(string User_Id)
        {
            if(string.IsNullOrEmpty(User_Id)){
                return NotFound("Không có ID để của Claim");
            }
            appUser = await userManager.FindByIdAsync(User_Id);
            if(appUser == null){
                return NotFound("Không Tìm Thấy User");
            }
            if(context.UserClaims.Any(p => p.ClaimType == Input.Claim_Type && p.ClaimValue == Input.Claim_Value && p.UserId == appUser.Id)){
                return NotFound("User này đã chứa Claim trên");
            }
            
            await userManager.AddClaimAsync(appUser,new Claim(Input.Claim_Type,Input.Claim_Value));

            await context.SaveChangesAsync();
            StatusMessage ="Đã Tạo Mới thành công Claim";
            return RedirectToPage("../AddRole", new{User_Id = appUser.Id});   

        }
        public async Task<IActionResult> OnGetAsync(string User_Id){
            if(string.IsNullOrEmpty(User_Id)){
                return NotFound("Không Tìm Thấy User id");
            }
            appUser = await userManager.FindByIdAsync(User_Id);
            if(appUser == null){
                return NotFound("Không Tìm Thấy User"); 
            }
            
            return Page();
        }
        
    }
}
