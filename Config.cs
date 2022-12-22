using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core;

namespace Config
{
    public static class Cfg
    {
        public static int SCORE = 0;
        public static int HIGH_SCORE = 0;
        public static bool GAMEOVER = false;

        public static int RENDER_DELAY = 450;

        public static Position UI_RENDER_POSITION = new Position(0, SCENE_HEIGHT + 1);

        public static int SCENE_WIDTH = 30; //Playing field width
        public static int SCENE_HEIGHT = 15; //Playing field height

        public static char CHR_WALL = ':';
        public static char CHR_SNAKE = '.';
        public static char CHR_HEAD = 'o';
        public static char CHR_APPLE = 'e';

        public static ConsoleKey CURRENT_KEY = ConsoleKey.RightArrow;
    }
}
