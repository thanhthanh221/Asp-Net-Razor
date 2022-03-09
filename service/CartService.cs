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

namespace Razor.Service{
    public class CartService :PageModel{
        // Key lưu chuỗi json của Cart
        public const string CARTKEY = "cart";

        // Lấy cart từ Session (danh sách Product)
        List<Product> GetProducts () {

            var session = HttpContext.Session;
            string jsoncart = session.GetString (CARTKEY);
            if (jsoncart != null) {
                return JsonConvert.DeserializeObject<List<Product>> (jsoncart);
            }
            return new List<Product> ();
        }

        // Xóa cart khỏi session
        void ClearCart () {
            var session = HttpContext.Session;
            session.Remove (CARTKEY);
        }

        // Lưu Cart (Danh sách Product) vào session
        void SaveCartSession (List<Product> ls) {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject (ls);
            session.SetString (CARTKEY, jsoncart);
        }

    }
}