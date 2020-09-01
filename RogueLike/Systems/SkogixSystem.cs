using System;
using ECS.EntitySystem;
using ECS.Events;
using ECS.Hub;
using ECS.World;
using RogueLike.Events;

namespace RogueLike.Systems
{
    public class SkogixSystem : EntitySystem
    {
        public override void Init()
        {
            World.Sub<MoveEvent>(OnMoveEvent);
        }

        private void OnMoveEvent(MoveEvent e)
        {
            World.Pub(new Debug("SkogixSystem lyssnade precis på ett moveevent"));
        }
    }
}