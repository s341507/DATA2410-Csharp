using System.Net;
using System.Net.Sockets;
using System.Text;

//setting up the endpoint and the server socket that will be listening for connections:
var ip = IPAddress.Parse("127.0.0.1");
var endpoint = new IPEndPoint(ip, 1337); // address and port
var server = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
var buffer = new byte[1024];

//we have a try block here in case something unexpected happens, so we can gracefully close the server socket
try
{
    //we bind the socket to the endpoint and put it in a listening state:
    server.Bind(endpoint);
    server.Listen(1);

    while (true)
    {
        Console.WriteLine("Listening for connections...");

        //wait until a client connects to us:
        var client = server.Accept();

        Console.WriteLine("Connected to client, waiting for message from client");

        while (true)
        {
            //read message from client
            int length = client.Receive(buffer);
            var mgs = Encoding.ASCII.GetString(buffer, 0, length);

            Console.WriteLine($"mgs from Client: {mgs}");

            //if the message is "bye" we send message to the client that we have disconnected
            if(mgs.ToLower() != "bye")
            {
                //heartbeat
                client.Send(new byte[1] { 0 });
                continue;
            }

            //send mesage to the client that we are disconnecting
            var disconnect = Encoding.ASCII.GetBytes("Hello! You are no longer connected to the server, Bye!");
            client.Send(disconnect);
            break;
        }

        Console.WriteLine("Disconnected from client..");
        client.Shutdown(SocketShutdown.Both);
        client.Close();
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}
finally
{
    server.Shutdown(SocketShutdown.Both);
    server.Close();
}