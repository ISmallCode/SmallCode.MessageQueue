using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.MessageQueue.Service;
using SmallCode.MessageQueue.Filters;
using SmallCode.MessageQueue.Model;
using SmallCode.Pager;
using SmallCode.MessageQueue.Service.ViewModel;
using SmallCode.MessageQueue.Model.DataModels;

namespace SmallCode.MessageQueue.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        [Inject]
        public IUserService userService { set; get; }

        [HttpGet]
        public IActionResult Index(string UserName, int PageIndex = 1, int PageSize = 20)
        {
            PagedList<User> data = userService.GetListByPage(UserName, PageIndex, PageSize);
            return View(data);
        }

        [HttpGet]
        public IActionResult Edit(Guid? Id)
        {
            UserViewModel model = null;
            if (Id.HasValue)
            {
                User user = userService.GetUserById(Id);
                model = new UserViewModel(user);
                model.IsNew = false;
            }
            else
            {
                model = new UserViewModel();
                model.IsNew = true;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(UserViewModel model)
        {
            if (model.IsNew)
            {
                User user = new User();
                user.UserName = model.UserName;
                user.Password = model.Password;
                userService.Save(user);
            }
            else
            {
                User user = new User();
                user = userService.GetUserById(model.Id);
                user.UserName = model.UserName;
                user.Password = model.Password;
                userService.Update(user);
            }

            if (userService.IsSuccess)
            {
                return Redirect("/Admin/User/Index");
            }
            else
            {
                ModelState.AddModelError("", userService.ReturnMsg);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            userService.Remove(id);
            ReturnResult model = new ReturnResult();
            model.Status = userService.IsSuccess ? "success" : "fail";
            model.ReturnMsg = userService.ReturnMsg;
            return Json(model);
        }

    }
}
