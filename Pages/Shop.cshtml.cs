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
        [BindProperty]
        [Display(Name ="Ảnh nhân viên")]
        [DataType(DataType.Upload)]
        public IFormFile FileUpLoads{set;get;}
        public Shop(Razor.model.Context context){
            this.context = context;
        }
        public void OnGet()
        {
        }
        public void OnPost(){
             
        }
    }
}