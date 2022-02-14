using System.Net;
using System.Net.Sockets;
using System.Text;

//setting up the endpoint and the server socket that will be listening for connections:
var ip = IPAddress.Parse("127.0.0.1");
var endpoint = new IPEndPoint(ip, 1337); // address and port
var server = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

//we have a try block here in case something unexpected happens, so we can gracefully close the server socket
try
{
    //we bind the socket to the endpoint and put it in a listening state:
    server.Bind(endpoint);
    server.Listen(10);

    while (true)
    {
        Console.WriteLine("Listening for connections...");

        //wait until a client connects to us:
        var client = server.Accept();

        Console.WriteLine("Connected to client, sending hello message");

        //creating the data we want to send, in this example it's just this text:
        byte[] mgs = Encoding.ASCII.GetBytes("Hello! You are connected to the server, Bye!");
        client.Send(mgs);

        //lastly closing the connection to the client:
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