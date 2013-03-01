using System;

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
                game.Run();
            }
        }
    }
}

