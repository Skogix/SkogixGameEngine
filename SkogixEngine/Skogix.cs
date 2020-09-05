using System;
using System.Collections.Generic;
using System.Linq;

namespace ECS {
	/// <summary>
	///     Interfacet utåt
	/// </summary>
	public static class Skogix {
		private static Dictionary<Type, int> _componentIdByType;
		private static Dictionary<string, Type> _componentTypeByName;
		private static List<Type> _componentTypes;
		public static List<EntitySystem> _entitySystems;
		public static List<RunSystem> _runSystems;
		public static List<InitSystem> _initSystems;
		public static Dictionary<string, Entity> _entityByHash;
		public static Dictionary<Type, Entity> _entityByType;
		private static void _init() {
			_componentIdByType = new Dictionary<Type, int>();
			_componentTypeByName = new Dictionary<string, Type>();
			_componentTypes = new List<Type>();
			_entitySystems = new List<EntitySystem>();
			_runSystems = new List<RunSystem>();
			_initSystems = new List<InitSystem>();
			_entityByHash = new Dictionary<string, Entity>();
			_entityByType = new Dictionary<Type, Entity>();
			var domain = AppDomain.CurrentDomain; // nuvarande domain, dvs inte SkogixEngine utan där den callas
			foreach (var componentType in from assembly in
				                              domain.GetAssemblies() // hämtar loadade assemblies från domainen
			                              from type in assembly.GetTypes() // hämtar typer från assembly
			                              where type.IsSubclassOf(typeof(Component)) // där typen är sealed och ärver av component
			                              select type) {
				var id = _componentTypes.Count;
				_componentTypes.Add(componentType);
				_componentTypeByName[componentType.Name] = componentType;
				_componentIdByType[componentType] = id;
			}
		}
		internal static int GetComponentId(Type type) { return _componentIdByType[type]; }
		/// <summary>
		///     Måste callas innan något annat
		///     Läser in alla components i domain
		/// </summary>
		public static void Init() { _init(); }
		public static void AddSystem(EntitySystem entitySystem) {
			_entitySystems.Add(entitySystem);
			//if(system is EntitySystem entitySystem) _entitySystems.Add(entitySystem);
			if (entitySystem is RunSystem runSystem) _runSystems.Add(runSystem);
			if (entitySystem is InitSystem initSystem) _initSystems.Add(initSystem);
		}
		public static void Run() { _runSystems.ForEach(s => s.Run()); }
		public static void InitSystems() { _initSystems.ForEach(s => s.Init()); }
		internal static class EntityIdFactory { }
	}
}