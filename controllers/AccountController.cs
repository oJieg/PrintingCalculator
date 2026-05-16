using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using printing_calculator.Models;
using printing_calculator.ViewModels;
using System.Security.Claims;

namespace printing_calculator.controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SettingAuthorization _settingAuthorization;

        public AccountController(IOptions<SettingAuthorization> settingAuthorization)
        {
            _settingAuthorization = settingAuthorization.Value;
        }

        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
                return View(model);

            // Проверка учётных данных (заглушка – замените на свою логику)
            if (model.Username == _settingAuthorization.Login && model.Password == _settingAuthorization.Password)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Username),
                new Claim(ClaimTypes.Role, "Administrator"),
                new Claim("Department", "IT")
            };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, // если был чекбокс "Запомнить"
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    authProperties);

                // Перенаправление на изначально запрошенную страницу или на главную
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                else
                    return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Неверное имя пользователя или пароль.");
            return View(model);
        }
    }
}
