using System;
using System.Threading;
using System.Collections.Generic;

using Config;
using Core;
using GameObject;

namespace Game
{
    public static class Program
    {
        public static int updateIteration = 0;

        static void Main()
        {
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
            Console.CursorVisible = false;
            Console.Title = "CSharp Snake by JustAnCore";

            if (File.Exists("highscore.txt"))
            {
                using (StreamReader reader = new StreamReader("highscore.txt"))
                {
                     int.TryParse(reader.ReadLine(), out Cfg.HIGH_SCORE);
                }
            }

            Start();
        }

        public static void Start()
        {
            Snake.Initialize(new Position(5, 10));
            Apple.Initialize();

            Renderer.RenderBorders();

            Update();
        }

        public static void Update()
        {
            while (!Cfg.GAMEOVER)
            {
                while (Console.KeyAvailable)
                {
                    Cfg.CURRENT_KEY = Console.ReadKey(intercept: true).Key;
                }

                if (Cfg.SCORE > Cfg.HIGH_SCORE) Cfg.HIGH_SCORE = Cfg.SCORE;

                Renderer.RenderUI();

                Snake.Move();

                Renderer.Clear();
                Renderer.RenderGameObjects();

                Thread.Sleep(Cfg.RENDER_DELAY);
                updateIteration++;
            }

            using (StreamWriter writer = new StreamWriter("highscore.txt"))
            {
                writer.WriteLine(Cfg.HIGH_SCORE);
            }

            while (Cfg.GAMEOVER)
            {
                Renderer.RenderUI();

                while (Console.KeyAvailable)
                {
                    Cfg.CURRENT_KEY = Console.ReadKey(intercept: true).Key;
                }
                if (Cfg.CURRENT_KEY == ConsoleKey.R) { Restart(); break; }
            }
        }

        public static void Restart()
        {
            Console.Clear();
            Snake.tail.Clear();
            Cfg.GAMEOVER = false;
            Cfg.SCORE = 0;
            Start();
        }
    }

    public static class Renderer
    {
        public static void Clear()
        {
            for (int y = 1; y < Cfg.SCENE_HEIGHT - 1; y++)
            {
                for (int x = 1; x < Cfg.SCENE_WIDTH - 1; x++)
                {
                    RenderCore.DrawPixel(' ', new Position(x, y));
                }
            }
        }

        public static void RenderBorders()
        {
            for(int y = 0; y < Cfg.SCENE_HEIGHT; y++)
            {
                for (int x = 0; x < Cfg.SCENE_WIDTH; x++)
                {
                    if(y == 0 || y == Cfg.SCENE_HEIGHT - 1)
                    {
                        RenderCore.DrawPixel(Cfg.CHR_WALL, new Position(x, y));
                    }else if(x == 0 || x == Cfg.SCENE_WIDTH - 1)
                    {
                        RenderCore.DrawPixel(Cfg.CHR_WALL, new Position(x, y));
                    }
                }
            }
        }

        public static void RenderUI()
        {
            if (Cfg.GAMEOVER) RenderCore.DrawText("GAME OVER - Press R to restart", new Position(0, Cfg.SCENE_HEIGHT + 1), ConsoleColor.Red);
            else RenderCore.DrawText(" ", new Position(0, Cfg.SCENE_HEIGHT + 1), ConsoleColor.Red);

            RenderCore.DrawText("SCORE: " + Cfg.SCORE + " | HIGH SCORE: " + Cfg.HIGH_SCORE , new Position(0, Cfg.SCENE_HEIGHT + 2), ConsoleColor.Green);
            RenderCore.DrawText("-----------", new Position(0, Cfg.SCENE_HEIGHT + 3), ConsoleColor.Gray);
            RenderCore.DrawText("Iteration: " + Program.updateIteration, new Position(0, Cfg.SCENE_HEIGHT + 4), ConsoleColor.Gray);
            RenderCore.DrawText("Current key: " + Cfg.CURRENT_KEY, new Position(0, Cfg.SCENE_HEIGHT + 5), ConsoleColor.Gray);
        }

        public static void RenderGameObjects()
        {
            Snake.RenderSelf();
            Apple.RenderSelf();
        }
    }
}