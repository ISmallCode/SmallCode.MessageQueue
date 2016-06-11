using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.MessageQueue.Model;
using SmallCode.MessageQueue.Infrastructure;
using SmallCode.MessageQueue.Model.ServiceModel;

namespace SmallCode.MessageQueue.Service.Impl
{
    public class UserService : IUserService
    {
        private readonly MQContext db;

        public UserService(MQContext _context)
        {
            db = _context;
        }

        public ServiceResult Save(User user)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                user.CreateDate = DateTime.Now;
                user.Password = user.Password.ToMD5Hash();
                db.Users.Add(user);

                result.IsSuccess = db.SaveChanges() > 0;
                result.ReturnMsg = result.IsSuccess ? "保存成功" : "保存失败";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.ReturnMsg = "保存出现异常";
            }
            return result;
        }
    }
}
