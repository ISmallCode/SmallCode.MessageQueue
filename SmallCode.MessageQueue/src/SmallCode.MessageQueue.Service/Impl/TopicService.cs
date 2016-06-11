using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.MessageQueue.Model;
using SmallCode.Pager;
using Microsoft.EntityFrameworkCore;
using SmallCode.MessageQueue.Model.ServiceModel;

namespace SmallCode.MessageQueue.Service.Impl
{
    public class TopicService : BaseService, ITopicService
    {
        private readonly MQContext db;

        public TopicService(MQContext _context)
        {
            db = _context;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public PagedList<Topic> GetListByPage(string key, int pageIndex, int pageSize)
        {
            IQueryable<Topic> query = db.Topices.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.Key.Contains(key));
            }
            return query.ToPagedList(pageIndex, pageSize);
        }

        public Topic GetTopicById(Guid? id)
        {
            return db.Topices.FirstOrDefault(x => x.Id == id);
        }

        public void Remove(Guid id)
        {
            try
            {
                Topic topic = db.Topices.FirstOrDefault(x => x.Id == id);
                db.Topices.Remove(topic);
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

        public void Save(Topic topic)
        {
            try
            {
                topic.CreateDate = DateTime.Now;
                db.Topices.Add(topic);
                bool flag = db.SaveChanges() > 0;
                base.IsSuccess = flag;
                base.ReturnMsg = flag ? "保存成功" : "保存失败";
            }
            catch (Exception ex)
            {
                base.IsSuccess = false;
                base.ReturnMsg = "保存异常";
            }

        }



        public void Update(Topic topic)
        {
            try
            {
                Topic oldTopic = db.Topices.FirstOrDefault(x => x.Id == topic.Id);
                oldTopic.Key = topic.Key;
                bool flag = db.SaveChanges() > 0;
                base.IsSuccess = flag;
                base.ReturnMsg = flag ? "修改成功" : "修改失败";
            }
            catch (Exception ex)
            {
                base.IsSuccess = false;
                base.ReturnMsg = "修改异常";
            }
        }
    }
}
