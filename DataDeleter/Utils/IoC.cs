using SimpleInjector;
using System;
using System.Collections.Generic;

using System.Text;

namespace DataDeleter.Utils
{
    public static class IoC
    {
        public static Container Container { get; set; } = new();
    }
}
