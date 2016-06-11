using SmallCode.MessageQueue.Model;
using SmallCode.MessageQueue.Model.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.MessageQueue.Service
{
    public interface IUserService:IBaseService
    {
        void Save(MessageQueue.Model.User user);
        User Login(string username, string password);
    }
}
