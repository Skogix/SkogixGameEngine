using System;
using ECS.Component;
using ECS.EntitySystem;
using RogueLike.Data.Components;

namespace RogueLike.Systems
{
    public class DrawSystem : EntitySystem, IRunSystem
    {
        public override void Init()
        {
            AddFilter<Transform>();
            AddFilter<Drawable>();
        }

        public void Run()
        {
#if RELEASE
            Console.Clear();
            foreach (var entity in Entities)
            {
                var glyph = entity.Get<Drawable>().Glyph;
                var transform = entity.Get<Transform>();
                
                Console.SetCursorPosition(transform.X, transform.Y);
                Console.Write(glyph);
            }
#else
            foreach (var entity in Entities)
            {
                if (entity.Has<Actor>())
                {
                    var glyph = entity.Get<Drawable>().Glyph;
                    var transform = entity.Get<Transform>();

                    Console.WriteLine($"{glyph}:");
                    Console.WriteLine($"\tX: {transform.X}");
                    Console.WriteLine($"\tY: {transform.Y}");
                }
            }
#endif
        }

    }
}