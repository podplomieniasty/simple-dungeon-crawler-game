using System.Collections.Generic;
using System.Linq;
using System;

namespace SimpleDungeonCrawlerThingy
{
    public class Player
    {
        public int PlayerHP { get; set; } = 10;
        public int PlayerDMG { get; set; } = 4;
        public int Heal { get; set; } = 3;

        public int MaxHealth { get; set; } = 10;
        public int Gold { get; set; } = 0;

        public List<Item> Equipment = new List<Item>
        {
            new Item(Item.ItemType.HEALPOT), 
            new Item(Item.ItemType.HEALPOT)
        };

        public void ReturnEquipment()
        {
            Console.WriteLine( String.Join("\n", Equipment.GroupBy(x => x.Name).Select(x => x.Key + " x" + x.Count())) );
            ReturnEquipment();
        }

        public void ReloadEquipment()
        {
            foreach(Item it in Equipment)
            {
                MaxHealth += it.HealthBonus;
                PlayerDMG += it.StrengthBonus;
            }
        }
    }
}
