using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System;
using System.Threading.Tasks;

namespace AzureIOT.Repositories
{
    public class QueueStorageRepository
    {
        private static string connStringStorage = "DefaultEndpointsProtocol=https;AccountName=storageahs230112;AccountKey=GQBehDz8SzJuUwzzzeabojmVk7qN2U6aQXPrHgh4Msc6FhCjCQPJ3VIjjNKvAgClU7mJ01rMeXHS+ASt2iVZow==;EndpointSuffix=core.windows.net";

        public static async Task<bool> CreateQueue(string queueName)
        {
            if (string.IsNullOrEmpty(queueName))
            {
                throw new ArgumentNullException("Queue Name Missing!");
            }
            try
            {
                QueueClient queue = new QueueClient(connStringStorage, queueName);
                await queue.CreateIfNotExistsAsync();
                if (queue.Exists())
                {
                    Console.WriteLine("Queue Created:" + queue.Name);
                    return true;
                }
                else
                {
                    Console.WriteLine("Check Azure Emulator connection and try again");
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<string> InsertMessage(string queueName, string msg)
        {
            if (string.IsNullOrEmpty(queueName) || string.IsNullOrEmpty(msg))
            {
                throw new ArgumentNullException("Parameter Missing!");
            }

            QueueClient queue = new QueueClient(connStringStorage, queueName);
            await queue.CreateIfNotExistsAsync();
            if (queue.Exists())
            {
                var data = queue.SendMessage(msg);
                Console.WriteLine($"Inserted: {msg}");
            }
            return null;
        }

        public static PeekedMessage[] PeekMessage(string queueName, int peekValue)
        {
            QueueClient queue = new QueueClient(connStringStorage, queueName);
            PeekedMessage[] msg = null;
            if (queue.Exists())
            {
                msg = queue.PeekMessages(peekValue);
            }
            return msg;
        }

        public static void UpdateMessage(string queueName, string updatedMsg)
        {
            QueueClient queue = new QueueClient(connStringStorage, queueName);
            if (queue.Exists())
            {
                QueueMessage[] msg = queue.ReceiveMessages();
                queue.UpdateMessage(msg[0].MessageId, msg[0].PopReceipt,
                    updatedMsg, TimeSpan.FromSeconds(60.0));
            }
        }

        public static void DequeueMessage(string queueName)
        {
            QueueClient queue = new QueueClient(connStringStorage, queueName);
            if (queue.Exists())
            {
                QueueMessage[] msg = queue.ReceiveMessages();
                System.Console.WriteLine($"Dequeued message: '{msg[0].Body}'");
                queue.DeleteMessage(msg[0].MessageId, msg[0].PopReceipt);
            }
        }

        public static async Task DeleteQueue(string queueName)
        {
            QueueClient queue = new QueueClient(connStringStorage, queueName);
            if (queue.Exists())
            {
                await queue.DeleteAsync();
            }
        }
    }
}
