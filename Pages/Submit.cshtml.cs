using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.model;

namespace Razor.Pages
{
    public class SubmitModel : PageModel
    {
        public readonly Razor.model.Context context;
        [BindProperty]
        public NhanVien nhanVien{set;get;}
        [BindProperty]
        [Display(Name ="Ảnh nhân viên")]
        [DataType(DataType.Upload)]
        public IFormFile[] FileUpLoads{set;get;}
        public SubmitModel(Razor.model.Context context){
            this.context = context;
        }
        public void OnGet()
        {
        }
        public void Post(){
            if(!ModelState.IsValid){
                ViewData["value"] = "Dữ Liệu phù hợp";
                
            }
             ViewData["value"] = "Cập nhật thành công lên database";
             
        }
    }
}
