using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    static public class ShapeFactory
    {
        //Still life shapes
        static public int[,] MakeBlock()
        {
            int[,] cells = new int[,]
            {
                { 1,1 },
                { 1,1 }
            };
            return cells;
        }

        static public int[,] MakeBoat()
        {
            int[,] cells = new int[,]{
                { 1,1,0 },
                { 1,0,1 },
                { 0,1,0 },
            };
            return cells;
        }

        //Oscillator shapes
        static public int[,] MakeBlinker()
        {
            int[,] cells = new int[,] {
                { 1, },
                { 1, },
                { 1, }
            };

            return cells;
        }

        static public int[,] MakeBeacon()
        {
            int[,] cells = new int[,]
            {
                { 1,1,0,0 },
                { 1,1,0,0 },
                { 0,0,1,1 },
                { 0,0,1,1 }
            };
            return cells;
        }

        static public int[,] MakeToad()
        {
            int[,] cells = new int[,]
            {
                { 0,1,1,1},
                { 1,1,1,0},
            };
            return cells;
        }

        //Spaceships
        static public int[,] MakeGlider()
        {
            int[,] cells = new int[,]
            {
                { 0,0,1 },
                { 1,0,1 },
                { 0,1,1 },
            };
            return cells;
        }
    }
}
