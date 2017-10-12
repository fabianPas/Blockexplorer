using System;
using System.Collections.Generic;
using System.Text;

namespace Blockexplorer.Services
{
    public class ApiCache<T>
    {
        public DateTime LastQuery { get; set; } = DateTime.MinValue;
        public T Model { get; set; }
    }
}
