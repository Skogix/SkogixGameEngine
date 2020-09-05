using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS {
	public static class Bus {
		private static readonly List<object> Bus = new List<object>();
		public static void Push<T>(T data) { Bus.Add(data); }
		public static IEnumerable<T> Pull<T>(Type type = default) where T : class {
			var output = from o in type != null
				             ? Bus.Where(o => o.GetType() == type)
				             : Bus.Where(o => o.GetType() == typeof(T))
			             select o as T;
			Bus.RemoveAll(o => o.GetType() == typeof(T));
			return output;
		}
	}
}