using System;
using System.Runtime.InteropServices;
using System.Threading;
using ECS;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = new Bus();
            var hub = new Hub();
            var skogixPerson = new Person("Skogix");
            var fooEvent1 = new FooEvent("Bar 1");
            
            hub.Sub<FooEvent>(skogixPerson, OnFooEvent);
            
            bus.Push(fooEvent1);
            
            bus.Push(new FooEvent("FooEvent2"));
            
            hub.Pub(fooEvent1);

            foreach (var fooEvent in bus.Pull<FooEvent>()) Console.WriteLine($"Pull: {fooEvent.Message}");
            
            hub.Pub(new FooEvent("Bar 2"));
        }

        private static void Br() => Console.WriteLine();
        private static void Hr() => Console.WriteLine("-------------------");
        private static void Tab() => Console.WriteLine("\t");
        private static void Print(string msg) => Console.WriteLine(msg);

        private static void OnFooEvent(FooEvent e) => Console.WriteLine("Från OnFooEvent: " + e.Message);
    }

    internal class FooEvent
    {
        public string Message;

        public FooEvent(string message)
        {
            Message = message;
        }
    }

    internal class Person
    {
        public string Name;
        public Person(string name)
        {
            Name = name;
        }
    }
}