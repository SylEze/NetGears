using System;
using System.Collections.Generic;
using System.Text;
using NetGears.Core;
using NetGears.Core.Logger;
using NetGears.Core.Network;
using NetGears.GameData.Enums;
using NetGears.GameData.Packets.Client;

namespace NetGears.Login.Handlers
{
    public class AuthentificationHandler
    {
        [PacketId((short)LoginPacketsId.VERSION_PACKET)]
        public static void CheckVersion(IClient client, IPacket packet)
        {
            var loginClient = client as LoginClient;
            var versionPacket = packet as VersionPacket;
            
            Logger.Info($"check version {client.Id}");
        }
    }
}