using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using SmallCode.MessageQueue.Filters;
using SmallCode.MessageQueue.Service;
using SmallCode.MessageQueue.Model;

namespace SmallCode.MessageQueue.Controllers
{
    public class AccountController : Controller
    {
        [Inject]
        public IUserService userService { set; get; }

        /// <summary>
        /// 返回登陆页面 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 执行登录
        /// </summary>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string UserName, string Password)
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                ModelState.AddModelError("", "用户名或者密码不能为空");
            }
            else
            {
                User user = userService.Login(UserName, Password);
                if (userService.IsSuccess)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, UserName));

                    await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                              new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)));

                    return Redirect("/Admin/Home/Index");
                }
                else
                {
                    ModelState.AddModelError("", "用户名或者密码错误");
                }
            }
            return View();
        }

        /// <summary>
        /// 注销登陆
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> LogOff()
        {
            await HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Home/Index");
        }




    }
}
