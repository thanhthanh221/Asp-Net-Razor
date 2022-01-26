using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Razor.model;

namespace Razor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Razor.model.Context context;

        public IndexModel(ILogger<IndexModel> logger, Razor.model.Context context)
        {
            _logger = logger;
            this.context = context;
        }
        [BindProperty(SupportsGet = true)]
        public List<Product> product{set;get;}

        public void OnGet(string Search_Name)
        {
            var kq = (from a in context.products select a).ToList();
            if(!string.IsNullOrEmpty(Search_Name)){
                ViewData["Post"]= kq.Where(p =>p.Name.Contains(Search_Name)).ToList();

            }
        }
        public void OnPost(){

        }
    }
}
