using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace Arcana
{
    static class MainRunner
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (ArcanaMain game = new ArcanaMain())
            {
                Server server = new Server();
                TcpClient client = new TcpClient();

                DatabaseLink db = server.getDB();
                db.Insert();

                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000);

                client.Connect(serverEndPoint);

                NetworkStream clientStream = client.GetStream();

                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] buffer = encoder.GetBytes("Hello Server!");

                clientStream.Write(buffer, 0, buffer.Length);
                clientStream.Flush();
                game.Run();
            }
        }
    }
}

