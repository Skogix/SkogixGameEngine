using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS
{
    public class Bus
    {
        private readonly List<object> _bus = new List<object>();

        public void Push<T>(T data) => _bus.Add(data);

        public IEnumerable<T> Pull<T>(Type type = default) where T : class => from o in type != null ? _bus.Where(o => o.GetType() == type) : _bus.Where(o => o.GetType() == typeof(T)) select o as T;
    }
}