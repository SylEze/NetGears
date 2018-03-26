using NetGears.Core.Logger;
using NetGears.Game.Packets.Client;

namespace NetGears.Login.Handlers
{
    public class AuthentificationHandler
    {
        public static void CheckVersion(LoginClient client, VersionPacket packet)
        {
            Logger.Info($"check version {client.Id}");
        }
        
        public static void CheckUserCredentials(LoginClient client, AccountPacket packet)
        {
            Logger.Info($"account {packet?.Username} {packet?.Password}");
        }
    }
}