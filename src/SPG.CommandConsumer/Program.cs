using EventStore.ClientAPI;
using System;
using System.Net;
using System.Text;

namespace SPG.CommandConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            const string streamName = "spg_command_stream";
            const string logPath = @"D:\workspace\spg\event_store_poc\spg_commands.txt";
            const int defaultPort = 1113;
            var settings = ConnectionSettings.Create();
            var logFile = System.IO.File.Create(logPath);
            var logWriter = new System.IO.StreamWriter(logFile);
           
          
            using (var conn = EventStoreConnection.Create(settings, new IPEndPoint(IPAddress.Loopback, defaultPort)))
            {
                conn.ConnectAsync().Wait();
                var sub = conn.SubscribeToStreamFrom(streamName, StreamPosition.Start, true,
                    (_, x) =>
                    {

                        logWriter.WriteLine("Received: " + x.Event.EventType + ":" + x.Event.Created);
                    });

                //logWriter.Dispose();
                Console.WriteLine("waiting for commands. press enter to exit");
                Console.ReadLine();
                logWriter.Dispose();
            }
        }
    }
}