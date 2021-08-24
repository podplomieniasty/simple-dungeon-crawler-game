using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace SimpleDungeonCrawlerThingy
{
    public class Game
    {
        public static void StartupSequence()
        {
            BorderSpeech("Witaj w SimpleDungeonCrawlerThingy!");
            GenerateChoices("Start", "Zakończ", "Twórcy");
            TakeAction();
            
        }


        private static void GameInProgress()
        {
            Player player = new Player();
            Random rng = new Random();
            Room room = null;
            NextRoom(player, room, rng);
        }

        private static void GenerateChoices(params string[] choices)
        {
            string str = String.Join(" + ", choices);
            BorderSpeech(str, "", false);
            Console.Write(">>");
        }

        private static void TakeAction(Player player = null, Room room = null, Random rng = null) //analiza wyborów
        {
            bool gameInProgress = Room.counter > 0 ? true : false;

            if (gameInProgress == false)    //OPCJE DLA MENU GŁÓWNEGO  
            {
                MenuRepeat:
                switch (Console.ReadLine().ToUpper().Trim()) // input
                {
                    case "ZAKOŃCZ": case "ZAKONCZ": 
                        Environment.Exit(0); break;

                    case "TWÓRCY": case "TWORCY":
                        BorderSpeech("Ta gra została stworzona przez Burcka. To dosłowny cud, że jakkolwiek działa.");
                        Console.ReadKey(); 
                        StartupSequence(); break;

                    case "START": 
                        Console.Clear(); 
                        GameInProgress(); break;

                    default: 
                        Console.Write("Błąd: niepoprawne polecenie.\n\n>>"); goto MenuRepeat;
                }
            }
            else //OPCJE W GRZE
            {
                RoomRepeat:
                switch (Console.ReadLine().ToUpper().Trim()) // input
                {
                    case "DALEJ":
                        if (room.Fight) // atakuje potwór
                        {
                            Console.Write($"Potwór blokuje ci drogę. Nie możesz uciec!\n\n>>"); goto RoomRepeat;
                        }
                        else // brak potwora
                        {
                            Console.Clear(); 
                            NextRoom(player, room, rng); break; 
                        }
                    //OPUŚĆ

                    case "OPIS":
                        Console.Write($"{room.Description}\n\n>>");
                        if (room.ContainsItem)
                            player.Equipment.Add(room.randomItem);
                        goto RoomRepeat;
                    //OPIS

                    case "ODPOCZNIJ":
                        if (room.RestedAlready) //po odpoczynku
                        {
                            Console.Write("Już tutaj odpoczywałeś. Powinieneś iść dalej\n\n>>"); goto RoomRepeat;
                        }
                        else if (room.Fight) //potwór atakuje
                        {
                            Console.Write("Nie możesz odpoczywać w trakcie walki!\n\n>>"); goto RoomRepeat;
                        }
                        else if (room.CanRest && room.RestedAlready == false) //odpoczynek 
                        {
                            if(player.PlayerHP == 10)
                                Console.Write($"Zdecydowałeś się odpocząć. Jesteś w pełni swoich sił.\n" +
                                              $"Twoje HP: {player.PlayerHP}\n\n>>");
                            else
                            {
                                player.PlayerHP += player.Heal;
                                if (player.PlayerHP > player.MaxHealth)
                                    player.PlayerHP = player.MaxHealth;
                                Console.Write($"Zdecydowałeś się odpocząć. Regenerujesz część swoich sił.\n" +
                                              $"Odnawiasz {player.Heal} pkt HP.\nTwoje HP: {player.PlayerHP}\n\n>>");
                            }
                            
                            room.RestedAlready = true;
                            goto RoomRepeat;
                        }
                        else
                            Console.Write("Nie możesz tutaj odpocząć. Musisz iść dalej.\n\n>>"); goto RoomRepeat; //nie można odpocząć
                        //ODPOCZNIJ

                    case "WALCZ":
                        if (!room.Fight) //brak potwora
                        {
                            Console.Write("Nie ma tu nikogo z kim mógłbyś walczyć.\n" +
                                          "Chyba że ze swoimi myślami.\n\n>>"); goto RoomRepeat;
                        }
                        else //atakuje potwór
                        {
                            room.roomsMonster.HP -= player.PlayerDMG;
                            Console.Write($"Zadałeś potworowi {player.PlayerDMG} pkt obrażeń!\nHP potwora: {room.roomsMonster.HP}\n");
                            if (room.roomsMonster.HP > 0)
                            {
                                player.PlayerHP -= room.roomsMonster.DMG;
                                Console.Write($"{room.roomsMonster.Name} zadał ci {room.roomsMonster.DMG} pkt obrażeń!\n" +
                                              $"Twoje HP: {player.PlayerHP}\n");
                                if (player.PlayerHP <= 0)
                                {
                                    Console.ReadKey();
                                    BadEndSequence();
                                }
                            }
                            else
                            {
                                player.Gold += room.roomsMonster.LootGold;
                                room.Fight = false;
                                Console.Write($"Udało ci się zabić potwora!\n");
                                Console.Write($"Zdobyłeś {room.roomsMonster.LootGold} sztuk złota!\n");
                            }
                            Console.Write("\n>>");
                            goto RoomRepeat;
                        }
                    //WALCZ

                    case "EKWIPUNEK":
                        if (player.Equipment.Count == 0)
                        {
                            Console.Write("Twój ekwipunek jest pusty.\n"); goto RoomRepeat;
                        }
                        else
                        {
                            BorderSpeech("EWKIPUNEK");
                            player.ReturnEquipment();
                        }
                        Console.Write("\n>>");
                        goto RoomRepeat;

                    case "UŻYJ":
                        Console.Write("\nBurcek please add inventories.");
                        goto RoomRepeat;

                    default: Console.Write("Błąd: niepoprawne polecenie.\n\n>>"); goto RoomRepeat;
                }
            }
        }

       private static void GoodEndSequence(Player player)
       {
            string bottom = $"Twoje statystyki:\nPrzebyte pokoje: {Room.counter - 1}\n" +
                $"Pokonane potwory: {Monster.counter}\n" +
                $"Zdobyte złoto: {player.Gold}\n" +
                $"Podniesione przedmioty: {Item.counter}\n" +
                $"\nNaciśnij dowolny przycisk, aby kontynuować";

            BorderSpeech("Udało ci się przejść labirynt!", bottom, true);
            Console.ReadKey();
            Environment.Exit(0);
       }

        private static void BadEndSequence()
        {
            BorderSpeech("-- NIE ŻYJESZ --", "Naciśnij dowolny przycisk, aby kontynuować", true);
            Console.ReadKey();
            Environment.Exit(0);
        }

        private static void BorderSpeech(string title, string bottom = "", bool clearConsole = false)
        {
            if (clearConsole) Console.Clear();
            Console.WriteLine($"+{new String('-', title.Length + 2)}+");
            Console.WriteLine($"| {title} |");
            Console.WriteLine($"+{new String('-', title.Length + 2)}+");
            if (bottom != null) Console.WriteLine(bottom);
        }



        private static void NextRoom(Player player, Room room, Random rng)
        {
            room = new Room();
            if (Room.counter <= 10)
            {
                Console.WriteLine(room.EntryDescription);
                GenerateChoices("Walcz", "Odpocznij", "Użyj", "Ekwipunek", "Opis", "Dalej");
                TakeAction(player, room, rng);

            }
            else
                GoodEndSequence(player);
        }
    }

}

