using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core;
using Config;

namespace GameObject
{
    public static class Snake
    {
        public enum Direction
        {
            up = 0,
            right = 1,
            down = 2,
            left = 3,
        }

        public static Position headPosition;
        public static Direction direction;
        public static List<Position> tail = new List<Position> { };

        public static void Initialize(Position _pos)
        {
            headPosition = _pos;
            tail.Add(_pos);
        }

        public static void RenderSelf()
        {
            foreach (Position pos in tail)
            {
                if(pos == headPosition)
                {
                    RenderCore.DrawPixel(Cfg.CHR_HEAD, pos, ConsoleColor.Green);
                }
                else
                {
                    RenderCore.DrawPixel(Cfg.CHR_SNAKE, pos, ConsoleColor.DarkGreen);
                }
            }
        }

        public static void ChangeDirection()
        {
            switch (Cfg.CURRENT_KEY)
            {
                case ConsoleKey.LeftArrow:
                    direction = Direction.left;
                    break;
                case ConsoleKey.UpArrow:
                    direction = Direction.up;
                    break;
                case ConsoleKey.RightArrow:
                    direction = Direction.right;
                    break;
                case ConsoleKey.DownArrow:
                    direction = Direction.down;
                    break;
            }
        }

        public static void Move()
        {

            ChangeDirection();

            switch (direction)
            {
                case Direction.up:
                    headPosition += new Position(0, -1);
                    break;
                case Direction.right:
                    headPosition += new Position(1, 0);
                    break;
                case Direction.down:
                    headPosition += new Position(0, 1);
                    break;
                case Direction.left:
                    headPosition += new Position(-1, 0);
                    break;
            }

            tail.Add(headPosition);

            tail.RemoveAt(0);

            if (tail.Last().x <= 0 || tail.Last().x >= Cfg.SCENE_WIDTH - 1 || tail.Last().y <= 0 || tail.Last().y >= Cfg.SCENE_HEIGHT - 1)
            {
                Cfg.GAMEOVER = true;
            }

            if(tail.Last().x == Apple.position.x && tail.Last().y == Apple.position.y)
            {
                Cfg.SCORE++;
                Grow();
                Apple.Relocate();
            }
        }

        public static void Grow()
        {
            switch (direction)
            {
                case Direction.up:
                    tail.Add(new Position(headPosition.x, headPosition.y + 1));
                    break;
                case Direction.right:
                    tail.Add(new Position(headPosition.x - 1, headPosition.y));
                    break;
                case Direction.down:
                    tail.Add(new Position(headPosition.x, headPosition.y - 1));
                    break;
                case Direction.left:
                    tail.Add(new Position(headPosition.x + 1, headPosition.y));
                    break;
            }
        }
    }

    public static class Apple
    {
        public static Random random = new Random();

        public static Position position;

        public static void Initialize()
        {
            position = new Position(random.Next(1, Cfg.SCENE_WIDTH - 1), random.Next(1, Cfg.SCENE_HEIGHT - 1));
        }

        public static void RenderSelf()
        {
            RenderCore.DrawPixel(Cfg.CHR_APPLE, new Position(position.x, position.y), ConsoleColor.Red);
        }

        public static void Relocate()
        {
            Initialize();
        }
    }
}
