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
            
            bus.Push(new TestEvent(skogix, "push message"));
            hub.Sub<TestFunctionEvent>(skogix, OnTestEvent);
            hub.Pub(skogix, new TestFunctionEvent(() => Print("huhu"))); 
            bus.Pull<TestEvent>().ToList().ForEach(PrintTestEvent);
        }

        private static void OnTestEvent(TestFunctionEvent e) => e.Action.Invoke();
        private static void PrintTestEvent(TestEvent e) => Print($"Sender: {e.Sender.Name}\nMessage: {e.Message}");

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

    internal class TestFunctionEvent
    {
        public Action Action;

        public TestFunctionEvent(Action action)
        {
            Action = action;
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