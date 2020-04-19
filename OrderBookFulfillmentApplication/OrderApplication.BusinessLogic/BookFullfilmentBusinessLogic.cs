using Confluent.Kafka;
using OrderApplication.IBusinessLogic;
using System;
using OrderApplication.Common;
using OrderApplication.Models;
using static OrderApplication.Common.Enums;
using OrderApplication.BusinessLogic.Helper;
using Newtonsoft.Json;

namespace OrderApplication.BusinessLogic
{
    public class BookFullfilmentBusinessLogic : IBookFullfilmentBusinessLogic
    {
        public void Listen(Action<string> message)
        {
            KafkaHelper kfkHlpr = new KafkaHelper();
            var config = new ConsumerConfig()
            {
                BootstrapServers = Constants.KafkaServerURL,
                GroupId = Constants.BookGroupName,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
            {
                bool cancelled = false;

                consumer.Subscribe(Constants.OrderTopicName);

                while (!cancelled)
                {
                    var consumeResult = consumer.Consume(Constants.CancellationToken);
                    if (consumeResult != null && consumeResult.Message != null)
                    {
                        Order orderObj = JsonConvert.DeserializeObject<Order>(consumeResult.Message.Value);

                        if (orderObj.OrderTypeID == OrderTypeEnums.Book)
                        {
                            for (int i = 1; i < 4; i++)
                            {
                                orderObj.OrderStatus = orderObj.OrderStatus + 1;
                                kfkHlpr.SendOrder(orderObj, Constants.OrderStatusTopicName);

                                string msg = string.Format("Order {0} new status is {1}", orderObj.OrderID.ToString(),
                                                  ((OrderStatusEnums)(orderObj.OrderStatus)).ToString());
                                message(msg);
                            }
                        }
                        else if (consumeResult != null && consumeResult.Message != null)
                        { kfkHlpr.SendOrder(orderObj, Constants.OrderTopicName); }
                    }
                }

                consumer.Close();
            }
        }
               
    }    
}
