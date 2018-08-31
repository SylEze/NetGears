using NetGears.Core.Logger;
using NetGears.Game.Packet.Login.Client;

namespace NetGears.Login.Server
{
    public class LoginPacketHandler
    {
        private static readonly Logger Logger = Logger.GetLogger<LoginPacketHandler>();
        
        public static void CheckVersion(LoginClient client, VersionPacket packet)
        {
            Logger.Debug($"({client.Id}) Check version LANG:{packet.Lang} VER:{packet.StreetGearsVersion}");
        }
        
        public static void CheckUserCredentials(LoginClient client, AccountPacket packet)
        {
            Logger.Debug($"({client.Id}) Check credentials USERNAME:{packet.Username} PW:{packet.Password}");
        }
    }
}