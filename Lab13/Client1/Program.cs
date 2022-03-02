#define opg1 // comment or uncomment this definition to compile the other task.

using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Console;

//payload
#if opg1
var data = Encoding.ASCII.GetBytes("HEAD / HTTP/1.1\r\nHost: webcode.me\r\nAccept: text/html\r\n\r\n");
#else
var data = Encoding.ASCII.GetBytes("GET / HTTP/1.1\r\nHost: webcode.me\r\nAccept: text/html\r\nConnection: close\r\n\r\n");
#endif
var buffer = new byte[1024];

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
#if opg1
    int length = socket.Receive(buffer);
    string responce = Encoding.ASCII.GetString(buffer, 0, length);
    WriteLine(responce);
#else
    while (true)
    {
        int length = socket.Receive(buffer);
        if (length == 0)
            break; // we don't have any more data to get.

        string responce = Encoding.ASCII.GetString(buffer, 0, length);
        WriteLine(responce);
    }
#endif
}