using OrderApplication.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderApplication.IBusinessLogic
{
    public interface IOrderManagementBusinessLogic
    {  
        void Send(Action<string> message);
        void Listen(Action<string> message);        
    }
}
