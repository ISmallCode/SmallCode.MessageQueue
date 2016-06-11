using SmallCode.MessageQueue.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.MessageQueue.Service.ViewModel
{
    public class TopicViewModel : BaseViewModel
    {
        public Guid Id { set; get; }

        public string Key { set; get; }

        public DateTime CreateDate { set; get; }

        public TopicViewModel() { }

        public TopicViewModel(Topic model)
        {
            this.Id = model.Id;
            this.Key = model.Key;
            this.CreateDate = model.CreateDate;
        }
    }
}
