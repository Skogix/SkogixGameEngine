#region
#region
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using ECS.Interfaces;
using ECS.Systems;

namespace ECS {
	#endregion
public interface IProductStockReportBuilder
{
    IProductStockReportBuilder BuildHeader();
    IProductStockReportBuilder BuildBody();
    IProductStockReportBuilder BuildFooter();
    ProductStockReport GetReport();
}
public class ProductStockReport { }


public IProductStockReportBuilder BuildHeader()
{
    _productStockReport.HeaderPart = $"STOCK REPORT FOR ALL THE PRODUCTS ON DATE: {DateTime.Now}\n";
    return this;
}

public IProductStockReportBuilder BuildBody()
{
    _productStockReport.BodyPart = string.Join(Environment.NewLine, _products.Select(p => $"Product name: {p.Name}, product price: {p.Price}"));
    return this;
}

public IProductStockReportBuilder BuildFooter()
{
    _productStockReport.FooterPart = "\nReport provided by the IT_PRODUCTS company.";
    return this;
}






public static class Extensions {
		public static void Add(this Entity e, Component c) { e.EntityManager.AddComponent(e, c); }
		public static T Get<T>(this Entity e) where T : Component { return e.EntityManager.Get<T>(e); }
		public static void RemoveComponent<T>(this Entity e) where T : Component {
			e.EntityManager.RemoveComponent(e, e.Get<T>());
		}
		public static void RemoveComponent(this Entity e, Component c) =>
			e.EntityManager.RemoveComponent(e, c);
		//public static Entity CreateEntity(this World w) => w.EntityFactory.Get();
		public static Entity CreateEntity(this World w, ITemplate t) => w.EntityFactory.Get(t);
		public static Entity CreateEntity(this World w, Component c) => w.EntityFactory.Get(c);
		public static Entity CreateEntity(this World w, params Component[] c) => w.EntityFactory.Get(c);

		
		public static Entity CreateEntity(this World w) {
			var entity = w.EntityFactory.Get();
			void Add(Component c) { entity.EntityManager.AddComponent(entity, c); }
			return entity;
		}
		/*
		internal static bool ContainsComponent(this EntityManager m, Type c) => EntityFactory.BackupData.ComponentsByType.ContainsKey(c);
		internal static bool ContainsComponents(this EntityManager m, IEnumerable<Type> c) => c.All(EntityFactory.BackupData.ComponentsByType.ContainsKey);
		internal static bool ContainsComponents(IEnumerable<Type> e, params IEnumerable<Type>[] c) => ContainsComponents(c as IEnumerable<Type>);
		
		public static void AddFilter<T>(this EntitySystem es) => es.AddFilter(typeof(T));
		public static void Add<T>(this Entity e, Component c) => e.EntityManager.AddComponent(e, c);
		
		
		public static Entity CreateEntity(this World w) => w.EntityFactory.Get();
		public static Entity CreateEntity(this World w, ITemplate t) => w.EntityFactory.Get(t);
		public static Entity CreateEntity(this World w, Component c) => w.EntityFactory.Get(c);
		public static Entity CreateEntity(this World w, params Component[] c) => w.EntityFactory.Get(c);
		
		
		public static string GetInfo(this Entity e) => $"Hash: {e.GetHash} \nComponents ({EntityFactory.BackupData.ComponentsByType.Count})\nName: {e.GetType().Name}";
		public static void AddComponents(this Entity e, IEnumerable<Component> components) { foreach (var component in components) e.Add(component); }
		*/
		public static void Push<T>(this EntitySystem es, T data) where T : ICommand => es.World.EventManager.Push<T>(data);
		public static void Pull<T>(this EntitySystem es, Type type) where T : class, ICommand => es.World.EventManager.Pull<T>(type);
		public static void Pub<T>(this EntitySystem es, T data) => es.World.EventManager.Publish(data);
		public static void Sub<T>(this EntitySystem es, object sub, Action<T> data) => es.World.EventManager.Subscribe<T>(sub, data);
	}
}