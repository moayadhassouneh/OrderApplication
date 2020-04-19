using System;
using System.Collections.Generic;
using System.Text;

namespace OrderApplication.IBusinessLogic
{
    public interface IComputerFulfillmentBusinessLogic
    {
        void Listen(Action<string> message);
    }
}
