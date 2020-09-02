using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS
{
    public class Hub
    {
        private readonly List<Handler> _handlers = new List<Handler>();

        public void Sub<T>(object sub, Action<T> handler) => _handlers.Add(GetHandler<T>(sub, handler));
        private Handler GetHandler<T>(object sub, Delegate handler) => new Handler
            {
                Action = handler,
                Type = typeof(T),
                Sender = new WeakReference(sub),
            };

        public void Pub<T>(T data = default)
        {
            foreach (var handler in _handlers.Where(h => h.Type == typeof(T)))
                if (handler.Action is Action<T> sendAction)
                    sendAction(data);
        }
        class Handler
        {
           public Delegate Action { get; set; } 
           public Type Type { get; set; }
           public WeakReference Sender { get; set; }
        }
    }
}