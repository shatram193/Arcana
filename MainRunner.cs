using System;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace Arcana
{
    /*Programmer: Mark Shatraw
     *This class runs Arcana proper, instantiating the server 
     *and an instance of the game proper.
     */
    static class MainRunner
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            //Initializes the server. More of a 
            //symbolic thing than an actual feature,
            //but it works.
            Server server = new Server();
            TcpClient client = new TcpClient();

            DatabaseLink db = server.getDB();

            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 3000);

            client.Connect(serverEndPoint);

            NetworkStream clientStream = client.GetStream();

            ASCIIEncoding encoder = new ASCIIEncoding();
            byte[] buffer = encoder.GetBytes("Hello Server!");

            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
            //The instance of Arcana gets a copy of the database
            //because the game state is initialized out of the database.
            using (ArcanaMain game = new ArcanaMain(db))
            {
                game.Run();
            }
        }
    }
}

