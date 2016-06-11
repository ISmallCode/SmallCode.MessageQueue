using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.MessageQueue.Model;
using SmallCode.MessageQueue.Infrastructure;
using SmallCode.MessageQueue.Model.ServiceModel;
using SmallCode.Pager;
using Microsoft.EntityFrameworkCore;

namespace SmallCode.MessageQueue.Service.Impl
{
    public class UserService : BaseService, IUserService
    {
        private readonly MQContext db;

        public UserService(MQContext _context)
        {
            db = _context;
        }

        public PagedList<User> GetListByPage(string userName, int pageIndex, int pageSize)
        {
            IQueryable<User> query = db.Users.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(userName))
            {
                query = query.Where(x => x.UserName.Contains(userName));
            }
            return query.ToPagedList(pageIndex, pageSize);
        }

        public User GetUserById(Guid? id)
        {
            return db.Users.FirstOrDefault(x => x.Id == id);
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

        public void Remove(Guid id)
        {
            try
            {
                User user = new User();
                user = db.Users.FirstOrDefault(x => x.Id == id);
                db.Users.Remove(user);
                bool flag = db.SaveChanges() > 0;
                base.IsSuccess = flag;
                base.ReturnMsg = flag ? "删除成功" : "删除失败";
            }
            catch (Exception ex)
            {
                base.IsSuccess = false;
                base.ReturnMsg = "删除异常";
            }

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

        public void Update(User user)
        {
            try
            {
                User oldUser = new User();
                oldUser = db.Users.FirstOrDefault(x => x.Id == user.Id);
                oldUser.UserName = user.UserName;
                base.IsSuccess = db.SaveChanges() > 0;
                base.ReturnMsg = base.IsSuccess ? "修改成功" : "修改失败";
            }
            catch (Exception ex)
            {
                base.IsSuccess = false;
                base.ReturnMsg = "修改异常";
            }
        }
    }
}
