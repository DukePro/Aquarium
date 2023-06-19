﻿namespace Aquarium
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
        public static int MesagePositionY = 4;
        private const string FishAdd = "1";
        private const string FishRemove = "2";
        private const string CycleStep = "3";
        private const string Exit = "0";

        public void Run()
        {
            string userInput;
            bool isExit = false;
            int menuPositionY = 0;
            int fishesListPositionY = 6;

            Aquarium aquarium = new Aquarium();

            while (isExit == false)
            {
                Console.SetCursorPosition(0, menuPositionY);
                Console.WriteLine(FishAdd + " - Add new fish");
                Console.WriteLine(FishRemove + " - Remove fish");
                Console.WriteLine(CycleStep + " - Skip 1 month");
                Console.WriteLine(Exit + " - Exit\n");

                Console.SetCursorPosition(0, fishesListPositionY);
                CleanConsoleBelowLine();
                aquarium.ShowAllFishes();

                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case FishAdd:
                        CleanMesageString();
                        aquarium.AddFish();
                        CleanMesageString();
                        break;

                    case FishRemove:
                        CleanMesageString();
                        aquarium.RemoveFish();
                        break;

                    case CycleStep:
                        CleanMesageString();
                        aquarium.CycleStep();
                        break;

                    case Exit:
                        isExit = true;
                        break;
                }
            }
        }

        private void CleanConsoleBelowLine()
        {
            int currentLineCursor = Console.CursorTop;

            for (int i = currentLineCursor; i < Console.WindowHeight; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.WindowWidth));
            }

            Console.SetCursorPosition(0, currentLineCursor);
        }

        private void CleanMesageString()
        {
            Console.SetCursorPosition(0, MesagePositionY);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, MesagePositionY);
        }
    }

    class Aquarium
    {
        private static int _capacity = 5;
        private List<Fish> _fishes = new List<Fish>();

        public void AddFish()
        {
            

            if (_fishes.Count <= _capacity)
            {
                _fishes.Add(new Fish());

                Console.SetCursorPosition(0, Menu.MesagePositionY);
                Console.WriteLine();
            }
            else
            {
                Console.SetCursorPosition(0, Menu.MesagePositionY);
                Console.WriteLine();
                Console.WriteLine("Max auqarium capacity riched");
            }
        }

        public void RemoveFish()
        {
            string userInput;
            int indexFromUser;
            Console.SetCursorPosition(0, Menu.MesagePositionY);
            Console.Write("Enter index to remove fish: ");
            userInput = Console.ReadLine();
            indexFromUser = Convert.ToInt32(userInput);

            for (int i = 0; i < _fishes.Count; i++)
            {
                if (_fishes[i].Index == indexFromUser)
                {
                    _fishes.RemoveAt(i);
                }
            }

            Console.Clear();
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
                Console.SetCursorPosition(0, Menu.MesagePositionY);
                Console.WriteLine("Aquarium is empty");
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

        public void CycleStep()
        {
            foreach (Fish fish in _fishes)
            {
                fish.GetOld(fish);
            }
        }
    }

    class Fish
    {
        private static int _index = 0;
        private int _minAge = 0;
        private int _maxAge = 36;
        private Random _random = new Random();

        public Fish()
        {
            Index = _index++;
            Age = _random.Next(_minAge, _maxAge);
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

            return colours[_random.Next(0, colours.Length - 1)];
        }

        public void GetOld(Fish fish)
        {
            KillFish(fish);
            fish.Age++;
        }

        public void KillFish(Fish fish)
        {
            int deathProbability;
            int minDeathProbability = 1;
            int maxDeathProbability = 100;
            int chanceToLive;
            double ageIndex;

            ageIndex = fish.Age * maxDeathProbability;
            deathProbability = Convert.ToInt32(Math.Floor(ageIndex / _maxAge));
            chanceToLive = _random.Next(minDeathProbability, maxDeathProbability);

            if (chanceToLive <= deathProbability)
            {
                IsAlive = false;
            }
        }
    }
}