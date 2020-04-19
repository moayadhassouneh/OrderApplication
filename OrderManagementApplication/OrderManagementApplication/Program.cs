using OrderApplication.BusinessLogic;
using OrderApplication.IBusinessLogic;
using System;
using System.Threading.Tasks;

namespace OrderManagementApplication
{
    class Program
    {
        static void Main(string[] args)
        {            
            Console.WriteLine("Sending Orders");
            IOrderManagementBusinessLogic orderMangBL = new OrderManagementBusinessLogic();
            Task task2 = Task.Factory.StartNew(() => orderMangBL.Listen(Console.WriteLine));
            Task task1 = Task.Factory.StartNew(() => orderMangBL.Send(Console.WriteLine));
            Task.WaitAll(task1, task2);
        }
    }
}
