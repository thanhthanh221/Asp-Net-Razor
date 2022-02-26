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
    public class EditClaimUser : PageModel
    {
        public RoleManager<IdentityRole> roleManager{set;get;}
        public readonly UserManager<AppUser> userManager;
        public readonly Razor.model.Context context;
        public EditClaimUser(RoleManager<IdentityRole> roleManager, Razor.model.Context context, UserManager<AppUser> userManager)
        {
            this.roleManager = roleManager;
            this.context = context;
            this.userManager = userManager;
        }
        public AppUser appUser{set;get;}
        public IdentityUserClaim<string> identityUserClaim{set;get;}
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

        public async Task<IActionResult> OnPostAsync(int? Claim_Id)
        {
            if(Claim_Id == null){
                return NotFound("Không Tìm Thấy Claim Id");
            }
            identityUserClaim = await (from a in context.UserClaims where a.Id == Claim_Id select a).FirstOrDefaultAsync();
            if(identityUserClaim == null){
                return NotFound("Không Tìm Thấy Claim"); 
            }
            appUser = await userManager.FindByIdAsync(identityUserClaim.UserId);
            identityUserClaim.ClaimType = Input.Claim_Type;
            identityUserClaim.ClaimValue = Input.Claim_Value;
            if(context.UserClaims.Any(p => p.ClaimType == identityUserClaim.ClaimType && p.ClaimValue == identityUserClaim.ClaimValue && p.UserId == appUser.Id)){
                StatusMessage = " Claim này bị trùng";
                return Page();
            }
            // update phải lưu lại context
            await context.SaveChangesAsync();
            StatusMessage =" Update thành công ";
            return RedirectToPage("../AddRole", new{User_Id = appUser.Id});
        }
         public async Task<IActionResult> OnPostDeleteAsync(int? Claim_Id)
        {
            if(Claim_Id == null){
                return NotFound("Không Tìm Thấy Claim Id");
            }
            identityUserClaim = await (from a in context.UserClaims where a.Id == Claim_Id select a).FirstOrDefaultAsync();
            if(identityUserClaim == null){
                return NotFound("Không Tìm Thấy Claim"); 
            }
            appUser = await userManager.FindByIdAsync(identityUserClaim.UserId);
            
            var kq = context.UserClaims.Remove(identityUserClaim);
            var hash = context.SaveChanges();
            StatusMessage ="Xóa thành công";
            return RedirectToPage("../AddRole",new{User_id = appUser.Id});
        }

        public async Task<IActionResult> OnGetAsync(int? Claim_Id){
            if(Claim_Id == null){
                return NotFound("Không Tìm Thấy Claim Id");
            }
            identityUserClaim = await (from a in context.UserClaims where a.Id == Claim_Id select a).FirstOrDefaultAsync();
            if(identityUserClaim == null){
                return NotFound("Không Tìm Thấy Claim"); 
            }
            appUser = await userManager.FindByIdAsync(identityUserClaim.UserId);

            Input = new InputModel();
            Input.Claim_Type = identityUserClaim.ClaimType;
            Input.Claim_Value = identityUserClaim.ClaimValue;
            
            return Page();
        }
        
    }
}
