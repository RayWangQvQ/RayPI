using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Ray.Infrastructure.Extensions
{
    public class EmitExtension<TAggregateRoot> where TAggregateRoot : class, new()
    {
        public delegate void SetValueDelegateHandler(TAggregateRoot entity, object value);

        public SetValueDelegateHandler EmitSetValue;

        public void BuildEmitMethod(Type entityType, string propertyName)
        {
            Type parmType = typeof(object);
            string methodName = "set_" + propertyName;
            var callMethod = entityType.GetMethod(methodName,
                BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic);
            var para = callMethod.GetParameters()[0];
            DynamicMethod method = new DynamicMethod("EmitCallable", null, new Type[] { entityType, parmType },
                entityType.Module);
            var il = method.GetILGenerator();
            var local = il.DeclareLocal(para.ParameterType, true);
            il.Emit(OpCodes.Ldarg_1);
            if (para.ParameterType.IsValueType)
            {
                il.Emit(OpCodes.Unbox_Any, para.ParameterType);
            }
            else
            {
                il.Emit(OpCodes.Castclass, para.ParameterType);
            }
            il.Emit(OpCodes.Stloc, local);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc, local);
            il.EmitCall(OpCodes.Callvirt, callMethod, null);
            il.Emit(OpCodes.Ret);

            EmitSetValue = method.CreateDelegate(typeof(SetValueDelegateHandler)) as SetValueDelegateHandler;
        }

        public void BuildEmitMethod(Expression<Func<TAggregateRoot, object>> func)
        {
            var propertyName = func.GetMember().Name;
            var entityType = typeof(TAggregateRoot);
            Type parmType = typeof(object);
            string methodName = "set_" + propertyName;
            var callMethod = entityType.GetMethod(methodName,
                BindingFlags.Instance | BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.NonPublic);
            var para = callMethod.GetParameters()[0];
            DynamicMethod method = new DynamicMethod("EmitCallable", null, new Type[] { entityType, parmType },
                entityType.Module);
            var il = method.GetILGenerator();
            var local = il.DeclareLocal(para.ParameterType, true);
            il.Emit(OpCodes.Ldarg_1);
            if (para.ParameterType.IsValueType)
            {
                il.Emit(OpCodes.Unbox_Any, para.ParameterType);
            }
            else
            {
                il.Emit(OpCodes.Castclass, para.ParameterType);
            }
            il.Emit(OpCodes.Stloc, local);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc, local);
            il.EmitCall(OpCodes.Callvirt, callMethod, null);
            il.Emit(OpCodes.Ret);

            EmitSetValue = method.CreateDelegate(typeof(SetValueDelegateHandler)) as SetValueDelegateHandler;
        }
    }
}
