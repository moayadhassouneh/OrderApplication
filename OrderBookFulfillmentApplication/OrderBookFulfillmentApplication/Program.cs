using OrderApplication.BusinessLogic;
using OrderApplication.IBusinessLogic;
using System;

namespace OrderFulfilmentApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            IBookFullfilmentBusinessLogic bookFulBL = new BookFullfilmentBusinessLogic();
            bookFulBL.Listen(Console.WriteLine);
        }
    }
}
