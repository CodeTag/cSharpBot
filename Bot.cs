using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace cSharpbot
{
    class Bot
    {
        private char letter = ' ';
        private int x;
        private int y;
        private String[][] map;

        public Bot(char letter)
        {
            this.letter = letter;

        }

        public void updateMap(string mapa)
        {
            string[] rows = Regex.Split(mapa, "\n");
            map = new String[rows.Length][];
            for (int i = 0; i < rows.Length; i++)
            {
                map[i] = Regex.Split(rows[i], ",");
            }
            int pos = mapa.IndexOf(this.letter) / 2;
            this.y = pos / rows.Length;
            this.x = pos % rows.Length;
        }

        public String move()
        {
            Random random = new Random();
            int mov = random.Next(0, 7);
            Console.WriteLine("mov " + mov);
            switch (mov)
            {
                case 0:
                    return "N";
                case 1:
                    return "E";
                case 2:
                    return "S";
                case 3:
                    return "O";
                case 4:
                    return "BN";
                case 5:
                    return "BE";
                case 6:
                    return "BS";
                case 7:
                    return "BO";
            }
            return "P";
        }

    }
}
