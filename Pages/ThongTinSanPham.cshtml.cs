using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Razor.model;
using System.Threading;
namespace Razor.model{
    public class ThongTinSanPhamModel: PageModel{
        private readonly Razor.model.Context context;
        [BindProperty]
        public Product product{set;get;}
        public ThongTinSanPhamModel(Razor.model.Context context){
            this.context = context;
        }
        public void OnGet(int? id)
        {
            var kq = (from a in context.products where a.MaSanPham == id orderby a.Price select a).FirstOrDefault();
            if(kq != null){
                product = kq;
            }
            else
            {
               
            }
            
        }
        public void OnPost(int? id){
            var kq = (from a in context.products where a.MaSanPham == id orderby a.Price select a).FirstOrDefault();
            if(kq != null){
                product = kq;
            }
            else
            {
               
            }

        }

    }
}