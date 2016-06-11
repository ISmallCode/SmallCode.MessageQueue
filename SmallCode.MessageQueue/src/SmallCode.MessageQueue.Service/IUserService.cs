using SmallCode.MessageQueue.Model.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.MessageQueue.Service
{
    public interface IUserService
    {
        ServiceResult Save(MessageQueue.Model.User user);
    }
}
