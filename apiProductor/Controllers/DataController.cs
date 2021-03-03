using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Azure.Messaging.ServiceBus;
using apiProductor.Models;
using Newtonsoft.Json;

namespace apiProductor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        [HttpPost]
        public async Task<bool> EnviarAsync([FromBody] Data data){

            string connectionString = "Endpoint=sb://queuemarco.servicebus.windows.net/;SharedAccessKeyName=envios;SharedAccessKey=0NAtXptjJ/ofNI85rZb1sq4chNsAWyvOOgpNgDlmACM=;EntityPath=queue1";
            string queueName = "queue1";
            string mensaje = JsonConvert.SerializeObject(data);

            await using (ServiceBusClient client = new ServiceBusClient(connectionString))
            {
                // create a sender for the queue 
                ServiceBusSender sender = client.CreateSender(queueName);

                // create a message that we can send
                ServiceBusMessage message = new ServiceBusMessage(mensaje);

                // send the message
                await sender.SendMessageAsync(message);
                Console.WriteLine($"Sent a single message to the queue: {queueName}");
            }
            return true;
        }
            
    }
}
