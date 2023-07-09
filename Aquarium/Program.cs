namespace Aquarium
{
    class Programm
    {
        static void Main()
        {
            Menu menu = new Menu();
            menu.Run();
        }
    }

    class Menu
    {
        private const string FishAdd = "1";
        private const string FishRemove = "2";
        private const string RemoveAllFishes = "3";
        private const string SkipTime = "4";
        private const string Exit = "0";

        public void Run()
        {
            string userInput;
            bool isExit = false;
            int menuPositionY = 0;
            int fishesListPositionY = 8;

            Aquarium aquarium = new Aquarium();
            UiOperations uiOperations = new UiOperations();

            while (isExit == false)
            {
                Console.SetCursorPosition(0, menuPositionY);
                Console.WriteLine(FishAdd + " - Add new fish");
                Console.WriteLine(FishRemove + " - Remove fish");
                Console.WriteLine(RemoveAllFishes + " - Clear Aquarium");
                Console.WriteLine(SkipTime + " - Skip 1 month");
                Console.WriteLine(Exit + " - Exit\n");

                Console.SetCursorPosition(0, fishesListPositionY);
                uiOperations.CleanConsoleBelowLine();
                aquarium.ShowAllFishes();
                Console.SetCursorPosition(0, uiOperations.UserInputPositionY);
                uiOperations.CleanInputString();
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case FishAdd:
                        uiOperations.CleanMesageString();
                        aquarium.AddFish();
                        break;

                    case FishRemove:
                        uiOperations.CleanMesageString();
                        aquarium.RemoveFish();
                        break;

                    case RemoveAllFishes:
                        uiOperations.CleanMesageString();
                        aquarium.RemoveAllFishes();
                        break;

                    case SkipTime:
                        uiOperations.CleanMesageString();
                        aquarium.SkipTime();
                        break;

                    case Exit:
                        isExit = true;
                        break;
                }
            }
        }
    }

    class Aquarium
    {
        private static int _capacity = 5;
        private List<Fish> _fishes = new List<Fish>();
        UiOperations uiOperations = new UiOperations();

        public void AddFish()
        {
            if (_fishes.Count < _capacity)
            {
                _fishes.Add(new Fish());
            }
            else
            {
                Console.SetCursorPosition(0, uiOperations.MesagePositionY);
                Console.WriteLine("Max auqarium capacity riched");
            }
        }

        public void RemoveFish()
        {
            Console.SetCursorPosition(0, uiOperations.MesagePositionY);
            Console.Write("Enter index to remove fish: ");

            Console.SetCursorPosition(0, uiOperations.UserInputPositionY);
            uiOperations.CleanInputString();
            string userInput = Console.ReadLine();
            int indexFromUser = Convert.ToInt32(userInput);

            for (int i = 0; i < _fishes.Count; i++)
            {
                if (_fishes[i].Index == indexFromUser)
                {
                    _fishes.RemoveAt(i);
                    uiOperations.CleanMesageString();
                    return;
                }
            }

            Console.Clear();
        }

        public void RemoveAllFishes()
        {
            _fishes.Clear();
        }

        public void ShowAllFishes()
        {
            if (_fishes.Count > 0)
            {
                foreach (Fish fish in _fishes)
                {
                    ShowFish(fish);
                }
            }
            else
            {
                Console.SetCursorPosition(0, uiOperations.MesagePositionY);
                uiOperations.CleanMesageString();
                Console.WriteLine("Aquarium is empty");
            }
            if (_fishes.Count < _capacity)
            {
                Console.SetCursorPosition(0, uiOperations.MesagePositionY - 1);
            }
        }

        public void SkipTime()
        {
            foreach (Fish fish in _fishes)
            {
                fish.GetOld(fish);
            }
        }

        private void ShowFish(Fish fish)
        {
            if (fish.IsAlive)
            {
                Console.ForegroundColor = fish.Colour;
                Console.WriteLine($"Fish number - {fish.Index}, Age - {fish.Age}, Status - Alive");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Fish number - {fish.Index}, Status - Dead");
                Console.ResetColor();
            }
        }
    }

    class Fish
    {
        private static int _index = 0;
        private int _minAge = 0;
        private int _maxAge = 36;

        public Fish()
        {
            Index = _index++;
            Age = RandomNumber.Rand.Next(_minAge, _maxAge);
            IsAlive = true;
            Colour = CreateColour();
        }

        public int Index { get; private set; }
        public int Age { get; private set; }
        public bool IsAlive { get; private set; }
        public ConsoleColor Colour { get; private set; }

        private ConsoleColor CreateColour()
        {
            ConsoleColor[] colours = new ConsoleColor[] {
            ConsoleColor.Red,
            ConsoleColor.Green,
            ConsoleColor.Yellow,
            ConsoleColor.Blue,
            ConsoleColor.Magenta,
            ConsoleColor.Cyan,
            ConsoleColor.DarkRed,
            ConsoleColor.DarkGreen,
            ConsoleColor.DarkYellow,
            ConsoleColor.DarkBlue,
            ConsoleColor.DarkMagenta,
            ConsoleColor.DarkCyan
        };

            return colours[RandomNumber.Rand.Next(0, colours.Length - 1)];
        }

        private void LiveOneMonth(Fish fish)
        {
            int minDeathProbability = 1;
            int maxDeathProbability = 100;

            double deathProbabilityByAge = fish.Age * maxDeathProbability;
            int deathProbability = Convert.ToInt32(Math.Floor(deathProbabilityByAge / _maxAge));
            int chanceToLive = RandomNumber.Rand.Next(minDeathProbability, maxDeathProbability);

            if (chanceToLive <= deathProbability)
            {
                IsAlive = false;
            }
        }

        public void GetOld(Fish fish)
        {
            LiveOneMonth(fish);
            fish.Age++;
        }
    }

    class RandomNumber
    {
        public static Random Rand = new Random();
    }

    class UiOperations
    {
        public int MesagePositionY { get; private set; } = 5;
        public int UserInputPositionY { get; private set; } = 7;

        public void CleanConsoleBelowLine()
        {
            int currentLineCursor = Console.CursorTop;

            for (int i = currentLineCursor; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.WindowWidth));
            }

            Console.SetCursorPosition(0, currentLineCursor);
        }

        public void CleanMesageString()
        {
            Console.SetCursorPosition(0, MesagePositionY);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, MesagePositionY);
        }

        public void CleanInputString()
        {
            Console.SetCursorPosition(0, UserInputPositionY);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, UserInputPositionY);
        }
    }
}