using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NetGears.Core.Network
{
    public static class PacketHandler
    {
        private static readonly Dictionary<object, Action<IClient, IPacket>> MethodReferences = new Dictionary<object, Action<IClient, IPacket>>();

        public static void Initialize<T>() where T : class
        {
            var methods = typeof(T).GetMethods()
                .Where(m => m.GetCustomAttributes(typeof(PacketIdAttribute), false).Length > 0 && m.IsPublic && m.IsStatic);

            foreach (var method in methods)
            {
                var attribute = method.GetCustomAttribute(typeof(PacketIdAttribute)) as PacketIdAttribute;
                var action = method.CreateDelegate(typeof(Action<IClient, IPacket>)) as Action<IClient, IPacket>;
                MethodReferences.Add(attribute?.Id, action);
            }
        }

        public static void Invoke(object packetHeader, IClient invoker, IPacket packet)
        {
            try
            {
                MethodReferences[packetHeader].Invoke(invoker, packet);
            }
            catch (Exception e)
            {
                Logger.Logger.Error($"An error occured when executing handler method for packet type {packetHeader}", e);
            }
        }
    }
}
