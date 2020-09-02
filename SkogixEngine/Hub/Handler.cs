using System;

namespace ECS
{
    public class Handler
    {
       public Delegate Action { get; set; } 
       public Type Type { get; set; }
       public WeakReference Sender { get; set; }
    }
}