using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.MessageQueue.Model.DataModels
{
    public class ReturnResult
    {
        //状态  ok error
        public string Status { set; get; }

        public string ReturnMsg { set; get; }
    }
}
