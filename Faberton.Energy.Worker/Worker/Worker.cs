using Faberton.Energy.Worker.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Faberton.Energy.Worker
{
    public static class Worker
    {
        [FunctionName("Worker")]
        public static async Task Run(
            [EventHubTrigger("%EventHubName%", Connection = "ServiceBusConnectionString")]DeviceData eventData,
            Binder binder,
            ILogger log)
        {
            log.LogInformation($"C# Event Hub trigger function processed a message: {eventData}");

            var sqlConnectionString = System.Environment.GetEnvironmentVariable("DatabaseConnectionString", System.EnvironmentVariableTarget.Process);

            // Validar que el dato entrante contiene al menos un numero negativo
            // esto significa que el dispositivo esta enviando datos por separado
            if(eventData.IRMS < 0 || eventData.Watts < 0)
            {
                // Leer el dato temporalmente almacenado en la tabla "buffer"
                var tableInfo = await binder.BindAsync<DeviceData>(
                    new TableAttribute("DeviceData", eventData.Identifier, eventData.DeviceId));

                // Si no hay datos temporales, significa que hay que insertar por primera vez
                if(tableInfo == null)
                {
                    var tableCollector = await binder.BindAsync<IAsyncCollector<DeviceData>>(
                        new TableAttribute("DeviceData", eventData.Identifier, eventData.DeviceId));

                    eventData.PartitionKey = eventData.Identifier;
                    eventData.RowKey = eventData.DeviceId;

                    await tableCollector.AddAsync(eventData);
                }
                // De lo contrario validar cual es el dato faltante
                else
                {
                    // Reemplazar el dato existente de table storage
                    // Verificar que el dato entrante es menor a 1 h con referencia a el dato temporal
                    // Si el lapso de diferencia es mayor a 1h ignorar el mensaje entrante y no almacenar en BD
                }

            }

            //using (SqlConnection conn = new SqlConnection(sqlConnectionString))
            //{
            //    var commandText = "INSERT INTO deviceEnergy(id_identifier, id_device, dateDevice, watts, irms) VALUES (@identifier, @device, @date, @watts, @irms)";
            //    using (SqlCommand comm = new SqlCommand())
            //    {
            //        comm.Connection = conn;
            //        comm.CommandText = commandText;
            //        comm.Parameters.AddWithValue("@identifier", eventData.Identifier);

            //        comm.Parameters.AddWithValue("@device", eventData.DeviceId);
            //        comm.Parameters.AddWithValue("@date", eventData.Date);
            //        comm.Parameters.AddWithValue("@watts", eventData.Watts);
            //        comm.Parameters.AddWithValue("@irms", eventData.IRMS);

            //        conn.Open();
            //        comm.ExecuteNonQuery();
            //    }
            //}
        }
    }
}
