using System;

namespace SimpleDungeonCrawlerThingy
{
    public class Item
    {
        private static Random rng = new Random();

        public static int counter = 0;
        public string Name { get; set; }
        public string Description { get; set; }
        public int HealRegenerate { get; set; } = 0;
        public int HealthBonus { get; set; } = 0;
        public int StrengthBonus { get; set; } = 0;
        public ItemType Type { get; set; }

        public enum ItemType
        {
            NONE,
            HEALPOT,
            STRENGTHPOT,
            HEALTHCHARM,
            SWORD
        }

        public Item(ItemType _type = ItemType.NONE)
        {
            counter++;
            Type = _type;
            if(_type != ItemType.NONE)
                Type = (ItemType)rng.Next(0, Enum.GetNames(typeof(ItemType)).Length);
            switch (Type)
            {
                //case ItemType.NONE: Name = "Nic";
                //                    Description = "Nie mam bladego pojęcia jak ci się udało to zdobyć. Poważnie, jak?"; break;

                case ItemType.HEALPOT: Name = "Mikstura zdrowia"; 
                                       Description = "Wywar o zachęcającym, czerwonym kolorze.\nUzdrawia 6 HP.";
                                       HealRegenerate = 6; break;

                case ItemType.STRENGTHPOT: Name = "Mikstura siły"; 
                                           Description = "Dziwnie pachnie.\nWzmacnia atak o 2.";
                                           StrengthBonus = 2; break;

                case ItemType.HEALTHCHARM: Name = "Talizman"; 
                                           Description = "Wygrawerowano na nim podobiznę pewnego Boga.\nZwiększa HP o 3.";
                                           HealthBonus = 3; break;

                case ItemType.SWORD: Name = "Miecz";
                                     Description = "Prosty, żelazny miecz. Ostrym końcem w stronę wrogów proszę.\nZwiększa atak o 2.";
                                     StrengthBonus = 2; break;
            }
        }
    }
}
