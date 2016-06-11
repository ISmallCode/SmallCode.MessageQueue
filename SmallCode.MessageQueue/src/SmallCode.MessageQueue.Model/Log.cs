using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.MessageQueue.Model
{
    public class Log
    {
        public Guid Id { set; get; }

        public string Thread { set; get; }

        public Level Level { set; get; }

        public string Description { set; get; }

        public string Exception { set; get; }

        public string Ip { set; get; }

        public DateTime CreateDate { set; get; }
    }
    public enum Level { 异常, 记录 }
}
