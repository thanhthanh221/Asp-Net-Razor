using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Razor.model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;


namespace Razor.Admin.User
{
    [Authorize]
    public class SetPasswordModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        // UserManager thiết lập cho tài khoản
        // SignManager thiết lập đăng nhập 
        public SetPasswordModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [BindProperty(SupportsGet = true)]
        public string role_Id{set;get;}

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Mật Khẩu Mới")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Nhập lại mật khẩu")]
            [Compare("NewPassword", ErrorMessage = "Nhập lại không trùng")]
            public string ConfirmPassword { get; set; }
        }
        [BindProperty]
        public AppUser user{set;get;}
        // Dữ Liệu truyền đến chỉ có mật khẩu 
        // User hiện tại đã có id
        public async Task<IActionResult> OnGetAsync()
        {
            if(string.IsNullOrEmpty(role_Id)){
                return NotFound($"không có role_Id để tìm. ");
            }
            user = await _userManager.FindByIdAsync(role_Id); // tìm user đăng nhập bằng role_Id
            if (user == null)
            {
                return NotFound($"không có người dùng role_Id: '{role_Id}'.");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(string.IsNullOrEmpty(role_Id)){
                return NotFound($"không có role_Id để tìm. ");
            }
            user = await _userManager.FindByIdAsync(role_Id); // tìm user đăng nhập bằng role_Id
            if (user == null)
            {
                return NotFound($"không có người dùng role_Id: '{role_Id}'.");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await _userManager.RemovePasswordAsync(user);

            var addPasswordResult = await _userManager.AddPasswordAsync(user, Input.NewPassword); // thêm mật khẩu cho thành viên
            if (!addPasswordResult.Succeeded)
            {
                foreach (var error in addPasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = $"Mật khẩu được thiết lập cho User :{user.UserName}";

            return RedirectToPage("./Index");
        }
    }
}
