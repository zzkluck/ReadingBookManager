using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingBookManager.Physics
{
    public class DrawOutOfRangeException:Exception
    {
        public DrawOutOfRangeException() : base() { }
        public DrawOutOfRangeException(string message) : base(message) { }
        public DrawOutOfRangeException(string message,Exception innerException) : base(message, innerException) { }
    }
}
