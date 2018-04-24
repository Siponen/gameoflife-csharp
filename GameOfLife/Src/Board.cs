using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Board
    {
        const int CELL_LIVES = 1;
        const int CELL_DIES = 0;
        const int CELL_UNDERPOPULATION_LIMIT = 2;
        const int CELL_OVERPOPULATION_LIMIT = 3;
        const int CELL_REPRODUCTION_LIMIT = 3;

        private byte[,] board;

        private Random random;
        public int Height { get; private set; }
        public int Width { get; private set; }
        public byte[,] Data { get { return board; } }

        public Board(int width, int height)
        {
            Width = width;
            Height = height;
            board = new byte[height, width];
            random = new Random(DateTime.Today.Millisecond);
        }

        public void InsertShape(int posX, int posY, int[,] shape)
        {
            var sizeY = shape.GetLength(0);
            var sizeX = shape.GetLength(1);

            if ((sizeX + posX > Width) || (sizeY + posY > Height)) return;

            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    board[posY + y, posX + x] = (byte)shape[y, x];
                }
            }
        }

        public void RandomInsert(int[,] shape)
        {
            int maxX = Width - shape.GetLength(1);
            int maxY = Height - shape.GetLength(0);

            var x = random.Next(maxX);
            var y = random.Next(maxY);

            InsertShape(x, y, shape);
        }

        public void UpdateCells()
        {
            byte[,] buffer = new byte[Height, Width];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int numLivingNeighbours = SumNeighbours(x, y);
                    int currentCellValue = board[y, x];
                    int newCellValue = GetNextValue(currentCellValue, numLivingNeighbours);
                    buffer[y, x] = (byte)newCellValue;
                }
            }
            board = buffer;
        }

        public void Draw()
        {
            StringBuilder sb = new StringBuilder();
            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    char c = (board[y, x] == 1) ? '*' : ' ';
                    sb.Append(c);
                }
                sb.Append('\n');
            }
            Console.Write(sb);
        }

        private int GetNextValue(int currentValue, int numLivingNeighbours)
        {
            int result = currentValue;
            bool isAlive = currentValue > 0;

            if (isAlive)
            {
                bool cellDies = numLivingNeighbours < CELL_UNDERPOPULATION_LIMIT || numLivingNeighbours > CELL_OVERPOPULATION_LIMIT;
                result = cellDies ? 0 : 1;
            }

            else if (numLivingNeighbours == CELL_REPRODUCTION_LIMIT)
            {
                result = 1;
            }

            return result;
        }

        private int SumNeighbours(int x, int y)
        {
            //Edges, assume them to be with 0. TODO: Stitch the edges and corners.
            if (x == 0 || x == Width - 1)
                return 0;
            else if (y == 0 || y == Height - 1)
                return 0;

            int nextRow = y + 1;
            int prevRow = y - 1;
            int nextColumn = x + 1;
            int prevColumn = x - 1;

            int sum = board[prevRow, prevColumn] + board[prevRow, x] + board[prevRow, nextColumn]
                + board[y, prevColumn] + board[y, nextColumn]
                + board[nextRow, prevColumn] + board[nextRow, x] + board[nextRow, nextColumn];

            return sum;
        }

        
    }
}
