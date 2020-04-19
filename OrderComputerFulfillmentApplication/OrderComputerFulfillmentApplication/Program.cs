using OrderApplication.BusinessLogic;
using OrderApplication.IBusinessLogic;
using System;

namespace OrderComputerFulfilmentApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            IComputerFulfillmentBusinessLogic computerFulBL = new ComputerFulfillmentBusinessLogic();
            computerFulBL.Listen(Console.WriteLine);
        }
    }
}
