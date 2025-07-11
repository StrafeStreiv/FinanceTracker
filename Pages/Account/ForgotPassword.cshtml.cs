using System.ComponentModel.DataAnnotations;
using FinanceTrackerWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FinanceTrackerWeb.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        public ForgotPasswordModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; } = string.Empty;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Не показываем что пользователь не существует
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // Здесь должна быть логика отправки email с токеном сброса
                // Но пока просто редиректим на confirmation page
                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}