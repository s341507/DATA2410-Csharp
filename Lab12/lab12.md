# Lab 12 Exercise (TCP and UDP Sockets)

Created with .NET 6, C#10. Dotnet can be downloaded [here](https://dotnet.microsoft.com/en-us/download) (It also comes bundled with Visual Studio). You can test if you have .NET 6 by doing: `dotnet --version` in the terminal (you should then see `6.0.102` in the console).

## Setup

The [TCP Client](https://github.com/s341507/DATA2410-Csharp/blob/main/Lab12/TcpClient/Program.cs) gets the IP for `time.nist.gov`, then Connects to that IP with the port `13`. Then sends a request with just a single byte (0) and waits for a responce from the server.

The [UDP Client](https://github.com/s341507/DATA2410-Csharp/blob/main/Lab12/UdpClient/Program.cs) gets the IP for `djxmmx.net`, then sends a request to the IP with port `17` with just a single byte (0) and waits for a responce from the server.

## Testing

To run the [TCP Client](https://github.com/s341507/DATA2410-Csharp/blob/main/Lab12/TcpClient/Program.cs) simply change to the client project instead of the server project. Example `dotnet run --project TcpClient`

To run the [UDP Client](https://github.com/s341507/DATA2410-Csharp/blob/main/Lab12/UdpClient/Program.cs), you can either run it from an IDE or simply use: `dotnet run --project UdpClient`.

Here is an example of how it works:

![](./test.png)
