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
    public class CreateModel : Role_PageModel
    {
        public CreateModel(RoleManager<IdentityRole> roleManager, Context context) : base(roleManager, context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid){
                return Page();
            }
            var newRole = new IdentityRole(Input.Name);
            IdentityResult result = await roleManager.CreateAsync(newRole);

            if(result.Succeeded){
                StatusMessage = "Bạn Vừa Tạo Một Vai Trò Mới";
                return RedirectToPage("./Index");
            }
            result.Errors.ToList().ForEach(error=>{
                ModelState.AddModelError(string.Empty, error.Description);
            });
            return Page();
        }
        public IActionResult OnGet(){
            return Page();

        }
        
    }
}
