using System;
using NetGears.Core.Logger;
using NetGears.Database;

namespace NetGears.Login
{
    internal static class Program
    {
        private const string Title = "NetGears - LoginServer";

        private static LoginServer _server;
        
        private static void Main(string[] args)
        {
            Console.Title = Title;
            Logger.Initialize(typeof(Program));

            try
            {
                DatabaseHelper.Initialize();
                _server = new LoginServer();
                _server.Start();
            }
            catch (Exception e)
            {
                Logger.Fatal("Failed to start server", e);
                _server?.Dispose();
            }
        }
    }
}