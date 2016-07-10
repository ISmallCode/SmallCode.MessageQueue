using Microsoft.EntityFrameworkCore;
using SmallCode.MessageQueue.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace SmallCode.MessageQueue.Model
{
    public class MQContext : DbContext
    {

        public MQContext(DbContextOptions option) : base(option)
        {
        }

        public DbSet<Topic> Topices { set; get; }

        public DbSet<Message> Messages { set; get; }

        public DbSet<User> Users { set; get; }

        public DbSet<Log> Logs { set; get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            //builder.Entity<Topic>(e =>
            //{
            //    e.HasIndex(x => x.Id);
            //});

            //builder.Entity<Message>(e =>
            //{
            //    e.HasIndex(x => x.Id);
            //    e.HasKey(x => x.TopicId).HasName("Topic");
            //});
            builder.HasPostgresExtension("uuid-ossp");
            base.OnModelCreating(builder);
        }

    }
}
