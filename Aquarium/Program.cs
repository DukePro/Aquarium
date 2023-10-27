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

            Aquarium aquarium = new Aquarium();

            while (isExit == false)
            {
                Console.SetCursorPosition(0, menuPositionY);
                Console.WriteLine(FishAdd + " - Add new fish");
                Console.WriteLine(FishRemove + " - Remove fish");
                Console.WriteLine(RemoveAllFishes + " - Clear Aquarium");
                Console.WriteLine(SkipTime + " - Skip 1 month");
                Console.WriteLine(Exit + " - Exit\n");

                UiOperations.SetCursourFishesListLine();
                UiOperations.CleanConsoleBelowLine();
                aquarium.ShowAllFishes();
                UiOperations.SetCursourUserInputLine();
                UiOperations.CleanString();
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case FishAdd:
                        UiOperations.CleanString();
                        aquarium.AddFish();
                        break;

                    case FishRemove:
                        UiOperations.CleanString();
                        aquarium.RemoveFish();
                        break;

                    case RemoveAllFishes:
                        UiOperations.CleanString();
                        aquarium.RemoveAllFishes();
                        break;

                    case SkipTime:
                        UiOperations.CleanString();
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

        public void AddFish()
        {
            if (_fishes.Count < _capacity)
            {
                _fishes.Add(new Fish());
            }
            else
            {
                UiOperations.SetCursourMessageLine();
                UiOperations.CleanString();
                Console.WriteLine("Max auqarium capacity riched");
            }
        }

        public void RemoveFish()
        {
            UiOperations.SetCursourMessageLine();
            UiOperations.CleanString();
            Console.Write("Enter index to remove fish: ");

            UiOperations.SetCursourUserInputLine();
            UiOperations.CleanString();
            string userInput = Console.ReadLine();
            int indexFromUser;

            if (int.TryParse(userInput, out indexFromUser))
            {
                indexFromUser = Convert.ToInt32(userInput);
            }
            else
            {
                UiOperations.SetCursourMessageLine();
                UiOperations.CleanString();
                Console.Write("Wrong index");
                return;
            }

            for (int i = 0; i < _fishes.Count; i++)
            {
                if (_fishes[i].Index == indexFromUser)
                {
                    _fishes.RemoveAt(i);
                    Console.Clear();
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
                UiOperations.SetCursourFishesListLine();

                foreach (Fish fish in _fishes)
                {
                    ShowFish(fish);
                }
            }
            else if (_fishes.Count == 0)
            {
                UiOperations.SetCursourMessageLine();
                UiOperations.CleanString();
                Console.WriteLine("Aquarium is empty");
            }
            if (_fishes.Count == 1)
            {
                UiOperations.SetCursourMessageLine();
                UiOperations.CleanString();
            }
        }

        public void SkipTime()
        {
            foreach (Fish fish in _fishes)
            {
                fish.Grow(fish);
            }
        }

        private void ShowFish(Fish fish)
        {
            if (fish.IsAlive)
            {
                Console.ForegroundColor = fish.Colour;
                Console.WriteLine($"Fish number - {fish.Index}, Age - {fish.Age}, Status - Alive");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine($"Fish number - {fish.Index}, Status - Dead");
            }

            Console.ResetColor();
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
            Age = MyRandom.GetRandomNumber(_minAge, _maxAge);
            IsAlive = true;
            Colour = GenerateColour();
        }

        public int Index { get; private set; }
        public int Age { get; private set; }
        public bool IsAlive { get; private set; }
        public ConsoleColor Colour { get; private set; }

        public void Grow(Fish fish)
        {
            LiveOneMonth(fish);
            fish.Age++;
        }

        private ConsoleColor GenerateColour()
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

            return colours[MyRandom.GetRandomNumber(0, colours.Length - 1)];
        }

        private void LiveOneMonth(Fish fish)
        {
            int minDeathProbability = 1;
            int maxDeathProbability = 100;

            double deathProbabilityByAge = fish.Age * maxDeathProbability;
            int deathProbability = (int)Math.Floor(deathProbabilityByAge / _maxAge);
            int chanceToLive = MyRandom.GetRandomNumber(minDeathProbability, maxDeathProbability);

            if (chanceToLive <= deathProbability)
            {
                IsAlive = false;
            }
        }
    }

    class MyRandom
    {
        private static Random _random = new Random();

        public static int GetRandomNumber(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }

    static class UiOperations
    {
        public static void CleanConsoleBelowLine()
        {
            int currentLineCursor = Console.CursorTop;

            for (int i = currentLineCursor; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.WindowWidth));
            }

            Console.SetCursorPosition(0, currentLineCursor);
        }

        public static void SetCursourMessageLine()
        {
            int mesagePositionY = 5;
            Console.SetCursorPosition(0, mesagePositionY);
        }

        public static void SetCursourUserInputLine()
        {
            int userInputPositionY = 7;
            Console.SetCursorPosition(0, userInputPositionY);
        }

        public static void SetCursourFishesListLine()
        {
            int fishesListPositionY = 9;
            Console.SetCursorPosition(0, fishesListPositionY);
        }

        public static void CleanString()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
        }
    }
}