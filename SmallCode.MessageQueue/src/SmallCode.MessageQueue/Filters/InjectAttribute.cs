using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmallCode.MessageQueue.Filters
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class InjectAttribute : Attribute, IBindingSourceMetadata
    {
        public BindingSource BindingSource => BindingSource.Services;
    }
}
