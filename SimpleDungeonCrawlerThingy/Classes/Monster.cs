using System;

namespace SimpleDungeonCrawlerThingy
{
    public class Monster
    {
        
        Random RNG = new Random();
        
        public static int counter = 0;
        public string Name;
        public int HP;
        public int DMG;
        public int LootGold;

        static string[] monsters = { "Szlam", "Szkielet", "Behemot"/*, "Wantuch" */ };
        public Monster()
        {
            counter++;
            Name = monsters[RNG.Next(0, monsters.Length)];
            switch(Name)
            {
                case "Szlam": HP = 5; DMG = 1; LootGold = RNG.Next(1, 11); break;
                case "Szkielet": HP = 8; DMG = 2; LootGold = RNG.Next(11, 26); break;
                case "Behemot": HP = 12; DMG = 3; LootGold = RNG.Next(26, 51); break;
                case "Wantuch": HP = 999; DMG = 999; LootGold = 999; break;
            }
        }
    }
}
