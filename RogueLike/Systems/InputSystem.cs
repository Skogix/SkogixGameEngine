using System;
using ECS.Component;
using ECS.EntitySystem;
using ECS.Hub;
using RogueLike.Data.Components;
using RogueLike.Events;

namespace RogueLike.Systems
{
    public class InputSystem : EntitySystem, IRunSystem
    {
        public override void Init()
        {
            AddFilter<Actor>();
            AddFilter<Player>();
        }

        public void Run()
        {
            foreach (var entity in Entities)
            {
                var actor = entity.Get<Actor>();
                var transform = entity.Get<Transform>();

                var key = Console.ReadKey().KeyChar;
                switch (key)
                {
                    case ',':
                        World.Pub(new MoveEvent(transform.X, transform.Y - 1));
                        break;
                    case 'e':
                        World.Pub(new MoveEvent(transform.X + 1, transform.Y));
                        break;
                    case 'o':
                        World.Pub(new MoveEvent(transform.X, transform.Y + 1));
                        break;
                    case 'a':
                        World.Pub(new MoveEvent(transform.X - 1, transform.Y));
                        break;
                    
                }
            }
        }
    }
}