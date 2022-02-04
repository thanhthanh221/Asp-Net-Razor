using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Razor.model;

namespace Razor.Pages{
    [Authorize]
    public class CartModel :PageModel{
        public CartModel(){
            
        }
    }
}