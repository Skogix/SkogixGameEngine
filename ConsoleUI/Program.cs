using System;
using ECS.Entity;
using ECS.EntitySystem;
using ECS.Events;
using ECS.Hub;
using ECS.World;
using RogueLike;
using RogueLike.Data;
using RogueLike.Events;
using RogueLike.Helpers;
using RogueLike.Systems;
using static RogueLike.Data.Items;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            var w = WorldFactory.CreateWorld<GameWorld>();

#if DEBUG
            w.Sub<Debug>(debug => Console.WriteLine(debug.Message) );
#endif
            
            DrawSystem drawSystem = w.AddSystem<DrawSystem>();
            InputSystem inputSystem = w.AddSystem<InputSystem>();
            SkogixSystem skogixSystem = w.AddSystem<SkogixSystem>();
            
            TileMap tileMap = w.GetPrototypeEntity<TileMap>();
            Skogix skogix = w.GetPrototypeEntity<Skogix>();
            Monster monster = w.GetPrototypeEntity<Monster>();

            skogix.Inventory.AddItem(new Sword("Svärd of doom!", 1000));


            w.PrintWorldInfo();
            while (true)
            {
                w.Run();
            }
        }
    }

}