using NetGears.Core.Logger;
using NetGears.Game.Packet.Login.Client;

namespace NetGears.Login.Server
{
    public class LoginPacketHandler
    {
        private static readonly Logger Logger = Logger.GetLogger<LoginPacketHandler>();
        
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