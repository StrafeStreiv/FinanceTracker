using FinanceTrackerWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinanceTrackerWeb.Pages.Account
{

    public class LogoutModel : PageModel
    {
        private readonly SignInManager<User>
        _signInManager;

        public LogoutModel(SignInManager<User>
            signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult>
            OnPostAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }
}