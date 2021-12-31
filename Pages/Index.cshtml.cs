using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Razor.model;

namespace Razor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly Context context;

        public IndexModel(ILogger<IndexModel> logger, Context context)
        {
            _logger = logger;
            this.context = context;
        }

        public void OnGet()
        {
            var kq = (from blog in context.blogs orderby blog.Created descending select blog).ToList();

            ViewData["Post"] = kq;

        }
    }
}
