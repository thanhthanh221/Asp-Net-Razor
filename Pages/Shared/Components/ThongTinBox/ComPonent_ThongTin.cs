using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Razor.model;
using System.Linq;
using System.Collections.Generic;
namespace Component{
    public class ComPonent_ThongTin :ViewComponent{
        private readonly Razor.model.Context context;
        public ComPonent_ThongTin(Razor.model.Context context){
            this.context = context;
        }
        public IViewComponentResult Invoke(){
            var kq = (from a in context.products orderby a.Price select a).ToList();
            Dictionary<int,Product> Hash = new Dictionary<int, Product>();
            foreach (var item in kq){
                if(!Hash.ContainsKey(item.MaSanPham)){
                    Hash.Add(item.MaSanPham,item);
                }               
            }
            return View<Dictionary<int,Product>>("ThongTin",Hash);
        }      

    }
}