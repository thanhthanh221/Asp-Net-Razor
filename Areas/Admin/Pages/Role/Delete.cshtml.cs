using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.model;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Razor.Admin.Role
{
    public class DeleteModel : Role_PageModel
    {
        public DeleteModel(RoleManager<IdentityRole> roleManager, Context context) : base(roleManager, context)
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
        

        public async Task<IActionResult> OnGetAsync(string role_Id)
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
                NotFound("Không Tìm Thấy Role.");
                return RedirectToPage("./Index");
            }
            identityRole = await roleManager.FindByIdAsync(role_Id);
            if(!ModelState.IsValid){
                return RedirectToPage("./Index");
            }

            IdentityResult reuslt = await roleManager.DeleteAsync(identityRole);

            if(reuslt.Succeeded){
                StatusMessage =$" Bạn vừa xóa role: {identityRole.Name}";
            }
            else{
                reuslt.Errors.ToList().ForEach(errorr =>{
                    ModelState.AddModelError(string.Empty,errorr.Description);
                });
            }
            

            return RedirectToPage("./Index");

        }
        
    }
}
