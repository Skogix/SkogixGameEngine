namespace ECS
{
    public class Entity
    {
        public string Hash => $"{Id}-{Gen}";
        internal readonly int Id;
        internal readonly int Gen;

        public Entity(int id)
        {
            Id = id;
            Gen = 0;
        }
    }
}