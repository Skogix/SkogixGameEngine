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
            
            Hr("Push 'push message'");
            bus.Push(new TestEvent(skogix, "push message"));
            Hr("Sub '<TestEvent>'");
            hub.Sub<TestEvent>(skogix, OnTestEvent);
            Hr("Pub");
            hub.Pub(skogix, new TestEvent(skogix, "pub message"));
            Hr("Pull");
            bus.Pull<TestEvent>().ToList().ForEach(e => Print($"Sender:{e.Person.Name}\nMessage: {e.Message}"));
        }

        private static void OnTestEvent(TestEvent e) => Print($"Sender: {e.Person.Name}\nMessage: {e.Message}");

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

    internal class TestEvent
    {
        public readonly string Message;
        public readonly Person Person;
        public TestEvent(Person person, string message)
        {
            Person = person;
            Message = message;
        }
    }
}