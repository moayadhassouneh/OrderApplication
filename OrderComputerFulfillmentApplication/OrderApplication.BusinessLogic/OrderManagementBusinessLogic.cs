using Confluent.Kafka;
using System.Net;
using OrderApplication.Common;
using OrderApplication.IBusinessLogic;
using OrderApplication.Models;
using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace OrderApplication.BusinessLogic
{
    public class OrderManagementBusinessLogic : IOrderManagementBusinessLogic
    {       

        public void Send(Action<string> message)
        {           
            string jsonStr = File.ReadAllText(Directory.GetCurrentDirectory() + "/TestingFiles/Orders.json");
            List<Order> orders = JsonConvert.DeserializeObject<List<Order>>(jsonStr);

            var config = new ProducerConfig
            {
                BootstrapServers = Constants.KafkaServerURL,
                ClientId = Dns.GetHostName(),

            };

            orders.ForEach(order =>
            {
                string orderStr = JsonConvert.SerializeObject(order);
                string msg = string.Format("Send order {0}", order.OrderID.ToString());

                message(msg);
                using (var producer = new ProducerBuilder<Null, String>(config).Build())
                {    
                    producer.Produce(Constants.OrderTopicName, new Message<Null, string> { Value = orderStr }, handler);
                }
            });
        }

        public static void handler(DeliveryReport<Null, string> deliveryReport)
        {
            string orderStr = deliveryReport.Value;
        }

        public void Listen(Action<string> message)
        {            
           var config = new ConsumerConfig()
            {
                BootstrapServers = Constants.KafkaServerURL,
                GroupId = Constants.BookGroupName,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, String>(config).Build())
            {
                bool cancelled = false;

                consumer.Subscribe(Constants.OrderStatusTopicName);

                while (!cancelled)
                {
                    var consumeResult = consumer.Consume(Constants.CancellationToken);
                    if (consumeResult != null && consumeResult.Message != null)
                    {
                        Order orderObj = JsonConvert.DeserializeObject<Order>(consumeResult.Message.Value);
                        string msg = string.Format("Order {0} current status is {1}", orderObj.OrderID.ToString(), 
                                                orderObj.OrderStatus.ToString());
                        message(msg);
                    }                   
                }

                consumer.Close();
            }
        }
    }
}
