using System;
using NetGears.Core.Logger;
using NetGears.Database;
using NetGears.Login.Server;

namespace NetGears.Login
{
    internal class Program
    {
        private const string Title = "NetGears - LoginServer";
        
        private static void Main(string[] args)
        {
            Console.Title = Title;

            Logger.Initialize();

            DatabaseFactory.Initialize();

            using (var loginServer = new LoginServer())
            {
                Console.CancelKeyPress += delegate(object sender, ConsoleCancelEventArgs e)
                {
                    loginServer.Stop();
                    e.Cancel = true;
                };
                loginServer.Start();
            }
        }
    }
}