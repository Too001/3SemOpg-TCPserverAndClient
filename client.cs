using System;
using System.IO;
using System.Net.Sockets;

internal class Client
{
    private static void Main(string[] args)
    {
        int start = Convert.ToInt32(args[0]);
        int end = Convert.ToInt32(args[1]);
        for (int current = start; current < end; current++)
        {
            Console.WriteLine(current);
        }

        Console.WriteLine($"Connecting to Server");

        bool keepSending = true;

        TcpClient socket = new TcpClient("127.0.0.1", 7);

        NetworkStream ns = socket.GetStream();

        StreamReader reader = new StreamReader(ns);
        StreamWriter writer = new StreamWriter(ns);

        while (keepSending)
        {

            string? message = Console.ReadLine();

            writer.WriteLine(message);

            writer.Flush();

            string? response = reader.ReadLine();

            Console.WriteLine($"Response: " + response);

            if (message?.ToLower() == "close")
            {
                keepSending = false;
            }
        }


        socket.Close();
    }
}