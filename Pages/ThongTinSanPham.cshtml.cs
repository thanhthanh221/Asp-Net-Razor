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