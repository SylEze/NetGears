using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NetGears.Core.Network
{
    public static class PacketHandler<TClient, TPacketBase>
        where TClient : class
        where TPacketBase : class
    {
        /// <summary>
        /// Dictionary which contains method references to invoke
        /// Each key corresponds to a packet header defined by the user
        /// </summary>
        private static readonly Dictionary<Type, MethodInfo> MethodReferences = new Dictionary<Type, MethodInfo>();

        public static void LoadFrom<T>()
        {
            IEnumerable<MethodInfo> methods = typeof(T).GetMethods()
                .Where(m => m.IsPublic && m.IsStatic)
                .ToList();

            foreach (var method in methods)
            {
                var parameters = method.GetParameters();

                if (parameters.Length != 2)
                {
                    throw new ArgumentException($"There must be two arguments in handler method: ${method.Name}");
                }
                if (parameters[0].ParameterType != typeof(TClient))
                {
                    throw new ArgumentException($"${method.Name} First argument must be of type: ${nameof(TClient)}\tWas of type: ${parameters[0].ParameterType}");
                }
                if (parameters[1].ParameterType.BaseType != typeof(TPacketBase))
                {
                    throw new ArgumentException($"${method.Name} Second argument must derived from : ${nameof(TPacketBase)}\tWas of type: ${parameters[1].ParameterType}");
                }
                
                MethodReferences.Add(parameters[1].ParameterType, method);
            }
        }

        public static void Invoke(TClient invoker, TPacketBase packet)
        {
            try
            {
                MethodReferences[packet.GetType()].Invoke(null, new object[] { invoker, packet });
            }
            catch (Exception e)
            {
                Logger.Logger.Error($"An error occured while executing handler method for packet type {typeof(TPacketBase).ToString()}", e);
            }
        }
    }
}