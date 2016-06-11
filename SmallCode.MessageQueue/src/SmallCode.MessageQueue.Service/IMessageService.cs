using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.MessageQueue.Model;
using SmallCode.Pager;

namespace SmallCode.MessageQueue.Service
{
    public interface IMessageService : IBaseService
    {
        PagedList<Message> GetListByPage(string key, int pageIndex, int pageSize);
        void Remove(Guid id);
        void Push(string topic, string content);
        Message Pull(string topic);
    }
}
