using System.Net;
using System.Net.Sockets;
using System.Text;

//setting up the server endpoint and making a buffer for the message data:
var ip = IPAddress.Parse("127.0.0.1");
var endpoint = new IPEndPoint(ip, 1337); // address and port
var client = new Socket(ip.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
var buffer = new byte[1024];

//we have a try block here in case something unexpected happens, so we can gracefully close the client socket:
try
{
    //connect to the server:
    client.Connect(endpoint);

    //wait for a text message:
    int length = client.Receive(buffer);
    var mgs = Encoding.ASCII.GetString(buffer, 0, length);

    //display the message:
    Console.WriteLine($"Got message from server: {mgs}");
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}
finally
{
    client.Shutdown(SocketShutdown.Both);
    client.Close();
}