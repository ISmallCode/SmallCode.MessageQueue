using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.MessageQueue.Model;
using SmallCode.MessageQueue.Infrastructure;
using SmallCode.MessageQueue.Model.ServiceModel;

namespace SmallCode.MessageQueue.Service.Impl
{
    public class UserService : BaseService, IUserService
    {
        private readonly MQContext db;

        public UserService(MQContext _context)
        {
            db = _context;
        }

        public User Login(string username, string password)
        {
            User user = new User();
            password = password.ToMD5Hash();
            user = db.Users.Where(x => x.UserName == username).FirstOrDefault();
            if (user != null)
            {
                bool result = user.Password.Equals(password);
                if (result)
                {
                    base.IsSuccess = true;
                    base.ReturnMsg = "登录成功";
                }
                else
                {
                    user = null;
                    base.IsSuccess = false;
                    base.ReturnMsg = "密码错误";
                }
            }
            else
            {
                base.IsSuccess = false;
                base.ReturnMsg = "用户名错误";
            }
            return user;
        }

        public void Save(User user)
        {
            try
            {
                user.CreateDate = DateTime.Now;
                user.Password = user.Password.ToMD5Hash();
                db.Users.Add(user);

                base.IsSuccess = db.SaveChanges() > 0;
                base.ReturnMsg = base.IsSuccess ? "保存成功" : "保存失败";
            }
            catch (Exception ex)
            {
                base.IsSuccess = false;
                base.ReturnMsg = "保存出现异常";
            }
        }
    }
}
