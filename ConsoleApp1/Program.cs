using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            dynamic processStatic = new StaticProcessForwarder();
            var notepad = processStatic.GetProcessesByName("notepad");
            foreach (var p in notepad)
            {
                Console.WriteLine($"{p.Id} - {p.ProcessName} - {p.Is64Bit()}");
            }
        }
    }
}
