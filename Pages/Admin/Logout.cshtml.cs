using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NhaHang.Pages.Admin
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnPostAsync()
        {
            // Xóa toàn bộ session
            HttpContext.Session.Clear();
            // Xóa toàn bộ cookie
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            await HttpContext.SignOutAsync("AdminCookie");
            return RedirectToPage("/Admin/Login");
        }
        public async Task<IActionResult> OnGetAsync()
        {
            // Xóa toàn bộ session
            HttpContext.Session.Clear();
            // Xóa toàn bộ cookie
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            await HttpContext.SignOutAsync("AdminCookie");
            return RedirectToPage("/Admin/Login");
        }
    }
} 