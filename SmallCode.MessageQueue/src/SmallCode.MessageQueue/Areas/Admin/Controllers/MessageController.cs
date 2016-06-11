using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.MessageQueue.Service;
using SmallCode.MessageQueue.Filters;
using SmallCode.MessageQueue.Model;
using SmallCode.Pager;
using SmallCode.MessageQueue.Model.DataModels;

namespace SmallCode.MessageQueue.Areas.Admin.Controllers
{
    public class MessageController : BaseController
    {
        [Inject]
        public IMessageService service { set; get; }

        public IActionResult Index(string Key, int PageIndex = 1, int PageSize = 20)
        {
            PagedList<Message> data = service.GetListByPage(Key, PageIndex, PageSize);
            return View(data);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            service.Remove(id);
            ReturnResult model = new ReturnResult();
            model.Status = service.IsSuccess ? "success" : "fail";
            model.ReturnMsg = service.ReturnMsg;
            return Json(model);
        }
    }
}
