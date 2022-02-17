using System.Net;
using System.Net.Sockets;
using System.Text;

var request = new byte[1]; // empty request
var response = new byte[1024];

//setting up the endpoint and the server socket that will be listening for connections:
var hostInfo = Dns.GetHostEntry("time.nist.gov");
var ip = hostInfo.AddressList[0];
var endpoint = new IPEndPoint(ip, 13); // address and port

//using a using statment so we don't need to close the socket by ourself
using (var socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
{
    try
    {
        Console.WriteLine("Connecting..");
        socket.Connect(endpoint);

        Console.WriteLine($"Sending request to {endpoint}:");
        socket.SendTo(request, endpoint);

        Console.WriteLine("Waiting for responce.. ");
        var length = socket.Receive(response);
        var mgs = Encoding.UTF8.GetString(response, 0, length);

        Console.WriteLine(mgs);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}