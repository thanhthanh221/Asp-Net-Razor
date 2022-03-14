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
using Microsoft.AspNetCore.Identity;
using Razor.Areas.Identity.Pages.Account;

namespace Razor.Pages{
    [Authorize]
    public class CartModel :PageModel{
        public class CartItem
        {
            public int quantity {set; get;}
            public Product product {set; get;}
        }
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
        [BindProperty]
        public int Sum{get; set;}
        private readonly Razor.model.Context context;
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        public CartModel(Razor.model.Context context,UserManager<AppUser> userManager,SignInManager<AppUser> signInManager ){
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
            
            
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
            var product = context.products
                .Where (p => p.MaSanPham == productid)
                .FirstOrDefault ();
            if (product == null)
                return NotFound ("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = GetCartItems ();
            var cartitem = cart.Find (p => p.product.MaSanPham == productid);
            if (cartitem != null) {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity++;
            } else {
                //  Thêm mới
                cart.Add (new CartItem () { quantity = 1, product = product });
            }

            // Lưu cart vào Session
            SaveCartSession (cart);
            // Chuyển đến trang hiện thị Cart
            return RedirectToPage();
     

        }
        public IActionResult OnPostDelete(int productid){
            var cart = GetCartItems();
            var cartitem = cart.Find (p => p.product.MaSanPham == productid);
            if (cartitem != null) {
                // Xóa Cart
                cart.Remove(cartitem);
            }

            SaveCartSession (cart);

            return RedirectToPage("./Cart");
        }
        public IActionResult OnPostUpdate(List<CartItem> cartItems){
            if(cartItems == null){
                return NotFound("Không có Dữ liệu");
            }
            SaveCartSession (cartItems);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return RedirectToPage("./Cart");
        }
        public async Task<IActionResult> OnPostCheckOutAsync(){
            AppUser user = await userManager.GetUserAsync(User); // Phương thức tìm người thực thi yêu cầu
            if(user == null){
                return NotFound("Không tìm người dùng");
            }

            List<CartItem> cart = GetCartItems(); // Chuyển SS thành item
            HoaDon New_HoaDon = new HoaDon(){
                Money = Sum+ 20000,
                Id_User = user.Id
                
            };
        

            return RedirectToPage("./Cart");
        }
        public IActionResult OnGet(){
            Sum = GetCartItems().Sum(p => p.product.Price* p.quantity);
            return Page();
        }
    }
}