using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmallCode.MessageQueue.Model;
using SmallCode.MessageQueue.Service;
using SmallCode.MessageQueue.Filters;
using SmallCode.Pager;
using SmallCode.MessageQueue.Service.ViewModel;
using SmallCode.MessageQueue.Model.DataModels;

namespace SmallCode.MessageQueue.Areas.Admin.Controllers
{
    public class TopicController : BaseController
    {
        [Inject]
        public ITopicService service { set; get; }

        public IActionResult Index(string Key, int PageIndex = 1, int PageSize = 20)
        {
            PagedList<Topic> data = service.GetListByPage(Key, PageIndex, PageSize);
            return View(data);
        }


        [HttpGet]
        public IActionResult Edit(Guid? Id)
        {
            TopicViewModel model = null;
            if (Id.HasValue)
            {
                Topic topic = service.GetTopicById(Id);
                model = new TopicViewModel(topic);
                model.IsNew = false;
            }
            else
            {
                model = new TopicViewModel();
                model.IsNew = true;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(TopicViewModel model)
        {
            if (model.IsNew)
            {
                Topic topic = new Topic();
                topic.Key = model.Key;
                topic.CreateBy = CurrentUser.Id;
                service.Save(topic);
            }
            else
            {
                Topic topic = new Topic();
                topic = service.GetTopicById(model.Id);
                topic.Key = topic.Key;
                service.Update(topic);
            }

            if (service.IsSuccess)
            {
                return Redirect("/Admin/Topic/Index");
            }
            else
            {
                ModelState.AddModelError("", service.ReturnMsg);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            service.Remove(id);
            ReturnResult model = new ReturnResult();
            model.Status = service.IsSuccess ? "ok" : "error";
            model.ReturnMsg = service.ReturnMsg;
            return Json(model);
        }

    }
}
