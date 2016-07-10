using SmallCode.MessageQueue.Model;
using SmallCode.MessageQueue.Model.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.Pager;

namespace SmallCode.MessageQueue.Service
{
    public interface IUserService:IBaseService
    {
        void Save(User user);
        User Login(string username, string password);
        PagedList<User> GetListByPage(string userName, int pageIndex, int pageSize);
        User GetUserById(Guid? id);
        void Update(User user);
        void Remove(Guid id);
    }
}
