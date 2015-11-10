namespace UdpSend
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    class Program
    {
        static void Main(string[] args)
        {
            if (args == null
                || args.Length < 3)
            {
                WriteUsage();
                return;
            }

            var host = args[0];
            var port = args[1];
            var message = string.Join(
                " ",
                args.Skip(2));

            var portNumber = int.Parse(port);
            var buffer = Encoding.ASCII.GetBytes(message);

            WriteSending(
                host,
                portNumber,
                message);

            ClientSend(
                host,
                portNumber,
                buffer);

            //Send(
            //    host,
            //    portNumber,
            //    buffer);
        }

        static void ClientSend(string host, int port, byte[] buffer)
        {
            using (var client = new UdpClient(
                host,
                port))
            {
                client.Send(
                    buffer,
                    buffer.Length);
            }
        }

        static void Send(string host, int port, byte[] buffer)
        {
            var udpSocket = new Socket(
                AddressFamily.InterNetwork,
                SocketType.Dgram,
                ProtocolType.Udp);

            var serverAddress = IPAddress.Parse(host);

            var endpoint = new IPEndPoint(
                serverAddress,
                port);


            udpSocket.SendTo(
                buffer,
                endpoint);
        }

        static void WriteSending(string host, int port, string message)
        {
            var sending = string.Format(
                "UdpSending: \"{0}\" to {1}:{2}",
                message,
                host,
                port);

            Console.Out.WriteLine(sending);
        }

        static void WriteUsage()
        {
            var usage = "Expected usage is \"UdpSend [host] [port] [message]\"" +
                "\r\nEverything after the port will be sent as an ASCII string over UDP to the host on the port.";

            Console.Out.WriteLine(usage);
        }
    }
}
