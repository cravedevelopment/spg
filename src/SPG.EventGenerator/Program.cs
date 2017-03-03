using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SPG.EventSourcing.Event;
using SPG.Utility;
//using SPG.Data.CQRS;
//using SPG.Data.CQRS.Events;

namespace SPG.EventGenerator
{
    class Program
    {

        public static IConfigurationRoot Configuration { get; set; }
        public static IEventStoreConnection Connection { get; set; }
     
        static void Main(string[] args)
        {
            const int defaultPort = 1113;
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            Connection = EventStoreConnection.Create(new IPEndPoint(IPAddress.Loopback, defaultPort));
            Connection.ConnectAsync();
            var eventsPerSecond = Convert.ToInt32(Configuration.GetSection("EventsPerSecond").Value);
            var maxNumberOfCustomers = Convert.ToInt32(Configuration.GetSection("NoOfCustomers").Value);
            var maxNumberOfSites = Convert.ToInt32(Configuration.GetSection("NoOfSites").Value);

            try
            {

                while (true)
                {
                    RunPublishEvent(eventsPerSecond, maxNumberOfCustomers, maxNumberOfSites);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }

        public static void RunPublishEvent(int eventsPerSecond, int maxNumberOfCustomers, int maxNumberOfSites)
        {
            var sw = Stopwatch.StartNew();
            int realeventsPerSecond = 0;
            for (int i = 0; i < eventsPerSecond; i++)
            {
                PublishEvent(maxNumberOfCustomers, maxNumberOfSites);
                realeventsPerSecond++;

                if ((sw.ElapsedMilliseconds / 1000) > 1)
                {
                    break;
                };
            }
            var interval = sw.ElapsedMilliseconds;

            Console.WriteLine("{0} : Event/s: {1}, Total Events: {2}", "Sending Events Finished", realeventsPerSecond, eventsPerSecond);
        }

        public static void PublishEvent(int maxNumberOfCustomers, int maxNumberOfSites)
        {
            //const string streamName = "poc_event_stream";
            //var aggregateId = Guid.NewGuid();
            //List<IEvent> events = new List<IEvent>
            //{
            //    new SampleEventCreated(aggregateId, DateTime.Now,
            //        RandomNumberGenerator.NumberValue(1, Convert.ToInt32(maxNumberOfCustomers)),
            //        RandomNumberGenerator.NumberValue(1, Convert.ToInt32(maxNumberOfSites)),
            //        RandomNumberGenerator.NumberValue(1, 250),
            //        RandomNumberGenerator.NumberValue(0, 20),
            //        RandomNumberGenerator.ByteArrayValue(200))
            //};

            //foreach (var item in events)
            //{
            //    var jsonString = JsonConvert.SerializeObject(item, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None });
            //    var jsonPayload = Encoding.UTF8.GetBytes(jsonString);
            //    var eventStoreDataType = new EventData(Guid.NewGuid(), item.GetType().Name, true, jsonPayload, null);
            //    Connection.AppendToStreamAsync(streamName, ExpectedVersion.Any, eventStoreDataType);
            //}

            //var results = Task.Run(() => connection.ReadStreamEventsForwardAsync(aggregateId.ToString(), StreamPosition.Start, 999,
            //  false));
            //Task.WaitAll();

            //var resultsData = results.Result;

        }
    }
}