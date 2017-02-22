using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;
using System.Net;

namespace SPG.WebAPI
{
    public static class EventStoreConfiguration
    {
        private static IEventStoreConnection _connection;
        private static string _eventStorePort;
        private static string _eventStoreHostName;
        public static IEventStoreConnection CreateConnection(string eventStorePort, string eventStoreHostName)
        {
            _eventStorePort = eventStorePort;
            _eventStoreHostName = eventStoreHostName;
            return _connection = _connection ?? Connect();
        }

        private static IEventStoreConnection Connect()
        {
            ConnectionSettings settings =
                ConnectionSettings.Create()
                    .UseConsoleLogger()
                    .SetDefaultUserCredentials(new UserCredentials("admin", "changeit"));
            var endPoint = new IPEndPoint(EventStoreIP, EventStorePort);
            var connection = EventStoreConnection.Create(settings, endPoint, null);
            connection.ConnectAsync();
            return connection;
        }

        public static IPAddress EventStoreIP
        {
            get
            {
                if (string.IsNullOrEmpty(_eventStoreHostName))
                {
                    return IPAddress.Loopback;
                }
                var ipAddresses = Dns.GetHostAddressesAsync(_eventStoreHostName);
                return ipAddresses.Result[0];
            }
        }

        public static int EventStorePort
        {
            get
            {
                if (string.IsNullOrEmpty(_eventStorePort))
                {
                    return 1113;
                }
                return int.Parse(_eventStorePort);
            }
        }
    }
}
