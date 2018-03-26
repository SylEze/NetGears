using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace NetGears.Core.Misc
{
    public static class InstanceHelper
    {
        private delegate T ObjectActivator<T>(params object[] args);

        private static ObjectActivator<T> GetActivator<T>(ConstructorInfo ctor)
        {
            var type = ctor.DeclaringType;
            ParameterInfo[] paramsInfo = ctor.GetParameters();
            ParameterExpression paramExp = Expression.Parameter(typeof(object[]), "args");
            Expression[] argsExp =  new Expression[paramsInfo.Length];

            for (int i = 0; i < paramsInfo.Length; i++)
            {
                Expression index = Expression.Constant(i);
                Type paramType = paramsInfo[i].ParameterType;

                Expression paramAccessorExp =
                    Expression.ArrayIndex(paramExp, index);

                Expression paramCastExp =
                    Expression.Convert(paramAccessorExp, paramType);

                argsExp[i] = paramCastExp;
            }

            NewExpression newExp = Expression.New(ctor, argsExp);

            LambdaExpression lambda = Expression.Lambda(typeof(ObjectActivator<T>), newExp, paramExp);

            ObjectActivator<T> compiled = (ObjectActivator<T>)lambda.Compile();
            return compiled;
        }

        /// <summary>
        /// Return a new instance of T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public static T CreateInstanceOf<T>(params object[] args)
        {
            ConstructorInfo ctor = typeof(T).GetConstructors().First();
            ObjectActivator<T> createdActivator = GetActivator<T>(ctor);

            return createdActivator(args);
        }

        /// <summary>
        /// Return a new derived instance of TBase
        /// </summary>
        /// <typeparam name="TBase"></typeparam>
        /// <param name="derivedType"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static TBase CreateInstanceOf<TBase>(Type derivedType, params object[] args)
        {
            ConstructorInfo ctor = derivedType.GetConstructors().First();
            ObjectActivator<TBase> createdActivator = GetActivator<TBase>(ctor);

            return createdActivator(args);
        }
    }
}
