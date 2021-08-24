using System;

namespace SimpleDungeonCrawlerThingy
{
    public class Room
    {
        static Random RNG = new Random();

        public static int counter = 0;  //licznik pomieszczeń
        public string Description { get; set; }  //opcja OPIS
        public string CountingOnEntry { get; set; } = $"---+ Pomieszczenie {counter + 1} +---\n"; //numer lokacji 
        public bool CanRest { get; set; } = false;  //decyzja, czy można odpocząć
        public bool RestedAlready { get; set; } = false;  //sprawdzenie, czy już odpoczywano
        public bool Fight { get; set; } = false;
        public bool ContainsItem { get; set; } = false;
        public string EntryDescription { get; set; } //wiadomość na wejście do pomieszczenia


        private string _currentRoom = roomTypes[RNG.Next(0, roomTypes.Length)];  //wybór typu pomieszczenia
        
        public Monster roomsMonster;
        public Item randomItem;

        static string[] roomTypes = { "Empty", "Safe" }; //typy pomieszczeń

        static string[] entryDescriptions =
        {
            "Wchodzisz ostrożnie do tajemniczego labiryntu.\nNie jesteś pewien, czego oczekiwać.",
            "Przechodzisz do kolejnego pomieszczenia."
        };

        static string[] emptyDescriptions =      //opis pustych pomieszczeń
        {
            "Pomieszczenie jest małe oraz wilgotne. \nDrobny strumyk przepływa przez jego środek.",
            "W pomieszczeniu unosi się gęsta mgła. \nEcho twoich kroków odbija się po całości pokoju.",
            "Pomieszczenie jest wychłodzone i oszronione. \nOstrożnie stąpasz po oblodzonej posadzce."
        };
        static string[] safeDescriptions =   //opis bezpiecznych pomieszczeń
        {
            "Całe pomieszczenie pokryła dziwna, zielona narośl. \nCzujesz pod swoimi nogami mięciutki mech.",
            "Pomieszczenie rozświetla blask ogniska. \nBijące od niego ciepło uspokaja cię."
        };



        public Room()
        {
            Console.WriteLine(CountingOnEntry);
            counter++;
            Fight = RNG.Next(0, 2) == 1 && _currentRoom != "Safe" ? true : false;
            ContainsItem = ( RNG.Next(0, 2) == 1 
                             && _currentRoom == "Empty" 
                             && Fight == false ) ? true : false;

            EntryDescription = (counter == 1) ? entryDescriptions[0] : entryDescriptions[1];
            switch (_currentRoom)   //definiowanie opisu pokoju w zależności od typu
            {
                case "Empty": Description = emptyDescriptions[RNG.Next(0, emptyDescriptions.Length)]; break;
                case "Safe": Description = safeDescriptions[RNG.Next(0, safeDescriptions.Length)]; 
                    CanRest = true; EntryDescription += "\nPomieszczenie wydaje ci się bezpieczne."; 
                    Description += "\nMożesz tutaj odpocząć."; 
                    break;
            }
            if (Fight)
            {   
                roomsMonster = new Monster();
                EntryDescription += $"\nW pomieszczeniu napotykasz potwora: {roomsMonster.Name}!";
                Description += $"\nW pomieszczeniu znajduje się {roomsMonster.Name}!";
            }
            if(ContainsItem)
            {
                randomItem = new Item();
                Description += $"\nPrzeszukując pomieszczenie dostrzegasz na ziemi przedmiot: {randomItem.Name}.\nPodnosisz {randomItem.Name}.";
            }
        }
    }
}
