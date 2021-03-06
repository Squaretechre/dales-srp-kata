using System.Collections.Generic;

namespace SrpTask.Game.ItemEffects
{
    public class ItemEffectsFactory
    {
        public static IEnumerable<ItemEffect> EffectsFor(Item item)
        {
            var effects = new List<ItemEffect>
            {
                new RegularItemEffect()
            };

            if (item.Heal > 0)
            {
                effects.Add(new AddHealthItemEffect());
                effects.Add(new LargeHealthQuantitySpecialEffect());
            }

            if (item.Rare)
            {
                effects.Add(new RareItemEffect());
            }

            if (item.Rare && item.Unique)
            {
                effects.Add(new RareAndUniqueEffect());
            }

            return effects;
        }
    }
}