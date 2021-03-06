﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SmallCode.MessageQueue.Model
{
    public class Message
    {
        public Guid Id { set; get; }

        public string Content { set; get; }

        [ForeignKey("Topic")]
        public Guid TopicId { set; get; }

        public DateTime CreateDate { set; get; }

        public virtual Topic Topic { set; get; }
    }
}
