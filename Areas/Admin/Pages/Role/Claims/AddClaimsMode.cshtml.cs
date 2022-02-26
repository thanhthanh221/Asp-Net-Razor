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
    public class AddClaimsMode : PageModel
    {
        public RoleManager<IdentityRole> roleManager{set;get;}
        public Razor.model.Context context{set;get;}
        public AddClaimsMode(RoleManager<IdentityRole> roleManager, Razor.model.Context context)
        {
            this.roleManager = roleManager;
            this.context = context;
        }
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

        public async Task<IActionResult> OnPostAsync(string role_Id)
        {
            if(string.IsNullOrEmpty(role_Id)){
                return NotFound("Không có ID để của Role");
            }
            var kq = await roleManager.FindByIdAsync(role_Id);
            if(kq == null){
                return NotFound("Không tim thấy Role");
            }
            if(!ModelState.IsValid){

                return Page();
            }
            Claim a = new Claim(Input.Claim_Type,Input.Claim_Value);
            bool check = (await roleManager.GetClaimsAsync(kq)).Any(p=> p.Value.Equals(Input.Claim_Value) && p.Type.Equals(Input.Claim_Type));
            if(check){
                StatusMessage = $"Claim này bị trùng với Role {kq.Name}";
                return RedirectToPage("../Index");
            }

            var Update_Claim = await roleManager.AddClaimAsync(kq,a);

            if(!Update_Claim.Succeeded){
                ModelState.AddModelError(string.Empty, "Lỗi Succeeded");
                return Page();
            }
            StatusMessage = $"Đã Update thành công Claim cho Role[{kq.Name}] với Claim [{Input.Claim_Type}] giá trị [{Input.Claim_Value}]";
            return RedirectToPage("../Index");   
        }
        public async Task<IActionResult> OnGetAsync(string role_Id){
            if(string.IsNullOrEmpty(role_Id)){
                return NotFound("Không có ID để của Role");
            }
            var kq = await roleManager.FindByIdAsync(role_Id);
            if(kq == null){
                return NotFound("Không tìm thấy Role");
            }
            return Page();
        }
        
    }
}
