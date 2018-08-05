using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace NetGears.Core.Network
{
    public class PacketHandler<TClient, TPacket> where TPacket : IPacket
    {
        private static readonly Logger.Logger Logger = NetGears.Core.Logger.Logger.GetLogger("PacketHandler");
        
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
                          ((PacketHeaderAttribute) x).SenderType == PacketSenderType.SERVER))
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
                    throw new ArgumentException($"There must be two arguments in handler method: ${method.Name}");
                }

                if (parameters[0].Type != typeof(TClient))
                {
                    throw new ArgumentException(
                        $"${method.Name} First argument must be of type: ${typeof(TClient)}\tWas of type: ${parameters[0].Type}");
                }

                if (parameters[1].Type.BaseType != typeof(TPacket))
                {
                    throw new ArgumentException(
                        $"${method.Name} Second argument must derived from : ${typeof(TPacket)}\tWas of type: ${parameters[1].Type}");
                }

                if (!method.IsStatic)
                {
                    throw new ArgumentException("The provided method must be static.", "method");
                }

                if (method.IsGenericMethod)
                {
                    throw new ArgumentException("The provided method must not be generic.", "method");
                }

                var deleg = method.CreateDelegate(Expression.GetDelegateType(
                    (from parameter in method.GetParameters() select parameter.ParameterType)
                    .Concat(new[] {method.ReturnType})
                    .ToArray()));

                MethodReferences.Add(parameters[1].Type, deleg);

                Logger.Debug($"{method.Name} method registered.");
            }
        }


        public static void ExecuteHandler(TClient client, TPacket packet)
        {

            MethodReferences[packet.GetType()].DynamicInvoke(client, packet);
        }
    }
}