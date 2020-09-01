using RogueLike.Data;
using RogueLike.Data.Components;

namespace RogueLike.Events
{
    public class MoveEvent
    {
        public int XTarget;
        public int YTarget;

        public MoveEvent(int xTarget, int yTarget)
        {
            XTarget = xTarget;
            YTarget = yTarget;
        }
    }
}