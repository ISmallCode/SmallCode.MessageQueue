using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.MessageQueue.Model.DataModels
{
    public class ReturnJsonResult<T>
    {
        //状态  ok error
        public string Status { set; get; }

        //返回结果
        public T Result { set; get; }

        public string ReturnMsg { set; get; }
    }
}
