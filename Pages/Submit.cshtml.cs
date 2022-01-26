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

namespace Razor.Pages
{
    public class SubmitModel : PageModel
    {
        private IHostingEnvironment _environment;
        public readonly Razor.model.Context context;
        [BindProperty]
        public NhanVien nhanVien{set;get;}
        [BindProperty]
        [Display(Name ="Ảnh nhân viên")]
        [DataType(DataType.Upload)]
        public IFormFile FileUpLoads{set;get;}
        public SubmitModel(Razor.model.Context context,IHostingEnvironment environment){
            this.context = context;
            _environment = environment;
        }
        public void OnGet()
        {
        }
        public void OnPost(){
            if (ModelState.IsValid)
            {
                if(FileUpLoads!= null){
                                 
                    var file = Path.Combine (_environment.ContentRootPath,"wwwroot", "img", FileUpLoads.FileName);
                    using (var fileStream = new FileStream (file, FileMode.Create)) {
                        FileUpLoads.CopyTo(fileStream);
                        var leng = new byte[fileStream.Length];
                        fileStream.Read(leng,0,leng.Length);
                        nhanVien.Anh = leng;
                    }
                }                                  
                                                             
                context.NhanViens.Add(nhanVien);
                context.SaveChanges();
                ViewData["Value"] ="Đã Up Dữ Liệu Lên Database";
            }
            else{
                ViewData["value"] ="Dữ Liệu Không Phù Hợp";
            }
             
        }
    }
}

