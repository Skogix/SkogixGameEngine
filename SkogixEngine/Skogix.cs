using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS
{
	/// <summary>
	///   Interfacet utåt
	/// </summary>
	public static class Skogix
	{
		private static Dictionary<Type, int> _idByType;
		private static Dictionary<string, Type> _typeByName;
		private static List<Type> _types;

		private static void _init()
		{
			_idByType = new Dictionary<Type, int>();
			_typeByName = new Dictionary<string, Type>();
			_types = new List<Type>();

			
			var domain = AppDomain.CurrentDomain;															// nuvarande domain, dvs inte SkogixEngine utan där den callas
			foreach (var componentType in
				from assembly in domain.GetAssemblies() 												// hämtar loadade assemblies från domainen
				from type in assembly.GetTypes() 																// hämtar typer från assembly
				where type.IsSealed && type.IsSubclassOf(typeof(Component))		// där typen är sealed och ärver av component
				select type)
			{
				var id = _types.Count;
				_types.Add(componentType);
				_typeByName[componentType.Name] = componentType;
				_idByType[componentType] = id;
			}
		}

		/// <summary>
		///   Måste callas innan något annat
		///   Läser in alla components i domain
		/// </summary>
		public static void Init()
		{
			_init();
		}

		internal static int GetComponentId(Type type)
		{
			return _idByType[type];
		}
	}
}