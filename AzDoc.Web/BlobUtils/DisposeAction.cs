using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzDoc.Web.BlobUtils
{
    public class DisposeAction : IDisposable
    {
        private readonly Action _action;

        public DisposeAction(Action action)
        {
            if (action == null) throw new ArgumentNullException("action");
            _action = action;
        }

        public void Dispose()
        {
            _action();
        }
    }
}