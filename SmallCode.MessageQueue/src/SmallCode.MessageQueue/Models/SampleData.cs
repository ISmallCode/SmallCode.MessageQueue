using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SmallCode.MessageQueue.Model;
using SmallCode.MessageQueue.Service;

namespace SmallCode.MessageQueue.Models
{
    public class SampleData
    {
        public async static Task InitDB(IServiceProvider service)
        {
            var db = service.GetService<MQContext>();
            var userService = service.GetService<IUserService>();

            if (db.Database != null && db.Database.EnsureCreated())
            {
                User user = new User
                {
                    UserName = "admin",
                    Password = "123456",
                };

                userService.Save(user);
            }
        }
    }
}
