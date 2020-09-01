using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bus
{
    public static class Hub
    {
        private static List<object> _bus = new List<object>();
        private static List<Handler> _handlers = new List<Handler>();

        public static void Sub<T>(object sub, Action<T> handler)
        {
            _handlers.Add(GetHandler<T>(sub, handler));
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
        
        public static void Pub<T>(T data = default)
        {
            foreach (var handler in _handlers.Where(h => h.Type == typeof(T)))
                if (handler.Action is Action<T> sendAction)
                    sendAction(data);
        }

        public static void Push<T>(T data)
        {
            _bus.Add(data);
        }

        public static IEnumerable<T> Pull<T>(Type type = default)
        {
            var output = type != null
                ? _bus.Where(o => o.GetType() == type)
                : _bus.Where(o => o.GetType() == typeof(T));
            foreach (var o in output)
                yield return (T) o;
        }
        
    }
    internal class Handler
    {
        public Delegate Action { get; set; }
        public Type Type { get; set; }
        public WeakReference Sender { get; set; }
    }
}