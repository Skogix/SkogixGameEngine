using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using ECS;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var hub = new Hub();
            var bus = new Bus();
            var skogix = new Person("Skogix");
            
            bus.Push(new TestEvent(skogix, "TestEvent"));
            bus.Push(new TestActionEvent(PrintActionEvent));
            bus.Push(new TestFuncEvent(PrintFuncEvent));

            bus.Pull<TestEvent>().ToList().ForEach(e => Print(e.Message));
            bus.Pull<TestActionEvent>().ToList().ForEach(e => e.Action.Invoke());
            bus.Pull<TestFuncEvent>().ToList().ForEach(e => Print(e.Func.Invoke()));
            
            hub.Sub<TestEvent>(skogix, e => Print(e.Message));
            hub.Sub<TestActionEvent>(skogix, e => e.Action.Invoke());
            hub.Sub<TestFuncEvent>(skogix, e => Print(e.Func.Invoke()));
            
            hub.Pub(skogix, new TestEvent(skogix, "TestEvent"));
            hub.Pub(skogix, new TestActionEvent(PrintActionEvent));
            hub.Pub(skogix, new TestFuncEvent(PrintFuncEvent));
        }

        private static string PrintFuncEvent() => "PrintFuncEvent";
        private static void PrintActionEvent() => Print("PrintActionEvent");

        private static void Br() => Console.WriteLine();
        private static void Hr(string msg = "") => Console.WriteLine($"-----{msg}");
        private static void Tab() => Console.WriteLine("\t");
        private static void Print(string msg) => Console.WriteLine(msg);
    }

    internal class Person
    {
        public readonly string Name;
        public Person(string name)
        {
            Name = name;
        }
    }

    internal class TestActionEvent
    {
        public Action Action;

        public TestActionEvent(Action action)
        {
            Action = action;
        }
    }

    internal class TestFuncEvent
    {
        public Func<string> Func;

        public TestFuncEvent(Func<string> func)
        {
            Func = func;
        }
    }

    internal class TestEvent
    {
        public Person Sender;
        public string Message;

        public TestEvent(Person sender, string message)
        {
            Sender = sender;
            Message = message;
        }
    }
}