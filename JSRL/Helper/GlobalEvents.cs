using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSRL.Helper
{
    public static class GlobalEvents
    {
        public static event Action ApplicationShutdown;

        public static void RaiseApplicationShutdown()
        {
            ApplicationShutdown?.Invoke();
        }
    }
}
