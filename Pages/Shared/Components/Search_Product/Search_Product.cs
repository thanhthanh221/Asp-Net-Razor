using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Razor.model;
using System.Linq;
using System.Collections.Generic;
namespace Component{
    public class Search_Product: ViewComponent{
        private readonly Razor.model.Context context;
        public Search_Product(Razor.model.Context context){
            this.context = context;
        }         
        public IViewComponentResult Invoke(){
            return View("Search_Product");
        }
    }
    
}