using System;
using System.Threading;

namespace DinoGame
{
    internal class Program
    {
        private static string dino = "       |";
        private static string cactus = "                                       f";
        private static bool isJump = false;
        private static bool isAlive = true;
        private static int direction = 0;
        private static int speedUp = 0;
        private static int speed = 100;

        private static void Main()
        {
            Console.CursorVisible = false;
            Console.WriteLine(
                "--------------------------------------------------\n" +
                "\n" +
                "\n" +
                "\n" +
                "           Press \"Enter\" to start the game\n" +
                "\n" +
                "\n" +
                "               Press \"Esc\" to exit\n" +
                "\n" +
                "\n" +
                "\n" +
                "--------------------------------------------------");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Enter: Start(); break;
                case ConsoleKey.Escape: break;
                default: Console.Clear(); Main(); break;
            }
        }

        private static void Start()
        {
            int score = 0;
            string[,] gameField =
            {
                { "", "" },
                { "                                         ", "" },
                { "", "" },
                { "", "" },
                { "", "" },
                { "", "" },
                { "", "" },
                { "", "" },
                { "", "" },
                { "", "" },
                { "", "" },
                { "--------------------------------------------------", "" }
            };

            while (isAlive == true && score != int.MaxValue)
            {
                gameField[10, 0] = dino;

                CreateCactus(ref gameField[10, 1]);

                while (Console.KeyAvailable)
                {
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.Escape: return;
                        case ConsoleKey.Spacebar:
                        case ConsoleKey.UpArrow: Jump(ref gameField[10, 0]); break;
                        default: break;
                    }
                    break;
                }

                if (speedUp == 10 && speed > 1)
                {
                    speed -= 1;
                    speedUp = 0;
                }

                if (isJump == true)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Render(gameField);
                    }
                    gameField[10, 0] = dino;
                    isJump = false;
                }
                Colision(gameField);
                Render(gameField);

                gameField[1, 1] = $"score: {score++}";
                speedUp++;
            }
            GameOver(score);
        }

        private static void CreateCactus(ref string cactusField)
        {
            Random rand = new Random();

            if (rand.Next(1, 10) == 1 && cactusField == string.Empty)
                cactusField = cactus;
        }

        private static void Jump(ref string dino)
        {
            if (isJump == false)
            {
                isJump = true;
                dino += "\n       ";
            }
        }

        private static bool Colision(string[,] gameField)
        {
            return gameField[10, 1] == "f" ||
                gameField[10, 1] == " f" &&
                gameField[10, 0] == dino ?
                isAlive = false : isAlive = true;
        }

        private static void Render(string[,] gameField)
        {
            Console.Clear();

            int rows = gameField.GetUpperBound(0) + 1;
            int columns = gameField.GetUpperBound(1) + 1;

            if (gameField[10, 1] != string.Empty)
            {
                gameField[10, 1] = cactus.Substring(direction++);
                if (direction == cactus.Length)
                {
                    direction = 0;
                    gameField[10, 1] = string.Empty;
                }
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(gameField[i, j]);
                }
                Console.WriteLine();
            }
            Thread.Sleep(speed);
        }

        private static void GameOver(int score)
        {
            Console.Clear();
            Console.WriteLine(
                "--------------------------------------------------\n" +
                "\n" +
                "\n" +
               $"                  Your score: {score}\n" +
                "\n" +
                "\n" +
                "         Press \"Enter\" to restart the game\n" +
                "\n" +
                "               Press \"Esc\" to exit\n" +
                "\n" +
                "\n" +
                "--------------------------------------------------");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Enter: Start(); break;
                case ConsoleKey.Escape: break;
                default: Console.Clear(); GameOver(score); break;
            }
        }
    }
}
