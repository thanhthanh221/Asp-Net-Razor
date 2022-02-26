using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.model;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Razor.Admin.Role
{
    [Authorize(Roles ="Admin")]
    public class IndexModel : Role_PageModel
    {
        public IndexModel(RoleManager<IdentityRole> roleManager, Context context) : base(roleManager, context)
        {

        }
        public class Claim_Role : IdentityRole {
            // có chứa 2 phần tử là ClaimType- ClaimValue
            public string[] Claims{set;get;}
        }
        [BindProperty(SupportsGet = true)]
        public List<Claim_Role> roles{set;get;}

        public async Task OnGetAsync()
        {
            List<IdentityRole> kq  = await roleManager.Roles.OrderBy(r => r.Name).ToListAsync();
            foreach (var item in kq)
            {
                // Phương thức tìm tất cả các Claim của Role
                var Hash = await roleManager.GetClaimsAsync(item); // là List chứa ClaimType_ClaimValue
                var Claim_in_Role = Hash.Select(c => c.Type+ ": " + c.Value); // Select(API LINQ => quyết định kiểu in ra)
                
                var Value = new Claim_Role(){
                    Id = item.Id,
                    Name = item.Name,
                    Claims =  Claim_in_Role.ToArray()
                };
                roles.Add(Value);
            }
        }
        public IActionResult OnPost(){
            return Page();
        }
    }
}
