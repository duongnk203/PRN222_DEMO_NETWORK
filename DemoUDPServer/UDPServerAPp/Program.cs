using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    const int listenPort = 11000;
    const string host = "127.0.0.1";
    private static void StartListener()
    {
        string message;
        UdpClient listener = new UdpClient(listenPort);
        IPAddress address = IPAddress.Parse(host);
        IPEndPoint remoteEndPoint = new IPEndPoint(address, listenPort);
        Console.Title = "UDP server";
        Console.WriteLine(new string('*', 40));
        try
        {
            while (true)
            {
                Console.WriteLine("Waiting for broadcast");
                byte[] bytes = listener.Receive(ref remoteEndPoint);
                message = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                Console.WriteLine($"Received broadcast from {remoteEndPoint}: {message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            listener.Close();
        }
    }
    public static void Main()
    {
        Thread thread = new Thread(new ThreadStart(StartListener));
        thread.Start();
    }
}
