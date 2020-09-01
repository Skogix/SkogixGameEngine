using System;
using System.Collections.Generic;
using System.Linq;

namespace Bus
{
    public static class Hub
    {
        private static readonly List<object> Bus = new List<object>();
        private static readonly List<Handler> Handlers = new List<Handler>();

        public static void Sub<T>(object sub, Action<T> handler)
        {
            Handlers.Add(GetHandler<T>(sub, handler));
        }
        
        public static void Pub<T>(T data = default)
        {
            foreach (var handler in Handlers.Where(h => h.Type == typeof(T)))
                if (handler.Action is Action<T> sendAction)
                    sendAction(data);
        }

        public static void Push<T>(T data)
        {
            Bus.Add(data);
        }

        public static IEnumerable<T> Pull<T>(Type type = default)
        {
            var output = type != null
                ? Bus.Where(o => o.GetType() == type)
                : Bus.Where(o => o.GetType() == typeof(T));
            foreach (var o in output)
                yield return (T) o;
        }
        
        private static Handler GetHandler<T>(object sub, Delegate handler)
        {
            return new Handler
            {
                Action = handler,
                Type = typeof(T),
                Sender = new WeakReference(sub)
            };
        }
        
    }
    internal class Handler
    {
        public Delegate Action { get; set; }
        public Type Type { get; set; }
        public WeakReference Sender { get; set; }
    }
}