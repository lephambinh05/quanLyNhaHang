using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace NhaHang.Pages.Auth
{
    public class ForgotPasswordModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = "";

        public bool Success { get; set; } = false;
        public string ErrorMessage { get; set; } = "";

        public void OnGet()
        {
            // Handle GET request
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Vui lòng nhập email hợp lệ";
                return Page();
            }

            // TODO: Implement actual password reset logic
            // For now, we'll just show a success message
            Success = true;
            
            // In a real application, you would:
            // 1. Check if the email exists in the database
            // 2. Generate a password reset token
            // 3. Send an email with the reset link
            // 4. Store the token in the database with an expiration time

            return Page();
        }
    }
} 