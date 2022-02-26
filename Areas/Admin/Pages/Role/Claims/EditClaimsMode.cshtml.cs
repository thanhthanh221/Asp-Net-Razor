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

namespace Razor.Admin.Role.Claims
{
    public class EditClaimsMode : PageModel
    {
        public RoleManager<IdentityRole> roleManager{set;get;}
        public readonly Razor.model.Context context;
        public EditClaimsMode(RoleManager<IdentityRole> roleManager, Razor.model.Context context)
        {
            this.roleManager = roleManager;
            this.context = context;
        }
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

        public async Task<IActionResult> OnPostAsync(int? Claim_Id)
        {
            if(Claim_Id == null){
                return NotFound("Không có ID để của Claim");
            }
            identityRoleClaim =await context.RoleClaims.Where(p => p.Id == Claim_Id).FirstOrDefaultAsync();
            if(identityRoleClaim == null){
                return NotFound("Không tìm thấy Claim");
            }
            var Role_Claim = await roleManager.FindByIdAsync(identityRoleClaim.RoleId);
            if(Role_Claim == null){
                StatusMessage = "Không co Role";
                return RedirectToPage("../Index");
            }
            if(!ModelState.IsValid){
                StatusMessage = "Lỗi dữ liệu nhập vào";
                return RedirectToPage("../Index");
            }
            // Kiểm tra xem Claim nhập vào bị trùng hay chưa 
            if(context.RoleClaims.Any(p => p.ClaimValue == Input.Claim_Value && p.ClaimType == Input.Claim_Type && p.RoleId == Role_Claim.Id)){
                StatusMessage =$"Role : {Role_Claim.Name} đã tồn tại Claim với {identityRoleClaim.ClaimType} giá trị {identityRoleClaim.ClaimValue}";

                return RedirectToPage("../Edit", new {role_Id = Role_Claim.Id});
            }
            identityRoleClaim.ClaimType = Input.Claim_Type;
            identityRoleClaim.ClaimValue = Input.Claim_Value;
            
            await context.SaveChangesAsync();
            StatusMessage ="Đã Update thành Công Claim ";
            return RedirectToPage("../Index");   

        }
        public async Task<IActionResult> OnPostDeleteAsync(int? Claim_Id)
        {
            if(Claim_Id == null){
                return NotFound("Không có ID để của Claim để xóa");
            }
            identityRoleClaim =await context.RoleClaims.Where(p => p.Id == Claim_Id).FirstOrDefaultAsync();
            if(identityRoleClaim == null){
                return NotFound("Không tìm thấy Claim");
            }
            var Role_Claim = await roleManager.FindByIdAsync(identityRoleClaim.RoleId);
            if(Role_Claim == null){
                StatusMessage = "Không co Role";
                return RedirectToPage("../Index");
            }
            await roleManager.RemoveClaimAsync(Role_Claim, new Claim(identityRoleClaim.ClaimType, identityRoleClaim.ClaimValue));
            StatusMessage ="Đã Xóa Thành công Claim";
            return RedirectToPage("../Index");   

        }
        public async Task<IActionResult> OnGetAsync(int? Claim_Id){
            if(Claim_Id == null){
                return NotFound("Không có ID để của Claim");
            }
            identityRoleClaim =await context.RoleClaims.Where(p => p.Id == Claim_Id).FirstOrDefaultAsync();
            if(identityRoleClaim == null){
                return NotFound("Không tìm thấy Claim");
            }
            // Phải tạo đối tượng mới
            Input = new InputModel(){
                Claim_Type = identityRoleClaim.ClaimType,
                Claim_Value = identityRoleClaim.ClaimValue
            };
            return Page();
        }
        
    }
}
