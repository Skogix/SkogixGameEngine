using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS
{
	public static class Bus
	{
		private static readonly List<object> _bus = new List<object>();

		public static void Push<T>(T data)
		{
			_bus.Add(data);
		}

		public static IEnumerable<T> Pull<T>(Type type = default) where T : class
		{
			return from o in type != null
					? _bus.Where(o => o.GetType() == type)
					: _bus.Where(o => o.GetType() == typeof(T))
				select o as T;
		}
	}
}