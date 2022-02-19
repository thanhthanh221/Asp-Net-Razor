using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.model;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Razor.Admin.Role
{
    [Authorize]
    public class EditModel : Role_PageModel
    {
        public EditModel(RoleManager<IdentityRole> roleManager, Context context) : base(roleManager, context)
        {
        }
         public class InputModel{
            [Display(Name ="Tên Của Role")]
            [Required(ErrorMessage ="Phải Nhận {0}")]
            [StringLength(256,ErrorMessage ="Tên Không được quá dài")]
            public string Name{set;get;}
        }
        [BindProperty]
        public InputModel Input{set;get;}
        public IdentityRole identityRole{set;get;}
        

        public async Task<IActionResult> OnGetAsync(string role_Id) // Phương thức dùng để
        {
            if(role_Id == null){
                return NotFound("null thì tìm cái gì ??");
            }
            identityRole = await roleManager.FindByIdAsync(role_Id);
            if(identityRole != null ){
                Input = new InputModel(){
                    Name = identityRole.Name
                };
                return Page();
            }
            
            return NotFound("Không Tìm thấy role");
        }
        public async Task<IActionResult> OnPostAsync(string role_Id){
            if(role_Id == null){
                return NotFound("Không Tìm Thấy Role.");
            }
            identityRole = await roleManager.FindByIdAsync(role_Id);
            if(!ModelState.IsValid){
                return Page();
            }
            identityRole.Name = Input.Name;
            IdentityResult reuslt = await roleManager.UpdateAsync(identityRole);

            if(reuslt.Succeeded){
                StatusMessage =$" Bạn vừa Đổi Tên {Input.Name}";
            }
            else{
                reuslt.Errors.ToList().ForEach(errorr =>{
                    ModelState.AddModelError(string.Empty,errorr.Description);
                });
                }
            

            return Page();

        }
        
    }
}
