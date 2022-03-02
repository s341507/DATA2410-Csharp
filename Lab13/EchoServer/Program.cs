#define async // comment/uncomment this to use async vs non async version

using System.Net;
using System.Net.Sockets;
using static System.Console;

//data container for request and responce
var buffer = new byte[1024];

//setting up the endpoint and the server socket that will be listening for connections:
var ip = IPAddress.Parse("127.0.0.1");
var endpoint = new IPEndPoint(ip, 8001); // address and port

using (var socket = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp))
{
    WriteLine("Binding and listening..");
    socket.Bind(endpoint);
    socket.Listen(1);

#if async
    var client = await socket.AcceptAsync();

    WriteLine($"Connected to client {client}");
    while (client.Connected)
    {
        int length = await client.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);

        if (length == 0)
            break;

        await client.SendAsync(new ArraySegment<byte>(buffer,0, length), SocketFlags.None);
    }
#else
    var client = socket.Accept();

    WriteLine($"Connected to client {client}");
    while (client.Connected)
    {
        int length = client.Receive(buffer);

        if (length == 0)
            break;

        client.Send(buffer.AsSpan(0, length));
    }
#endif
}