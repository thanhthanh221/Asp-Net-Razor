using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.model;
using System.IO;
using Microsoft.AspNetCore.Server;
namespace Razor.Pages{
    public class Shop :PageModel{
        public readonly Razor.model.Context context;
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
        public const int ITEM_PER_PAGE= 6;

        [BindProperty(SupportsGet =true,Name ="p")]
        public int currentPage{set;get;}
        public int countPages{set;get;}
        [BindProperty]
        public List<Product> Product{set;get;}
        public Shop(Razor.model.Context context){
            this.context = context;
        }
        public void OnGet()
        {
            int total_Products = context.products.Count();

            countPages = (int)Math.Ceiling((double)total_Products/ITEM_PER_PAGE);
            if(currentPage < 1){
                currentPage = 1;
            }
            else if(countPages > countPages){
                currentPage = countPages;
            }
            var kq = (from a in context.products orderby a.Name select a).Skip((currentPage-1)*ITEM_PER_PAGE).Take(ITEM_PER_PAGE);
            
  
            Product =  kq.ToList();
            
        }
        public void OnPost(){
             
        }
    }
}