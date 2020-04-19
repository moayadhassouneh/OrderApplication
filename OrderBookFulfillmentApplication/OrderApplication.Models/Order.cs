using Confluent.Kafka;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using static OrderApplication.Common.Enums;

namespace OrderApplication.Models
{
    public class Order
    {
        public decimal OrderID { get; set; }
        public OrderTypeEnums OrderTypeID { get; set; }
        public OrderStatusEnums OrderStatus { get; set; }
        public string OrderRemarks { get; set; }
      
    }
}
