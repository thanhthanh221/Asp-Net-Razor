using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.model;
using System.Threading;

namespace Razor.Areas.Identity.Pages.Account.Manage
{
    [Authorize] // user phải đăng nhập nếu không nó chuyển đến trang login 
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly Razor.model.Context context;

        public IndexModel(
            UserManager<AppUser> userManager, // thông tin người dùng
            SignInManager<AppUser> signInManager,  // thông tin để đăng nhập
            Razor.model.Context context            // Context dữ liệu
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.context = context;
        }
        [Display(Name ="Tên Tài Khoản")]
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone(ErrorMessage ="{0} sai định dạng")]
            [Display(Name = "Số điện thoại")]
            public string PhoneNumber { get; set; }
            [Display(Name = "Số Tiền đã dùng")]
            public double Money_User {set;get;}
        }

        private async Task LoadAsync(AppUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var TienSuDung = user.Monney_Use;

            Username = userName; // gán biến username  = UserName

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Money_User = TienSuDung
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Không tìm thấy ID :'{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid) // dữ liệu không phù hợp
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Bạn đã update thành công trang cá nhân";
            return RedirectToPage();
        }
    }
}
