using Confluent.Kafka;
using Newtonsoft.Json;
using OrderApplication.Common;
using OrderApplication.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace OrderApplication.BusinessLogic.Helper
{
    public class KafkaHelper
    {
        public void SendOrder(Order orderObj, string topicName)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = Constants.KafkaServerURL,
                ClientId = Dns.GetHostName(),
            };

            string message = JsonConvert.SerializeObject(orderObj);

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                producer.Produce(topicName, new Message<Null, string> { Value = message }, handler);
            }
        }

        public static void handler(DeliveryReport<Null, string> deliveryReport)
        {
            string orederStr = deliveryReport.Value;
        }
    }
}
