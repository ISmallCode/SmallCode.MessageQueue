using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.MessageQueue.Model.DataModels.Api
{
    public class MessageRequestModel
    {
        public string Topic { set; get; }

        public string Content { set; get; }
    }
}
