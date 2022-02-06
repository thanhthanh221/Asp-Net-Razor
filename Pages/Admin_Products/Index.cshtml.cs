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

        public IList<Product> Product { get;set; }
        public const int ITEM_PER_PAGE= 3;

        [BindProperty(SupportsGet =true,Name ="p")]
        public int currentPage{set;get;}
        public int countPages{set;get;}
        public void OnGet(string name_Product)
        {
            int total_Products = _context.products.Count();

            countPages = total_Products/ITEM_PER_PAGE;
            if(currentPage < 1){
                currentPage = 1;
            }
            else if(countPages > countPages){
                currentPage = countPages;
            }
            var kq = (from a in _context.products orderby a.Name select a).Skip((currentPage-1)*ITEM_PER_PAGE).Take(ITEM_PER_PAGE);
            if(string.IsNullOrEmpty(name_Product)){
                Product = kq.ToList();
            }
            else{
                Product = (from a in _context.products where a.Name.ToLower().Contains(name_Product.ToLower()) select a).ToList();
                if(Product.Count == 0){
                    Product =  kq.ToList();
                }
            }
        }
    }
}
