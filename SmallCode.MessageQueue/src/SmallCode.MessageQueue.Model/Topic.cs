using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.MessageQueue.Model
{
    public class Topic
    {
        public Guid Id { set; get; }

        public string Key { set; get; }

        public DateTime CreateDate { set; get; }

        [ForeignKey("CreateUser")]
        public Guid CreateBy { set; get; }

        public virtual User CreateUser { set; get; }
    }
}
