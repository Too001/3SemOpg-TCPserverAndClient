using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


Console.WriteLine("TCP SERVER");

TcpListener listener = new TcpListener(IPAddress.Any, 7);

listener.Start();
Console.WriteLine("server running, please minimize and run client.cs program!");
while (true)
{
    TcpClient socket = listener.AcceptTcpClient();
    Task.Run(() => { HandleClient(socket); });
}
listener.Dispose();
async void HandleClient(TcpClient socket)
{
    Console.WriteLine("Socket connected: " + socket.Client.RemoteEndPoint);
    NetworkStream stream = socket.GetStream();
    StreamWriter writer = new StreamWriter(stream);
    StreamReader reader = new StreamReader(stream);

    while (socket.Connected)
    {
        string? input = null;
        try
        {
            input = reader.ReadLine();
        }
        catch (Exception ex) { }
        if (input == null)
            break;

        string[] parts = input.Split('|');
        switch (parts[0])
        {
            // Random
            case "random":
                if (parts.Length != 0)
                    break;
                writer.WriteLine("input numbers");
                writer.Flush();

                string numberInput = reader.ReadLine();
                if (numberInput != null)
                {
                    string[] numberParts = numberInput.Split(' ');
                    if (numberParts.Length == 2 &&
                        int.TryParse(numberParts[0], out int part1) &&
                        int.TryParse(numberParts[1], out int part2))
                    {
                        Random number = new Random();
                        int resultInt = number.Next(part1, part2);
                        writer.WriteLine($"{resultInt}");
                        writer.Flush();
                    }
                    else
                    {
                        writer.WriteLine("error: input 2 numbers seperated by a space.");
                        writer.Flush();
                    }
                }
                break;
            case "add":
                if (parts.Length != 0)
                    break;
                writer.WriteLine("input numbers");
                writer.Flush();

                string numberInput2 = reader.ReadLine();
                if (numberInput2 != null)
                {
                    string[] numParts = numberInput2.Split(' ');
                    if (numParts.Length == 2 &&
                        int.TryParse(numParts[0], out int part1add) &&
                        int.TryParse(numParts[1], out int part2add))
                    {
                        int resultIntSum = part1add + part2add;
                        writer.WriteLine($"{resultIntSum}");
                        writer.Flush();
                    }
                    else
                    {
                        writer.WriteLine("error: input 2 numbers seperated by a space.");
                        writer.Flush();
                    }
                }
                break;
            case "subtract":
                if (parts.Length != 0)
                    break;
                writer.WriteLine("input numbers");
                writer.Flush();

                string numberInput3 = reader.ReadLine();
                if (numberInput3 != null)
                {
                    string[] numbParts = numberInput3.Split(' ');
                    if (numbParts.Length == 2 &&
                        int.TryParse(numbParts[0], out int part1sub) &&
                        int.TryParse(numbParts[1], out int part2sub))
                    {
                        int resultIntSub = part1sub - part2sub;
                        writer.WriteLine($"{resultIntSub}");
                        writer.Flush();
                    }
                    else
                    {
                        writer.WriteLine("error: input 2 numbers seperated by a space.");
                        writer.Flush();
                    }
                }
                break;
        }

    }
    socket.Close();
}