using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Razor.model;
using System.Threading;

namespace Razor.Pages_Admin_Products
{
    public class IndexModel : PageModel
    {
        private readonly Razor.model.Context _context;
        public string SetUpMoney(int value){
            List<string> a = new List<string>();
            string hash = string.Empty;
            string s = value.ToString();
            int temp = 0;
            for (int i = s.Length-1; i >= 0; i--)
            {
                a.Add(s[i].ToString());
                temp++;
                if(temp== 3){
                    temp= 0;
                    a.Add(".");
                }
            }
            if(!a[a.Count-1].Equals(".")){
                hash+= a[a.Count-1];
            }
            for (int i = a.Count-2; i >= 0; i--)
            {
                hash+= a[i];
                            
            }
            return hash;
        }

        public IndexModel(Razor.model.Context context)
        {
            _context = context;
        }
        public class In_Attribute_Product : Product{
            public int ID{set;get;}
            public string value{set;get;}   
        }
        public List<In_Attribute_Product> Result{set;get;}

        public IList<Product> Product { get;set; }
        public const int ITEM_PER_PAGE= 3;

        [BindProperty(SupportsGet =true,Name ="p")]
        public int currentPage{set;get;}
        public int countPages{set;get;}
        public async Task<IActionResult> OnGetAsync(string name_Product)
        {
            int total_Products = _context.products.Count();

            countPages = (int)Math.Ceiling((double)total_Products/ITEM_PER_PAGE);
            if(currentPage < 1){
                currentPage = 1;
            }
            else if(countPages > countPages){
                currentPage = countPages;
            }
            // B1 nhóm tất cả các giá trị của thuộc tính theo thuộc tính => Đơn vị
            List<Attributes_Value> kq1 = (from p in _context.attributes_Values where p.Attributes_ID == 1 select p).ToList();
            // B2 lấy tất cả thuộc tính sản phẩm của sản phẩm
            Result = (from a in kq1
                        join p in _context.product_Attributes on a.ID equals p.Product_ID
                        select new In_Attribute_Product() {
                            ID = p.Product_ID,
                            value = a.value
                        }).ToList();

            var kq = (from a in _context.products orderby a.Name select a).Skip((currentPage-1)*ITEM_PER_PAGE).Take(ITEM_PER_PAGE);
            if(string.IsNullOrEmpty(name_Product)){
                Product = kq.ToList();
            }
            else{
                Product = (from a in _context.products where a.Name.ToLower().Contains(name_Product.ToLower()) select a).ToList();
                if(Product.Count == 0){
                    Product =  await kq.ToListAsync();
                }
            }
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int? Product_Id){
            if(Product_Id != null){
                return NotFound("Không có mã sản phẩm");
            }
            Product product = await _context.products.Where(p => p.MaSanPham == Product_Id).FirstOrDefaultAsync();
            if(product == null){
                return NotFound("Không tìm thấy sản phẩm");
            }
            _context.products.Remove(product);
            await _context.SaveChangesAsync();

            return Page();
        }
    }
}
