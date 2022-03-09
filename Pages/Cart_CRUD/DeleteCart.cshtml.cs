using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Razor.model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Razor.Pages.Cart{
    public class DeleteCart: PageModel{
        public class CartItem
        {
            public int quantity {set; get;}
            public Product product {set; get;}
        }
        
        private readonly Razor.model.Context context;
        public DeleteCart(Razor.model.Context context){
            this.context = context;
            
        }
          // Key lưu chuỗi json của Cart
        public const string CARTKEY = "cart";

        // Lấy cart từ Session (danh sách CartItem)
        public List<CartItem> GetCartItems () {

            ISession session = HttpContext.Session;
            string jsoncart = session.GetString (CARTKEY);
            if (jsoncart != null) {
                return JsonConvert.DeserializeObject<List<CartItem>> (jsoncart);
            }
            return new List<CartItem> ();
        }

        // Xóa cart khỏi session
        void ClearCart () {
            var session = HttpContext.Session;
            session.Remove (CARTKEY);
        }

        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession (List<CartItem> ls) {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject (ls);
            session.SetString (CARTKEY, jsoncart);
        }
        public IActionResult OnPost(int productid){
            var cart = GetCartItems();
            var cartitem = cart.Find (p => p.product.MaSanPham == productid);
            if (cartitem != null) {
                // Đã tồn tại, tăng thêm 1
                cart.Remove(cartitem);
            }

            SaveCartSession (cart);

            return RedirectToPage("./Cart");
     

        }
        public IActionResult OnGet(){
            return RedirectToPage("./Cart");    
        }
        
    }
}