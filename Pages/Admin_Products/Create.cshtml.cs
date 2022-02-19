using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Razor.model;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace Razor.Pages_Admin_Products
{
    [Authorize(Roles ="Admin")]
    public class CreateModel : PageModel
    {
        private IHostingEnvironment environment;
        private readonly Razor.model.Context _context;
        [Display(Name ="Ảnh Sản Phẩm")]
        [BindProperty]
        public IFormFile fileUploads{set;get;}

        public CreateModel(Razor.model.Context context,IHostingEnvironment environment)
        {
            _context = context;
            this.environment = environment;
            
        }
        public IActionResult OnGet()
        {
            ViewData["MaKho"] = (from a in _context.khos select a).ToList();
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("/Index");
                
            }
            string file;
            if (fileUploads != null) {
                string hash = Product.Name;
                file = Path.Combine (environment.ContentRootPath,"wwwroot","img","products", "Product-img-"+hash+"+.jpg");
                using (var fileStream = new FileStream (file, FileMode.Create)) {
                    await fileUploads.CopyToAsync (fileStream);
                    var leng = new byte[fileStream.Length];
                    fileStream.Read(leng,0,leng.Length);
                    Product.Anh1 = leng;
                }
            }
            if(!_context.products.Contains(Product)){
                await _context.products.AddAsync(Product);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("/Admin_Products/Index");
            
        }
    }
}
