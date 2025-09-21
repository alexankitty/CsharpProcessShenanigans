using System;
using System.Dynamic;
using System.Reflection;

namespace ConsoleApp1
{
    public class DynamicDeepPointer : DynamicObject
    {
        private readonly DeepPointer _deeppointer = null;
        private readonly string _remote;
        private readonly string _ip;
        private readonly int _port;
        private readonly int _pid;

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var prop = _deeppointer.GetType().GetProperty(binder.Name, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null)
            {
                result = prop.GetValue(_deeppointer, null);
                return true;
            }

            result = null;
            return false;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var prop = _deeppointer.GetType().GetProperty(binder.Name, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(_deeppointer, value, null);
                return true;
            }

            return false;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var type = _deeppointer.GetType();
            var method = type.GetMethod(binder.Name, BindingFlags.Public | BindingFlags.Instance);
            if (method != null)
            {
                result = method.Invoke(_deeppointer, args);
                return true;
            }
            // fallback to extension methods
            var extMethod = ExtensionHelper.FindExtensionMethod(type, binder.Name, args);
            if (extMethod != null)
            {
                var allArgs = new object[args.Length + 1];
                allArgs[0] = _deeppointer;
                Array.Copy(args, 0, allArgs, 1, args.Length);
                result = extMethod.Invoke(null, allArgs);
                return true;
            }

            result = null!;
            return false;
        }
    }
}