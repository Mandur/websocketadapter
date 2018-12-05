using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.Devices.Client;
using Microsoft.Extensions.Hosting;

namespace websocketadapter
{
    public class ModuleClientService : IHostedService
    {
        private int counter;

        IHubContext<MessagingHub> hubContext;
        public ModuleClientService(IHubContext<MessagingHub> hubContext)
        {
            this.hubContext = hubContext;

        }


        public async Task StartAsync(CancellationToken cancellationToken)
        {
            AmqpTransportSettings amqpSetting = new AmqpTransportSettings(TransportType.Amqp_Tcp_Only);
            ITransportSettings[] settings = { amqpSetting };

            // Open a connection to the Edge runtime
            ModuleClient ioTHubModuleClient = await ModuleClient.CreateFromEnvironmentAsync(settings);
            await ioTHubModuleClient.OpenAsync();
            Console.WriteLine("IoT Hub module client initialized.");

            // Register callback to be called when a message is received by the module
            await ioTHubModuleClient.SetInputMessageHandlerAsync("input1", PipeMessage, ioTHubModuleClient);
        }




        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }
        /// <summary>
        /// This method is called whenever the module is sent a message from the EdgeHub. 
        /// It just pipe the messages without any change.
        /// It prints all the incoming messages.
        /// </summary>
        async Task<MessageResponse> PipeMessage(Message message, object userContext)
        {
            int counterValue = Interlocked.Increment(ref counter);
            Console.WriteLine("test");
            var moduleClient = userContext as ModuleClient;
            if (moduleClient == null)
            {
                throw new InvalidOperationException("UserContext doesn't contain " + "expected values");
            }

            byte[] messageBytes = message.GetBytes();
            Console.WriteLine("msgId " + message.ConnectionDeviceId);

            string messageString = Encoding.UTF8.GetString(messageBytes);

            Console.WriteLine($"Received message: {counterValue}, Body: [{messageString}]");
            await ProcessMessage(message, messageString);
            if (!string.IsNullOrEmpty(messageString))
            {
                var pipeMessage = new Message(messageBytes);

                if (message.Properties.Count > 0)
                {
                    foreach (var prop in message.Properties)
                    {
                        pipeMessage.Properties.Add(prop.Key, prop.Value);
                    }

                    Console.WriteLine("prep to send back");
                }
                await moduleClient.SendEventAsync("output1", pipeMessage);
                Console.WriteLine("Received message sent");
            }
            return MessageResponse.Completed;

        }

        private async Task ProcessMessage(Message message, string messageString)
        {
            //send it to the SignalR people

            Console.WriteLine("sending a message");
            await this.hubContext.Clients.All.SendAsync("ReceiveMessage", message.ConnectionDeviceId, messageString);



            string path = @"/mnt/tmp.txt";


            using (StreamWriter sw = File.AppendText(path))
            {
                System.Console.WriteLine("starting writing");
                sw.WriteLine(String.Format("#{0}:{1}:{2}", message.SequenceNumber, message.CreationTimeUtc, messageString));
                System.Console.WriteLine("done writing");
            }
            //serialize messages             

        }

    }
}
