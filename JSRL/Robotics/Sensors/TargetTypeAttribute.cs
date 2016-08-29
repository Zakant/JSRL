using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSRL.Robotics.Sensors
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TargetTypeAttribute : Attribute
    {
        public Type Target { get; protected set; }

        public TargetTypeAttribute(Type type)
        {
            Target = type;
        }
    }
}
