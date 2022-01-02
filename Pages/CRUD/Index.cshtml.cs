using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Razor.model;
using System.Threading;

namespace Razor.Pages_CRUD
{
    public class IndexModel : PageModel
    {
        private readonly Razor.model.Context _context;

        public IndexModel(Razor.model.Context context)
        {
            _context = context;
        }

        public IList<Blog> Blog { get;set; }
        public const int ITEM_PER_PAGE= 10;

        [BindProperty(SupportsGet =true,Name ="p")]
        public int currentPage{set;get;}
        public int countPages{set;get;}

        public void OnGet(int STT_ID){
            int totalBlog = _context.blogs.Count();

            countPages = totalBlog/ITEM_PER_PAGE;
            if(countPages<1){
                countPages =1;
            }
            else if(countPages> countPages){
                currentPage = countPages;
            }



            var kq = (from a in _context.blogs orderby a.Created descending select a).Skip((currentPage)*10).Take(ITEM_PER_PAGE);
            Blog = kq.Where((p) => p.ID == STT_ID).ToList();
            if(Blog.Count == 0){
                Blog =  kq.ToList();
            }
        }
    }
    
}
