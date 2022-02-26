using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Razor.model;

namespace Razor.Pages_Admin_Products
{
    public class EditModel : PageModel
    {
        private readonly Razor.model.Context _context;

        public EditModel(Razor.model.Context context)
        {
            _context = context;
        }

        [BindProperty]
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
                return NotFound("Không tìm thấy sản phẩm");
            }
            // B1 nhóm tất cả các giá trị của thuộc tính theo thuộc tính
            var kq = (from p in _context.attributes_Values where p.Attributes_ID == 1 select p).ToList();
            // B2 lấy tất cả thuộc tính sản phẩm của sản phẩm
            var Result = (from a in kq 
                        join p in _context.product_Attributes on a.ID equals p.Product_ID
                        select new {
                            ID = p.Product_ID,
                            Value = a.value
                        }).ToList();

            

            ViewData["MaKho"] = new SelectList(_context.khos, "MaKho", "MaKho");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(Product.MaSanPham))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ProductExists(int id)
        {
            return _context.products.Any(e => e.MaSanPham == id);
        }
    }
}
