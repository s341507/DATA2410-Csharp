using System.Net;
using System.Net.Sockets;
using System.Text;

//setting up the endpoint and the server socket that will be listening for connections:
var ip = IPAddress.Parse("127.0.0.1");
var endpoint = new IPEndPoint(ip, 8080); // address and port for HTTP
var server = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

var receive = new byte[1024];
var responce = Encoding.ASCII.GetBytes("HTTP/1.1 200 OK\n\nHello World");

//we have a try block here in case something unexpected happens, so we can gracefully close the server socket
try
{
    //we bind the socket to the endpoint and put it in a listening state:
    server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
    server.Bind(endpoint);
    server.Listen(1);

    while (true)
    {
        Console.WriteLine("Listening for connections...");

        //wait until a client connects to us:
        var client = server.Accept();

        //GET request from the client
        var length = client.Receive(receive);
        var req = Encoding.ASCII.GetString(receive, 0, length);
        Console.WriteLine(req);

        //sending our hello world page toe the client
        client.Send(responce);

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