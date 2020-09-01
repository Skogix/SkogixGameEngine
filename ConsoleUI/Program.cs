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