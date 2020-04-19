using System;
using System.Collections.Generic;
using System.Text;

namespace OrderApplication.IBusinessLogic
{
    public interface IBookFullfilmentBusinessLogic
    {
        void Listen(Action<string> message);
    }
}
