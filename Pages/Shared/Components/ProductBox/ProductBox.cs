using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Razor.model;
using System.Linq;
using System.Collections.Generic;

namespace Component{
    public class ProductBox : ViewComponent{
        private readonly Razor.model.Context context;
        public ProductBox(Razor.model.Context context){
            this.context = context;
        }
             
        public IViewComponentResult Invoke(string Name){
            var kq = (from a in context.products orderby a.Price select a).ToList();
            if(!string.IsNullOrEmpty(Name)){
                kq.Where(p => p.Name.Contains(Name));
                
            }       
            return View<List<Product>>("ProductBox",kq);
        }      
    }
}