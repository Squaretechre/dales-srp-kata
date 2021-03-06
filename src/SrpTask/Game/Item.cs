﻿using SrpTask.Game.ItemEffects;

namespace SrpTask.Game
{
    public class Item
    {
        public readonly bool Rare;

        public Item(int id, string name, int heal, int armour, int weight, bool unique, bool rare)
        {
            Rare = rare;
            Name = name;
            Heal = heal;
            Armour = armour;
            Weight = weight;
            Unique = unique;
            Id = id;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Heal { get; set; }
        public int Armour { get; set; }
        public int Weight { get; set; }
        public bool Unique { get; set; }

        public static bool ItemIsTooHeavyToPickupRule(Item item, Player player)
        {
            var weight = player.CalculateInventoryWeight();
            var itemWeightIsOverPlayerCarryingCapacity = weight + item.Weight > player.CarryingCapacityInKilograms;
            return itemWeightIsOverPlayerCarryingCapacity;
        }

        public static bool UniqueItemPickupRule(Item item, Player player)
        {
            var itemIsUniqueAndPlayerAlreadyHasIt = item.Unique && player.CheckIfItemExistsInInventory(item);
            return itemIsUniqueAndPlayerAlreadyHasIt;
        }

        public void ActionForPlayer(Player player)
        {
            if (ItemIsTooHeavyToPickupRule(this, player)) return;
            if (UniqueItemPickupRule(this, player)) return;

            foreach (var effect in ItemEffectsFactory.EffectsFor(this))
                effect.Effect(this, player, player.GameEngine, AddItemToInventory);
        }

        private void AddItemToInventory(Item item, Player player)
        {
            if (player.Inventory.Contains(item)) return;
            if (item.Heal > 0) return;

            player.Inventory.Add(this);
        }
    }
}