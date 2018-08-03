using NetGears.Core.Logger;
using NetGears.Database;
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

            using (var context = DatabaseFactory.GetNetGearsContext())
            {
                var account = context.AccountRepository.Get(x =>
                    x.Username == packet?.Username && x.Password == packet?.Password);

                if (account == null)
                {
                    Logger.Info($"{client.Id} bad credentials");
                }
            }
        }
    }
}