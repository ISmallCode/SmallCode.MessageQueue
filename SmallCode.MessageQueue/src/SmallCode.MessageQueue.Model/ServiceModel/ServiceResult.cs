using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.MessageQueue.Model.ServiceModel
{
    public class ServiceResult
    {
        public bool IsSuccess { set; get; }

        public string ReturnMsg { set; get; }
    }
}
