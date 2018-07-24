using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoShare.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple =true)]
   public class ModifyAttribute:Attribute
    {
    }
}
