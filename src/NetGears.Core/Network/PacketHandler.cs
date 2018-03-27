using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

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
        private static readonly Dictionary<Type, Delegate> MethodReferences = new Dictionary<Type, Delegate>();

        public static void LoadFrom<T>()
        {
            IEnumerable<MethodInfo> methods = typeof(T).GetMethods()
                .Where(m => m.IsPublic && m.IsStatic);

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
                    throw new ArgumentException($"${method.Name} First argument must be of type: ${typeof(TClient)}\tWas of type: ${parameters[0].Type}");
                }
                if (parameters[1].Type.BaseType != typeof(TPacketBase))
                {
                    throw new ArgumentException($"${method.Name} Second argument must derived from : ${typeof(TPacketBase)}\tWas of type: ${parameters[1].Type}");
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
                    .Concat(new[] { method.ReturnType })
                    .ToArray()));

                MethodReferences.Add(parameters[1].Type, deleg);
            }
        }

        public static void ExecuteHandler(TClient invoker, TPacketBase packet)
        {
            MethodReferences[packet.GetType()].DynamicInvoke(invoker, packet);
        }
    }
}