using System;
using System.Runtime.CompilerServices;
using Bus;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = new object();
            Hub.Sub<Skogix>(obj, onSkogix);
            Hub.Pub(new Skogix("huhuhuhuu"));
            
            Hub.Push(new Skogix("msg1"));
            Hub.Push(new Skogix("msg2"));
            Hub.Push(new Skogix("msg3"));
            Hub.Push(new Skogix("msg4"));

            foreach (var skogix in Hub.Pull<Skogix>())
            {
               Console.WriteLine(skogix.Message); 
            }
        }

        private static void onSkogix(Skogix e)
        {
            Console.WriteLine(e.Message);
        }
    }

    internal class Skogix
    {
        public string Message;

        public Skogix(string message)
        {
            Message = message;
        }

    }
}