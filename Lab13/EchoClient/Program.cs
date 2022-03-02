using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Console;

//payload
var data = Encoding.ASCII.GetBytes("hello there");
var buffer = new byte[1024];

//setting up the endpoint and the server socket that will be connecting to:
var ip = IPAddress.Parse("127.0.0.1");
var endpoint = new IPEndPoint(ip, 8001); // address and port

using (var socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
{
    WriteLine("Connecting..");
    socket.Connect(endpoint);

    WriteLine("sending request..");
    socket.Send(data);

    WriteLine("waiting responce..");
    int length = socket.Receive(buffer);
    string responce = Encoding.ASCII.GetString(buffer, 0, length);
    WriteLine(responce);
}