#region
using System;
using System.Collections.Generic;
using System.Linq;
using ECS.Factories;
using ECS.Interfaces;
using ECS.Systems;
#endregion

namespace ECS {
	public class World {
		private readonly List<ISystem> _allSystems = new List<ISystem>();
		private readonly EntityManager _entityManager;
		private readonly List<InitSystem> _initSystems = new List<InitSystem>();
		private readonly List<IRunSystem> _runSystems = new List<IRunSystem>();
		internal readonly DebugSystem DebugSystem;
		internal readonly EntityFactory EntityFactory;
		internal readonly EntityManager EntityManager;
		internal readonly EventManager EventManager;
		public World() {
			EventManager = new EventManager(this);
			EntityFactory = new EntityFactory(this);
			DebugSystem = new DebugSystem(this);
			EntityManager = new EntityManager(this);
			_init();
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
				var id = EntityManager.ComponentTypes.Count;
				EntityManager.ComponentTypes.Add(componentType);
				EntityManager.ComponentIdByType[componentType] = id;
			}
		}
		public void AddSystem(ISystem system) {
			_allSystems.Add(system);
			//if(system is EntitySystem entitySystem) _entitySystems.Add(entitySystem);
			if (system is IRunSystem runSystem) _runSystems.Add(runSystem);
			if (system is InitSystem initSystem) _initSystems.Add(initSystem);
		}
		public void Run() { _runSystems.ForEach(s => s.Run()); }
		public void InitSystems() { _initSystems.ForEach(s => s.Init()); }
	}
}