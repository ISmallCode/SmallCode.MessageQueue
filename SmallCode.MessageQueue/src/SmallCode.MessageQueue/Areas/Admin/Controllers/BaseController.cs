using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using SmallCode.MessageQueue.Model;
using Microsoft.Extensions.DependencyInjection;
using SmallCode.MessageQueue.Service;
using SmallCode.MessageQueue.Service.Impl;
using System.Threading;

namespace SmallCode.MessageQueue.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class BaseController : Controller
    {
        public User CurrentUser { set; get; }

        public IDictionary<string, object> Parameters { set; get; }

        public MQContext DB { get { return HttpContext.RequestServices.GetService<MQContext>(); } }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            ILogService logService = HttpContext.RequestServices.GetService<ILogService>();

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                CurrentUser = DB.Users.Where(x => x.UserName == HttpContext.User.Identity.Name).SingleOrDefault();
                ViewBag.UserCurrent = CurrentUser;
            }
            Parameters = new Dictionary<string, object>();

           
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {

        }


    }
}
