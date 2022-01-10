using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Razor.model;
namespace Razor.model{
    public class ThongTinSanPham: PageModel{
        private readonly Razor.model.Context context;
        [BindProperty]
        public List<Product> products{set;get;}
        public ThongTinSanPham(Razor.model.Context context){
            this.context = context;
        }
        public void OnGet([FromRoute]int? x)
        {
            var kq = (from a in context.products where a.MaSanPham == x orderby a.Price select a).FirstOrDefault();
            
            ViewData["Data"] = kq;
        }

    }
}