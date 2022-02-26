using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Razor.model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Razor.Pages
{
    [Authorize(Policy = "ChinhSach3")]
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        private readonly Razor.model.Context context;

        public PrivacyModel(ILogger<PrivacyModel> logger,Razor.model.Context context)
        {
            _logger = logger;
            this.context = context;
        }
        [BindProperty]
        public IList<Product> products{set;get;}
        public void OnGet()
        {
            var kq = from a in context.products select a;
            products = kq.ToList();
        }
    }
}
