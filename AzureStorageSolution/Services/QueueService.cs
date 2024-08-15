using Azure.Storage.Queues;
using System;
using System.Threading.Tasks;

namespace AzureStorageSolution.Services
{
    public class QueueService
    {
        private readonly QueueServiceClient _queueServiceClient;

        public QueueService(string storageAccountConnectionString)
        {
            // Initialize QueueServiceClient
            _queueServiceClient = new QueueServiceClient(storageAccountConnectionString);
        }

        public async Task SendMessageAsync(string queueName, string message)
        {
            // Get a reference to the queue
            QueueClient queueClient = _queueServiceClient.GetQueueClient(queueName);

            // Create the queue if it doesn't exist
            await queueClient.CreateIfNotExistsAsync();

            // Send a message to the queue
            await queueClient.SendMessageAsync(message);
        }

        public async Task<string> ReceiveMessageAsync(string queueName)
        {
            // Get a reference to the queue
            QueueClient queueClient = _queueServiceClient.GetQueueClient(queueName);

            // Receive the message
            var message = await queueClient.ReceiveMessageAsync();

            if (message.Value != null)
            {
                // Process the message (example: return the message text)
                string messageText = message.Value.MessageText;

                // Delete the message from the queue after processing
                await queueClient.DeleteMessageAsync(message.Value.MessageId, message.Value.PopReceipt);

                return messageText;
            }

            return null; // No messages available
        }

        public async Task DeleteQueueAsync(string queueName)
        {
            // Get a reference to the queue
            QueueClient queueClient = _queueServiceClient.GetQueueClient(queueName);

            // Delete the queue
            await queueClient.DeleteIfExistsAsync();
        }
    }
}