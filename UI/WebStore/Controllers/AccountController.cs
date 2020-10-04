using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels.Identity;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ILogger<AccountController> logger;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        #region Процесс регистрации нового пользователя

        public IActionResult Register() => View(new RegisterUserViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            logger.LogInformation("Начало регистрации нового пользователя {0}", model.UserName);

            var user = new User
            {
                UserName = model.UserName
            };

            var registrationResult = await userManager.CreateAsync(user, model.Password);
            if (registrationResult.Succeeded)
            {
                logger.LogInformation("Пользователь {0} успешно зарегистрирован", user.UserName);

                await userManager.AddToRoleAsync(user, Role.User);

                logger.LogInformation("Пользователь {0} наделен ролью {1}", user.UserName, Role.User);

                await signInManager.SignInAsync(user, false);

                logger.LogInformation("Пользователь {0} автоматически вошел в систему после регистрации", user.UserName);

                return RedirectToAction("Index", "Home");
            }

            logger.LogWarning("Ошибка при регистрации нового пользователя {0}\r\n{1}",
                model.UserName,
                string.Join(Environment.NewLine, registrationResult.Errors.Select(error => error.Description)));

            foreach (var error in registrationResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }

        #endregion

        #region Процесс входа пользователя в систему

        public IActionResult Login(string returnUrl) => View(new LoginViewModel { ReturnUrl = returnUrl });

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var loginResult = await signInManager.PasswordSignInAsync(
                model.UserName,
                model.Password,
                model.RememberMe,
                false);

            logger.LogInformation("Попытка входа пользователя {0} в систему", model.UserName);


            if (loginResult.Succeeded)
            {
                logger.LogInformation("Пользователь {0} успешно вошёл в систему", model.UserName);

                if (Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            logger.LogWarning("Ошибка имени пользователя или пароля при входе {0}", model.UserName);

            ModelState.AddModelError(string.Empty, "Неверное имя пользвателя или пароль!");

            return View(model);
        }

        #endregion

        public async Task<IActionResult> Logout()
        {
            var userName = User.Identity.Name;
            await signInManager.SignOutAsync();

            logger.LogInformation("Пользователь {0} вышел из системы", userName);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied() => View();
    }
}
