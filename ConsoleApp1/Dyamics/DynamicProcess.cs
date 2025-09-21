using System;
using System.Dynamic;
using System.Reflection;
using System.Diagnostics;

namespace ConsoleApp1
{
    public class DynamicProcess : DynamicObject
    {
        private readonly object _process;
        private readonly string _remote;
        private readonly string _ip;
        private readonly int _port;
        private readonly int _pid;

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            var prop = _process.GetType().GetProperty(binder.Name, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null)
            {
                result = prop.GetValue(_process, null);
                return true;
            }

            result = null;
            return false;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var prop = _process.GetType().GetProperty(binder.Name, BindingFlags.Public | BindingFlags.Instance);
            if (prop != null && prop.CanWrite)
            {
                prop.SetValue(_process, value, null);
                return true;
            }

            return false;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var type = _process.GetType();
            var method = type.GetMethod(binder.Name, BindingFlags.Public | BindingFlags.Instance);
            if (method != null)
            {
                result = method.Invoke(_process, args);
                return true;
            }
            // fallback to extension methods
            var extMethod = ExtensionHelper.FindExtensionMethod(type, binder.Name, args);
            if (extMethod != null)
            {
                Console.WriteLine("Invoking extension method: " + extMethod.Name);
                Console.WriteLine("Parameters: " + string.Join(", ", extMethod.GetParameters().Select(p => p.ParameterType.Name + " " + p.Name)));
                Console.WriteLine("Arguments: " + string.Join(", ", args.Select(a => a?.GetType().Name + " " + a?.ToString())));
                var allArgs = new object[args.Length + 1];
                allArgs[0] = _process;
                Array.Copy(args, 0, allArgs, 1, args.Length);
                result = extMethod.Invoke(null, allArgs);
                return true;
            }

            result = null!;
            return false;
        }
    }
}