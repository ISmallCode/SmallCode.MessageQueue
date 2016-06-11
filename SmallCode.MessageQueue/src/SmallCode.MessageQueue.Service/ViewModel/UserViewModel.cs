using SmallCode.MessageQueue.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.MessageQueue.Service.ViewModel
{
    public class UserViewModel : BaseViewModel
    {
        public Guid Id { set; get; }

        public string UserName { set; get; }

        public string Password { set; get; }

        public DateTime CreateDate { set; get; }

        public UserViewModel()
        { }

        public UserViewModel(User model)
        {
            this.Id = model.Id;
            this.UserName = model.UserName;
            this.Password = model.Password;
            this.CreateDate = model.CreateDate;
        }
    }
}
