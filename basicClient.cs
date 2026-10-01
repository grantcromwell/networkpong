using System;
using System.Linq.Expressions;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

class SimpleTcpClient
{
    public static void Main()
    {
        byte[] data = new byte[1024];
        string input, stringData;
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 47777);
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        int z = 0;
        try
        {
            server.Connect(ipep);

        while (true)
        {
            data = new byte[1024];
            int recv = server.Receive(data);
            if (recv == 0)
            break;
            stringData = Encoding.ASCII.GetString(data, 0 , recv);
            Console.WriteLine(stringData);
            if (stringData.Contains("Ping"))
            {
                z += 1;
            }
            data = new byte[1024];
            string rpong = "Pong";
            data = Encoding.ASCII.GetBytes(rpong);
            server.Send(data, data.Length, SocketFlags.None);

            if (recv == 0)
            {
                break;
            }

            if (z >= 132)
            {
                Console.WriteLine("Disconnecting from server...");
                break;
                

            }
        }
        }
        catch (SocketException e)
        {
              
            return;
        }
     
        
        server.Shutdown(SocketShutdown.Both);
        server.Close();
    }
}