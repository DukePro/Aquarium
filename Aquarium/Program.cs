namespace Aquarium
{
    class Programm
    {
        static void Main()
        {
            Aquarium aquarium = new Aquarium();
            aquarium.Run();
        }
    }

    class Aquarium

    {
        private const string FishAdd = "1";
        private const string FishRemove = "2";
        private const string CycleStep = "3";
        private const string Exit = "0";

        public void Run()
        {
            string userInput;
            bool isExit = false;
            int cardDeckPositionY = 0;
            int cardTrayPositionY = 1;
            int dealerMessagePositionY = 3;
            int scorePositionY = 5;
            int gameMenuPositionY = 7;

            while (isExit == false)
            {
                Console.Clear();
                Console.SetCursorPosition(0, cardDeckPositionY);
                _dealer.CountDeck();
                Console.SetCursorPosition(0, cardTrayPositionY);
                _player.ShowHand();
                Console.SetCursorPosition(0, dealerMessagePositionY);
                _dealer.DecideWin(_player.CountScore());
                Console.SetCursorPosition(0, scorePositionY);
                Console.Write(_player.Name);
                _player.DisplayScore(_player.CountScore());
                Console.SetCursorPosition(0, gameMenuPositionY);
                Console.WriteLine(FishAdd + " - Взять карту");
                Console.WriteLine(FishRemove + " - Заново");
                Console.WriteLine(MenuExit + " - Выход");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case FishAdd:
                        TakeCard();
                        break;

                    case FishRemove:
                        TakeCard();
                        break;

                    case CycleStep:
                        Run();
                        break;

                    case MenuExit:
                        isExit = true;
                        break;
                }
            }
        }
    }

    class Fish
    {
        private Random _random = new Random();

        public Fish()
        {
            Index = CreateIndex();
            Age = 0;
            IsAlive = true;
            Colour = CreateColour();
        }

        public static int Index { get; private set; } = 0;
        public int Age { get; private set; }
        public bool IsAlive { get; private set; }
        public string Colour { get; private set; }

        private int CreateIndex()
        {
            return Index++;
        }

        private int CreateAge()
        {
            int minAge = 0;
            int maxAge = 36;
            int age = _random.Next(minAge, maxAge);
            return age;
        }

        private string CreateColour()
        {
            string[] colours = new string[] {
            "Red",
            "Green",
            "Yellow",
            "Blue",
            "Magenta",
            "Cyan",
            "DarkRed",
            "DarkGreen",
            "DarkYellow",
            "DarkBlue",
            "DarkMagenta",
            "DarkCyan"
        };

            return colours[_random.Next(0, colours.Length - 1)];
        }

        public void GetOld(Fish fish)
        {
            fish.Age++;
        }

        public void KillFish(Fish fish)
        {
            int deathProbability;
            int deathProbabilityIncrement = 1;
            int minDeathProbability = 1;
            int maxDeathProbability = 100;
            int ageIndex;

            ageIndex = fish.Age / 100;
        }
    }
}