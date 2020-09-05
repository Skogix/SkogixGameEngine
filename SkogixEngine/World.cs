#region
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace ECS {
	public class World {
		private static readonly Dictionary<Type, int> ComponentIdByType = new Dictionary<Type, int>();
		private static readonly Dictionary<string, Type> ComponentTypeByName = new Dictionary<string, Type>();
		private static readonly List<Type> ComponentTypes = new List<Type>();
		private readonly List<EntitySystem> _entitySystems = new List<EntitySystem>();
		private readonly List<INitSystem> _initSystems = new List<INitSystem>();
		private readonly List<IRunSystem> _runSystems = new List<IRunSystem>();
		private Dictionary<string, Entity> _entityByHash = new Dictionary<string, Entity>();
		private Dictionary<Type, Entity> _entityByType = new Dictionary<Type, Entity>();
		public EntityFactory EntityFactory;
		public MessageManager MessageManager;
		public World() {
			MessageManager = new MessageManager(this);
			EntityFactory = new EntityFactory(this);
		}
		private void _init() {
			var domain = AppDomain.CurrentDomain; // nuvarande domain, dvs inte SkogixEngine utan där den callas
			foreach (var componentType in from assembly in
				                              domain.GetAssemblies() // hämtar loadade assemblies från domainen
			                              from type in assembly.GetTypes() // hämtar typer från assembly
			                              where
				                              type.IsSubclassOf(typeof(Component
				                                                )) // där typen är sealed och ärver av component
			                              select type) {
				var id = ComponentTypes.Count;
				ComponentTypes.Add(componentType);
				ComponentTypeByName[componentType.Name] = componentType;
				ComponentIdByType[componentType] = id;
			}
		}
		internal static int GetComponentId(Type type) { return ComponentIdByType[type]; }
		/// <summary>
		///     Måste callas innan något annat
		///     Läser in alla components i domain
		/// </summary>
		public void Init() { _init(); }
		public void AddSystem(EntitySystem entitySystem) {
			_entitySystems.Add(entitySystem);
			//if(system is EntitySystem entitySystem) _entitySystems.Add(entitySystem);
			if (entitySystem is IRunSystem runSystem) _runSystems.Add(runSystem);
			if (entitySystem is INitSystem initSystem) _initSystems.Add(initSystem);
		}
		internal void Run() { _runSystems.ForEach(s => s.Run()); }
		public void InitSystems() { _initSystems.ForEach(s => s.Init()); }
	}
}