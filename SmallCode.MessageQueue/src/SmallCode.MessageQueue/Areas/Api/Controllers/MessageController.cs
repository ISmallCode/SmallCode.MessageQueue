using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System.Web.Http;
using SmallCode.MessageQueue.Model.DataModels;
using System.Net.Http;
using System.Net;
using SmallCode.MessageQueue.Filters;
using SmallCode.MessageQueue.Service;
using SmallCode.MessageQueue.Model.DataModels.Api;
using SmallCode.MessageQueue.Model;
using System.Threading;
using SmallCode.MessageQueue.Models;

namespace SmallCode.MessageQueue.Areas.Api.Controllers
{
    public class MessageController : BaseController
    {


        [Inject]
        public IMessageService messageService { set; get; }

        [HttpPost]
        public IActionResult Push([FromBody] MessageRequestModel model)
        {
            ReturnResult result = new ReturnResult();
            messageService.Push(model.Topic, model.Content);
            result.ReturnMsg = messageService.ReturnMsg;
            result.Status = messageService.IsSuccess ? "ok" : "error";
            return Json(result);
        }

        [HttpPost]
        public IActionResult Pull([FromBody] MessageRequestModel model)
        {

            ReturnJsonResult<Message> result = new ReturnJsonResult<Message>();
            LockControl.mutex.WaitOne();
            Message message = messageService.Pull(model.Topic);
            LockControl.mutex.ReleaseMutex();
            result.ReturnMsg = messageService.ReturnMsg;
            result.Status = messageService.IsSuccess ? "ok" : "error";
            result.Result = message;
            return Json(result);
        }

    }
}
