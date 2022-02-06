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
        public string SetUpMoney(int value){
            List<string> a = new List<string>();
            string hash = string.Empty;
            string s = value.ToString();
            int temp = 0;
            for (int i = s.Length-1; i >= 0; i--)
            {
                a.Add(s[i].ToString());
                temp++;
                if(temp== 3){
                    temp= 0;
                    a.Add(".");
                }
            }
            if(!a[a.Count-1].Equals(".")){
                hash+= a[a.Count-1];
            }
            for (int i = a.Count-2; i >= 0; i--)
            {
                hash+= a[i];
                            
            }
            return hash;
        }
        private readonly ILogger<IndexModel> _logger;
        private readonly Razor.model.Context context;

        public IndexModel(ILogger<IndexModel> logger, Razor.model.Context context)
        {
            _logger = logger;
            this.context = context;
        }
        [BindProperty(SupportsGet = true)]
        public List<Product> product{set;get;}

        public void OnGet()
        {
            var kq = (from a in context.products orderby a.sold descending select a).ToList();
             
            for (int i = 0; i < 3; i++)
            {
                product.Add(kq[i]);
            }

        }
        public void OnPost(){

        }
    }
}
