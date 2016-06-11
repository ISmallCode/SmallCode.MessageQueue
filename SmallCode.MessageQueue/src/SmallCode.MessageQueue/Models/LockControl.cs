using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmallCode.MessageQueue.Models
{
    public class LockControl
    {
        public static int count = 0;

        public static Mutex mutex = new Mutex();
    }
}
