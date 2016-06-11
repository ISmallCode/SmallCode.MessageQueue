using SmallCode.MessageQueue.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.MessageQueue.Service
{
    public interface ILogService : IBaseService
    {
        void Save(Log log);
    }
}
