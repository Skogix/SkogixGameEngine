#region
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace ECS {
	public class World {
		public static readonly Dictionary<Type, int> ComponentIdByType = new Dictionary<Type, int>();
		public static readonly Dictionary<string, Type> ComponentTypeByName = new Dictionary<string, Type>();
		public static readonly List<Type> ComponentTypes = new List<Type>();
		public readonly List<EntitySystem> _entitySystems = new List<EntitySystem>();
		public readonly List<InitSystem> _initSystems = new List<InitSystem>();
		public readonly List<IRunSystem> _runSystems = new List<IRunSystem>();
		public Dictionary<string, Entity> _entityByHash = new Dictionary<string, Entity>();
		public Dictionary<Type, Entity> _entityByType = new Dictionary<Type, Entity>();
		
		public EntityFactory EntityFactory;
		public MessageManager MessageManager;
		public DebugSystem DebugSystem;
		public World() {
			MessageManager = new MessageManager(this);
			EntityFactory = new EntityFactory(this);
			DebugSystem = new DebugSystem(this);
			MessageManager.Subscribe<EntityAddedEvent>(this, OnEntityAdded);
			_init();
			
		}
		private void OnEntityAdded(EntityAddedEvent e) {
			_entityByHash.Add(e.Entity.GetHash, e.Entity);
			if(_entityByType.ContainsKey(e.GetType()) == false) _entityByType.Add(e.GetType(), e.Entity);
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
		public void AddSystem(EntitySystem entitySystem) {
			_entitySystems.Add(entitySystem);
			//if(system is EntitySystem entitySystem) _entitySystems.Add(entitySystem);
			if (entitySystem is IRunSystem runSystem) _runSystems.Add(runSystem);
			if (entitySystem is InitSystem initSystem) _initSystems.Add(initSystem);
		}
		public void Run() {
			_runSystems.ForEach(s => s.Run());
		}
		public void InitSystems() { _initSystems.ForEach(s => s.Init()); }

		public virtual void Destroy() {
		}
	}
}