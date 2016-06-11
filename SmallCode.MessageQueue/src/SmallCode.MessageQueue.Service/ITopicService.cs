using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.MessageQueue.Model;
using SmallCode.MessageQueue.Model.ServiceModel;

namespace SmallCode.MessageQueue.Service
{
    public interface ITopicService:IBaseService
    {
        SmallCode.Pager.PagedList<Topic> GetListByPage(string key, int pageIndex, int pageSize);
        void Remove(Guid id);
        Topic GetTopicById(Guid? id);
        void Save(Topic topic);
        void Update(Topic topic);
    }
}
