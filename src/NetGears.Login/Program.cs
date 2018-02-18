using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using log4net;
using log4net.Config;
using log4net.Repository;
using NetGears.Core.Configuration;
using NetGears.Core.Logger;

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