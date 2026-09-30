using System;
using System.Data.SqlTypes;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;

class SimpleTcpSrvr
{
    public static void Main()
    {
        
        int recv;
        byte[] data = new byte[1024];
        string srecv;
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
        Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        newsock.Bind(ipep);
        newsock.Listen(10);
        Console.WriteLine("Waiting for a client...");
        Socket client = newsock.Accept();
        IPEndPoint clientep = (IPEndPoint)client.RemoteEndPoint;
        Console.WriteLine("Connected with {0} at port {1}", clientep.Address, clientep.Port);

        string welcome = "Ping";
        data = Encoding.ASCII.GetBytes(welcome);
        client.Send(data, data.Length, SocketFlags.None);
        int z = 0;
        while (true)
        {   

            data = new byte[1024];
            recv = client.Receive(data);
            srecv = Encoding.ASCII.GetString(data, 0 ,recv);
            Console.WriteLine(srecv);
        
            if (srecv.Contains("Pong"))
            {
                z += 1;
            }
            data = new byte[1024];
            data = Encoding.ASCII.GetBytes(welcome); 
            client.Send(data, data.Length, SocketFlags.None);

            if (recv == 0)
            {
                break;
            }

            if (z == 132)
            {
                break;
            }
    
        }

        

    
        Console.WriteLine("Disconnected from {0}", clientep.Address);
        client.Close();
        newsock.Close();
    }
}
    
       
