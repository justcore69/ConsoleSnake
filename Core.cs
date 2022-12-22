using System;
using System.Collections.Generic;

namespace Core
{
    public readonly struct Position
    {
        public int x { get; }
        public int y { get; }

        public Position(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public static Position operator +(Position a, Position b) => new Position(a.x + b.x, a.y + b.y);

        public static implicit operator string(Position _pos) => $"({_pos.x},{_pos.y})";

    }

    public static class RenderCore
    {
        public static void DrawPixel(char _pixel, Position _pos, ConsoleColor _color)
        {
            Console.ForegroundColor = _color;
            Console.SetCursorPosition(_pos.x, _pos.y);
            Console.Write(_pixel);
        }

        public static void DrawPixel(char _pixel, Position _pos) => DrawPixel(_pixel, _pos, ConsoleColor.Gray);

        public static void DrawText(string _text, Position _pos, ConsoleColor _color)
        {
            Console.ForegroundColor = _color;
            Console.SetCursorPosition(_pos.x, _pos.y);
            Console.Write(_text + "            ");
        }

        public static void DrawText(string _text, Position _pos) => DrawText(_text, _pos, ConsoleColor.Gray);
    }
}
