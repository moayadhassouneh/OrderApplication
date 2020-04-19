using System;

namespace OrderApplication.Common
{
    public class Constants
    {
        public const string KafkaServerURL = "192.168.99.101:9092";
        public const string ComputerGroupName = "ComputerGroup";
        public const string OrderTopicName = "OrderTopic";
        public const int CancellationToken = 100;
        public const string BookGroupName = "BookGroup";
        public const string OrderStatusTopicName = "OrderStatusTopic";
    }
}
