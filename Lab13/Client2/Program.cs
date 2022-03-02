using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Console;

//payload
var data = Encoding.ASCII.GetBytes("GET / HTTP/1.1\r\nHost: webcode.me\r\nAccept: text/html\r\nConnection: close\r\n\r\n");
var buffer = new byte[2048];

//setting up the endpoint and the server socket that will be connecting to:
var hostInfo = Dns.GetHostEntry("webcode.me");
var ip = hostInfo.AddressList[0];
var endpoint = new IPEndPoint(ip, 80); // address and port

using (var socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
{
    WriteLine("Connecting..");
    socket.Connect(endpoint);

    WriteLine("sending request..");
    socket.Send(data);

    WriteLine("waiting responce..");
    while (true)
    {
        int length = socket.Receive(buffer);
        if (length == 0)
            break; // we don't have any more data to get.

        string responce = Encoding.ASCII.GetString(buffer, 0, length);
        WriteLine(responce);
    }
}