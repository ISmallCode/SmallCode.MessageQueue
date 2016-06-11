using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmallCode.MessageQueue.Model;
using System.Threading;

namespace SmallCode.MessageQueue.Service.Impl
{
    public class LogService : BaseService, ILogService
    {
        private readonly MQContext db;
        public LogService(MQContext _context)
        {
            db = _context;
        }

        public void Save(Log log)
        {
            try
            {
                log.CreateDate = DateTime.Now;
                log.Thread = Thread.CurrentThread.ManagedThreadId.ToString();
                db.Logs.Add(log);
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
    }
}
