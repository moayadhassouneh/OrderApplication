using System;
using System.Collections.Generic;
using System.Text;

namespace OrderApplication.Common
{
    public class Enums
    {
        public enum OrderStatusEnums
        {
            Initial = 1,
            NEW = 2,
            SHIPPED = 3,
            DELIVERED = 4
        }

        public enum OrderTypeEnums
        {
            None = 0,
            Book = 1,
            Computer = 2
        }
    }
}
