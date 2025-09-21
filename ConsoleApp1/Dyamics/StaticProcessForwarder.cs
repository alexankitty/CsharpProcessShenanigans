using System;
using System.Dynamic;
using System.Reflection;
using System.Diagnostics;

namespace ConsoleApp1
{
    public class StaticProcessForwarder : DynamicObject
    {
        private readonly bool _remote = false;
        private readonly string _ip;
        private readonly int _port;

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            Type type = typeof(Process);
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static);
            foreach (var method in methods){
                if (method.Name == binder.Name && method.GetParameters().Length == args.Length)
                {
                    result = method.Invoke(null, args);
                    if(result is Process[])
                    {
                        var processes = (Process[])result;
                        dynamic[] dynamicProcesses = new dynamic[processes.Length];
                        for(int i = 0; i < processes.Length; i++)
                        {
                            dynamicProcesses[i] = new DynamicProcess();
                            ((DynamicProcess)dynamicProcesses[i]).GetType().GetField("_process", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dynamicProcesses[i], processes[i]);
                        }
                        result = dynamicProcesses;
                    }
                    else if(result is Process)
                    {
                        var process = (Process)result;
                        dynamic dynamicProcess = new DynamicProcess();
                        ((DynamicProcess)dynamicProcess).GetType().GetField("_process", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(dynamicProcess, process);
                        result = dynamicProcess;
                    }
                    return true;
                }
            }
            result = null;
            return false;
        }
    }
}