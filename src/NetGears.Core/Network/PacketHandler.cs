using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NetGears.Core.Extensions;
using NetGears.Core.Misc;

namespace NetGears.Core.Network
{
    public class PacketHandler<TClient, TPacket> where TPacket : IPacket
    {
        private static readonly Logger.Logger Logger = Core.Logger.Logger.GetLogger("PacketHandler");

        /// <summary>
        /// Dictionary which contains method references to invoke
        /// Each key corresponds to a packet header defined by the user
        /// </summary>
        private static readonly Dictionary<Type, Delegate> MethodReferences = new Dictionary<Type, Delegate>();

        private static readonly Dictionary<PacketHeaderAttribute, Type> PacketTypes =
            new Dictionary<PacketHeaderAttribute, Type>();

        public static void LoadPackets(PacketServerType serverType)
        {
            var packets = typeof(TPacket)
                .Assembly
                .GetTypes()
                .Where(m => m.GetCustomAttributes(typeof(PacketHeaderAttribute), false)
                .Any(x => ((PacketHeaderAttribute) x).ServerType == serverType && 
                          ((PacketHeaderAttribute) x).SenderType == PacketSenderType.CLIENT))
                .ToList();

            foreach (var packet in packets)
            {
                var idAttribute = packet.GetCustomAttribute(typeof(PacketHeaderAttribute));

                PacketTypes.Add(idAttribute as PacketHeaderAttribute, packet);

                Logger.Debug($"{packet.Name} packet registered.");
            }
        }

        public static void LoadHandlers<THandler>()
        {
            var methods = typeof(THandler).GetTypeInfo().DeclaredMethods;

            foreach (var method in methods)
            {
                var parameters = method.GetParameters()
                    .Select(p => Expression.Parameter(p.ParameterType, p.Name))
                    .ToArray();

                if (parameters.Length != 2)
                {
                    throw new ArgumentException($"There must be two arguments in handler method: ${method.Name}.");
                }

                if (parameters[0].Type != typeof(TClient))
                {
                    throw new ArgumentException(
                        $"${method.Name} First argument must be of type: ${typeof(TClient)}.");
                }

                if (parameters[1].Type.BaseType != typeof(TPacket))
                {
                    throw new ArgumentException(
                        $"${method.Name} Second argument must derived from : ${typeof(TPacket)}.");
                }

                if (!method.IsStatic)
                {
                    throw new InvalidOperationException("The provided method must be static.");
                }

                if (method.IsGenericMethod)
                {
                    throw new InvalidOperationException("The provided method must not be generic.");
                }

                var deleg = method.CreateDelegate(Expression.GetDelegateType(
                    (from parameter in method.GetParameters() select parameter.ParameterType)
                    .Concat(new[] {method.ReturnType})
                    .ToArray()));

                MethodReferences.Add(parameters[1].Type, deleg);

                Logger.Debug($"{method.Name} method registered.");
            }
        }

        public static IReadOnlyCollection<TPacket> DeserializePackets(byte[] buffer)
        {
            var result = new List<TPacket>();

            var bufferIndex = 0;

            deserialize:

            if (bufferIndex >= buffer.Length)
            {
                return result;
            }

            var length = BitConverter.ToInt16(buffer, bufferIndex);
            var id = BitConverter.ToInt16(buffer, bufferIndex + 2);
            var hash = buffer[bufferIndex + 4];

            bufferIndex += 5;

            Logger.Debug($"New packet with id:{id} and length:{length}");

            if (length <= 0 || length + bufferIndex > buffer.Length)
            {
                return result;
            }
            else
            {
                var buff = buffer.GetSubArray(bufferIndex, length);

                result.Add(GetPacket(id, buff));

                bufferIndex += length;

                goto deserialize;
            }
        }

        private static TPacket GetPacket(short id, byte[] buffer)
        {
            foreach (var packetType in PacketTypes)
            {
                if (packetType.Key.Id == id)
                {
                    var result = InstanceHelper.CreateInstanceOf<TPacket>(packetType.Value, buffer);
                    result.Deserialize(buffer);
                    return result;
                }
            }
            return default;
        }

        public static void ExecuteHandler(TClient client, TPacket packet)
        {
            MethodReferences[packet.GetType()].DynamicInvoke(client, packet);
        }
    }
}