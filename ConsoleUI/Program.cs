using System;
using System.Threading;
using ECS;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = new Bus();
            var skogixObject = new Skogix("foo");
            bus.Push(skogixObject);
            bus.Push(new Skogix("bar"));
            Console.WriteLine("Pushat 2 skogix");

            Thread.Sleep(1000);
            foreach (var skogix in bus.Pull<Skogix>())
            {
               Console.WriteLine(skogix.Msg); 
            }
        }
    }

    internal class Skogix
    {
        public string Msg;
        public Skogix(string msg)
        {
            Msg = msg;
        }
    }
}