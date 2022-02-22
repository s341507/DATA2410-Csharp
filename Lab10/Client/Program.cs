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
    //connect to the server
    client.Connect(endpoint);
    Console.WriteLine("Connected to server, text in the terminal will be send to the server..");

    while (true)
    {
        // get message from terminal
        var input = Console.ReadLine();
        if (input is null)
            break;

        //send message from terminal to server
        var mgs = Encoding.ASCII.GetBytes(input);
        client.Send(mgs);

        //wait for responce message from server:
        int length = client.Receive(buffer);
        if (length == 1 && buffer[0] == 0)
            continue; // hearthbeat can just be ignored

        var responce = Encoding.ASCII.GetString(buffer, 0, length);
        Console.WriteLine($"mgs from server: {responce}");

        if (responce.EndsWith("Bye!"))
            break; // if the message from the server ends with "bye!" we know the server has disconnected
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}
finally
{
    Console.WriteLine("Disconnected!");
    client.Shutdown(SocketShutdown.Both);
    client.Close();
}