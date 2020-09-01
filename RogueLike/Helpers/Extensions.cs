using RogueLike.Data;
using RogueLike.Interfaces;

namespace RogueLike.Helpers
{
  public static class Extensions
  {
    public static void AddItem(this Inventory inventory, IITem item)
    {
      inventory.Items.Add(item);
    }
  }
}