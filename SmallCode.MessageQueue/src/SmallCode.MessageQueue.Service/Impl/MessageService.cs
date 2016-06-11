using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.MessageQueue.Model;
using SmallCode.Pager;
using Microsoft.EntityFrameworkCore;

namespace SmallCode.MessageQueue.Service.Impl
{
    public class MessageService : BaseService, IMessageService
    {
        private readonly MQContext db;

        public MessageService(MQContext _context)
        {
            db = _context;
        }

        public PagedList<Message> GetListByPage(string key, int pageIndex, int pageSize)
        {
            IQueryable<Message> query = db.Messages.Include(x => x.Topic).AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.Content.Contains(key));
            }
            return query.ToPagedList(pageIndex, pageSize);
        }

        public Message Pull(string key)
        {
            try
            {
                Topic topic = new Topic();
                topic = db.Topices.Where(x => x.Key == key).FirstOrDefault();
                if (topic != null)
                {
                    Message message = new Message();
                    message = db.Messages.OrderBy(x => x.CreateDate).FirstOrDefault(x => x.TopicId == topic.Id);
                    db.Messages.Remove(message);
                    bool flag = db.SaveChanges() > 0;
                    base.IsSuccess = flag;
                    base.ReturnMsg = flag ? "出队列成功" : "出队列失败";
                    return message;
                }
                else
                {
                    base.IsSuccess = false;
                    base.ReturnMsg = "不存在改主题";
                    return null;
                }
            }
            catch (Exception ex)
            {
                base.IsSuccess = false;
                base.ReturnMsg = "出队列出现异常";
                return null;
            }
        }

        public void Push(string key, string content)
        {
            try
            {
                Topic topic = new Topic();
                topic = db.Topices.Where(x => x.Key == key).FirstOrDefault();
                if (topic != null)
                {
                    Message message = new Message();
                    message.Content = content;
                    message.TopicId = topic.Id;
                    message.CreateDate = DateTime.Now;
                    db.Messages.Add(message);
                    bool flag = db.SaveChanges() > 0;
                    base.IsSuccess = flag;
                    base.ReturnMsg = flag ? "入队列成功" : "入队列失败";
                }
                else
                {
                    base.IsSuccess = false;
                    base.ReturnMsg = "不存在该主题";
                }
            }
            catch (Exception ex)
            {
                base.IsSuccess = false;
                base.ReturnMsg = "入队列失败";
            }
        }

        public void Remove(Guid id)
        {
            try
            {
                Message message = db.Messages.FirstOrDefault(x => x.Id == id);
                db.Messages.Remove(message);
                bool flag = db.SaveChanges() > 0;
                base.IsSuccess = flag;
                base.ReturnMsg = flag ? "删除成功" : "删除失败";
            }
            catch (Exception ex)
            {
                base.IsSuccess = false;
                base.ReturnMsg = "删除失败";
            }
        }
    }
}
