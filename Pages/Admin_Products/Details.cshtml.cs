using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Razor.model;

namespace Razor.Pages_Admin_Products
{
    public class DetailsModel : PageModel
    {
        private readonly Razor.model.Context _context;

        public DetailsModel(Razor.model.Context context)
        {
            _context = context;
        }

        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.products
                .Include(p => p.kho).FirstOrDefaultAsync(m => m.MaSanPham == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
