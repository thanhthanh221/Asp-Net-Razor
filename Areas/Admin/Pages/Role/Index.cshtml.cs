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
        public List<IdentityRole> roles{set;get;}

        public async Task OnGetAsync()
        {
            roles = await roleManager.Roles.OrderBy(r => r.Name).ToListAsync();
        }
        public IActionResult OnPost(){
            return Page();
        }
    }
}
